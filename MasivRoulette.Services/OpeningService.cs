using System;
using System.Collections.Generic;
using System.Data;
using MasivRoulette.DataAccess.Interfaces;
using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;
using MasivRoulette.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MasivRoulette.Services
{
    public class OpeningService : IOpeningService
    {
        #region Constructor - Interfaces
        public IOpeningDac OpeningDac { get; set; }
        public IRouletteDac RouletteDac { get; set; }
        public IBetDac BetDac { get; set; }
        public IUserDac UserDac { get; set; }
        public OpeningService(IOpeningDac OpeningDac, IRouletteDac RouletteDac, IBetDac BetDac, IUserDac UserDac)
        {
            this.OpeningDac = OpeningDac;
            this.RouletteDac = RouletteDac;
            this.BetDac = BetDac;
            this.UserDac = UserDac;
        }
        #endregion Constructor - Interfaces

        #region Methods
        public ResponseServiceModel OpenOpening(Opening opening)
        {
            ResponseServiceModel response = new();
            try
            {
                Roulette roulette = this.RouletteDac.GetRoulette(opening.IdRoulette);
                if (roulette.IdRoulette == 0)
                    throw new Exception($"No se pudo encontrar de la Ruleta {opening.IdRoulette}, por favor verifique el id de la Ruleta");
                if (!(bool)roulette.StateRoulette)
                    throw new Exception($"La Ruleta {roulette.IdRoulette} se encuentra inactiva, por favor seleccione otra Ruleta");

                bool rpta = this.OpeningDac.OpenOpening(opening);
                if (rpta)
                {
                    response.Success = true;
                    response.Message = $"Se creó la apertura {opening.IdOpening} de la Ruleta {opening.IdRoulette} éxito";
                    response.Count = 1;
                    response.Data = opening;
                }
                else
                    throw new Exception($"No se pudo crear la apertura de la Ruleta {opening.IdRoulette}, por favor intente más tarde");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Count = 0;
                response.Data = null;
            }
            return response;
        }

        public ResponseServiceModel GetOpening(long idOpening)
        {
            ResponseServiceModel response = new();
            try
            {
                Opening rpta = this.OpeningDac.GetOpening(idOpening);
                if (rpta.IdOpening != 0)
                {
                    response.Success = true;
                    response.Message = "Se obtuvó la apertura de la Ruleta con éxito";
                    response.Count = 1;
                    response.Data = rpta;
                }
                else
                    throw new Exception("No se pudo obtener la apertura de la Ruleta por id, por favor verifique el Id de la apertura");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Count = 0;
                response.Data = null;
            }
            return response;
        }

        public ResponseServiceModel CloseOpening(Opening opening)
        {
            ResponseServiceModel response = new();
            try
            {
                Opening ExistsOpening = this.OpeningDac.GetOpening(opening.IdOpening);
                if (ExistsOpening.IdOpening == 0)
                    throw new Exception("No se encontró la apertura de la Ruleta por id, por favor verifique el Id de la apertura de Ruleta a cerrar");
                if (!ExistsOpening.EnableOpening && ExistsOpening.DateStartOpening == null)
                    throw new Exception("La apertura de la Ruleta no se encuentra abierta");
                if (ExistsOpening.DateStartOpening != null && ExistsOpening.ColorOpening != null && !string.IsNullOrEmpty(ExistsOpening.ColorOpening))
                    throw new Exception("La apertura de la Ruleta ya se encuentra cerrada");
                opening.NumberOpening = ObtenerNumberRulette();
                opening.ColorOpening = ObtenerColorRulette();
                Opening finishOpening = this.OpeningDac.CloseOpening(opening);

                if (finishOpening.IdOpening != 0)
                {
                    JObject ResultRound = new();
                    JObject listPlayers = UpdateWinners(finishOpening);
                    ResultRound.Add(new JProperty("Result", JObject.Parse(JsonConvert.SerializeObject(finishOpening))));
                    ResultRound.Add(new JProperty("Players", listPlayers));
                    response.Success = true;
                    response.Message = "Se realizó el cierre de las apuestas en la Ruleta con éxito";
                    response.Count = listPlayers.Count;
                    response.Data = ResultRound;
                }
                else
                    throw new Exception("No se pudo cerrar la apertura de la Ruleta, por favor verifique los datos de la apertura a cerrar");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Count = 0;
                response.Data = null;
            }
            return response;
        }

        private JObject UpdateWinners(Opening setOpenings)
        {
            DataSet players = this.BetDac.GetBetsIdOpening(setOpenings.IdOpening);
            if (players != null && players.Tables.Count > 0 && players.Tables[0].Rows.Count > 0)
            {
                JObject winnersRound = new();
                foreach (DataRow winner in players.Tables[0].Rows)
                {
                    JObject winningUser = new();
                    long idWinningUser = (long)winner["IdUser"];
                    decimal valueBet = (decimal)winner["ValueBet"];
                    decimal earnedValue = 0;
                    string typeWin = "";
                    if (!Convert.IsDBNull(winner["NumberBet"]) && (Int16?)winner["NumberBet"] == (Int16?)setOpenings.NumberOpening)
                    {
                        typeWin = "NumberBet";
                        earnedValue = CalculateMoneyWin(valueBet, true);
                        UserDac.ModifyUser(new User(idWinningUser, earnedValue + valueBet));
                    }
                    else if (winner["ColorBet"] != null && Convert.ToString(winner["ColorBet"]) == setOpenings.ColorOpening)
                    {
                        typeWin = "ColorBet";
                        earnedValue = CalculateMoneyWin(valueBet, false);
                        UserDac.ModifyUser(new User(idWinningUser, earnedValue + valueBet));
                    }
                    else
                        earnedValue = 0;
                    winningUser.Add(new JProperty("IdUser", idWinningUser));
                    winningUser.Add(new JProperty("BetValue", valueBet));
                    winningUser.Add(new JProperty("EarnedValue", earnedValue));
                    winningUser.Add(new JProperty("TypeWin", typeWin));
                    winnersRound.Add(new JProperty(winner["IdBet"].ToString(), winningUser));
                }
                return winnersRound;
            }
            else
                return new JObject();
        }

        private decimal CalculateMoneyWin(decimal valueBet, bool isNumber)
        {
            if (isNumber)
                return valueBet * 5;
            return (valueBet * 18)/10;
        }

        private string ObtenerColorRulette()
        {
            Random random = new Random();
            int ranNum = random.Next(0, 9);
            if (ranNum % 2 == 0)
                return "R";
            else
                return "B";
        }

        private int ObtenerNumberRulette()
        {
            Random random = new Random();
            int ranNum = random.Next(0, 36);
            return ranNum;
        }
        #endregion Methods
    }
}
