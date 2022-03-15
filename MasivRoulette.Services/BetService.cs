using MasivRoulette.DataAccess.Interfaces;
using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;
using MasivRoulette.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasivRoulette.Services
{
    public class BetService : IBetService
    {
        #region Constructor - Interfaces
        public IBetDac BetDac { get; set; }
        public IOpeningDac OpeningDac { get; set; }
        public IUserDac UserDac { get; set; }
        public BetService(IBetDac BetDac, IOpeningDac OpeningDac, IUserDac UserDac)
        {
            this.BetDac = BetDac;
            this.OpeningDac = OpeningDac;
            this.UserDac = UserDac;
        }
        #endregion Constructor - Interfaces

        #region Methods
        public ResponseServiceModel CreateBet(Bet bet)
        {
            ResponseServiceModel response = new();
            try
            {
                Opening ExistsOpening = this.OpeningDac.GetOpening(bet.IdOpening);
                if (ExistsOpening.IdOpening == 0)
                    throw new Exception("No se encontró la apertura de la Ruleta por id, por favor verifique el Id de la apertura de Ruleta para apostar");
                if (!ExistsOpening.EnableOpening && ExistsOpening.DateStartOpening == null)
                    throw new Exception("La apertura de la Ruleta no se encuentra abierta, no es posible hacer la apuesta");
                if (ExistsOpening.DateStartOpening != null && ExistsOpening.ColorOpening != null && !string.IsNullOrEmpty(ExistsOpening.ColorOpening))
                    throw new Exception("La apertura de la Ruleta ya se encuentra cerrada, no es posible hacer la apuesta");

                User ExistsUser = this.UserDac.GetUser(bet.IdUser);
                if (ExistsUser.IdUser == 0)
                    throw new Exception("No se encontró el Usuario por id, por favor verifique el Id del Usuario para apostar");
                if (!(bool)ExistsUser.StateUser)
                    throw new Exception("El usuario no se encuentra activo, no es posible hacer la apuesta");
                if (ExistsUser.CreditUser < bet.ValueBet)
                    throw new Exception("El usuario no cuenta con el credito de la apuesta, por favor cambiar el valor apostado");

                if (bet.ValueBet < 1 || bet.ValueBet > 10000)
                    throw new Exception("El valor de la apuesta no está entre los límites, mínima 1 y máxima 10.000");
                
                bool rpta = this.BetDac.CreateBet(bet);
                if (rpta)
                {
                    this.UserDac.ModifyUser(new User(bet.IdUser, (decimal)(ExistsUser.CreditUser - bet.ValueBet)));
                    response.Success = true;
                    response.Message = "Se creó la Apuesta con éxito";
                    response.Count = 1;
                    response.Data = bet;
                }
                else
                    throw new Exception("No se pudo crear la Apuesta, por favor intente más tarde");
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

        public ResponseServiceModel GetBet(long idBet)
        {
            ResponseServiceModel response = new();
            try
            {
                Bet rpta = this.BetDac.GetBet(idBet);
                if (rpta.IdBet != 0)
                {
                    response.Success = true;
                    response.Message = "Se obtuvó la Apuesta con éxito";
                    response.Count = 1;
                    response.Data = rpta;
                }
                else
                    throw new Exception("No se pudo obtener la Apuesta por id");
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

        public ResponseServiceModel GetBetIdOpening(long idOpening)
        {
            ResponseServiceModel response = new();
            try
            {
                DataSet ds = this.BetDac.GetBetsIdOpening(idOpening);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.Success = true;
                    response.Message = "Se obtuvo apuestas de la apertura de la Ruleta con éxito";
                    response.Count = ds.Tables[0].Rows.Count;
                    response.Data = ds;
                }
                else if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                {
                    response.Success = true;
                    response.Message = "No se obtuvo apuestas de la apertura de la Ruleta consultada";
                    response.Count = 0;
                    response.Data = ds;
                }
                else
                    throw new Exception("No se pudo obtener información de la apertura de la Ruleta ingresada");
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

        public ResponseServiceModel GetWinnersBet(Bet bet)
        {
            ResponseServiceModel response = new();
            try
            {
                //Modificar ******************************
                DataSet ds = this.BetDac.GetBetsIdOpening(bet.IdOpening);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.Success = true;
                    response.Message = "Se obtuvo apuestas de la apertura de la Ruleta con éxito";
                    response.Count = ds.Tables[0].Rows.Count;
                    response.Data = ds;
                }
                else if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                {
                    response.Success = true;
                    response.Message = "No se obtuvo apuestas de la apertura de la Ruleta consultada";
                    response.Count = 0;
                    response.Data = ds;
                }
                else
                    throw new Exception("No se pudo obtener información de la apertura de la Ruleta ingresada");
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
        #endregion Methods
    }
}
