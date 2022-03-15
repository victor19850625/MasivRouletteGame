using MasivRoulette.DataAccess.Interfaces;
using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;
using MasivRoulette.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace MasivRoulette.Services
{
    public class RouletteService : IRouletteService
    {
        #region Constructor - Interfaces
        public IRouletteDac RouletteDac { get; set; }
        public RouletteService(IRouletteDac RouletteDac)
        {
            this.RouletteDac = RouletteDac;
        }
        #endregion Constructor - Interfaces

        #region Methods
        public ResponseServiceModel CreateRoulette(Roulette roulette)
        {
            ResponseServiceModel response = new();
            try
            {
                bool rpta = this.RouletteDac.CreateRoulette(roulette);
                if (rpta)
                {
                    response.Success = true;
                    response.Message = "Se creó la Ruleta con éxito";
                    response.Count = 1;
                    response.Data = roulette;
                }
                else
                    throw new Exception("No se pudo crear la Ruleta, por favor intente más tarde");
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

        public ResponseServiceModel GetRoulette(long idRoulette)
        {
            ResponseServiceModel response = new();
            try
            {
                Roulette rpta = this.RouletteDac.GetRoulette(idRoulette);
                if (rpta.IdRoulette != 0)
                {
                    response.Success = true;
                    response.Message = "Se obtuvó la Ruleta con éxito";
                    response.Count = 1;
                    response.Data = rpta;
                }
                else
                    throw new Exception("No se pudo obtener la Ruleta por id, por favor verifique el Id de la Ruleta");
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

        public ResponseServiceModel ModifyRoulette(Roulette roulette)
        {
            ResponseServiceModel response = new();
            try
            {
                Roulette rpta = this.RouletteDac.ModifyRoulette(roulette);
                if (rpta.IdRoulette != 0)
                {
                    response.Success = true;
                    response.Message = "Se modificó la Ruleta con éxito";
                    response.Count = 1;
                    response.Data = rpta;
                }
                else
                    throw new Exception("No se pudo modificar la Ruleta, por favor verifique los datos de la Ruleta a modificar");
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

        public ResponseServiceModel GetAllRoulettes()
        {
            int countRoulettes = 0;
            ResponseServiceModel response = new();
            try
            {
                JArray roulettesAll = new();
                DataSet roulettes = this.RouletteDac.GetAllRoulettes(out countRoulettes);
                if (countRoulettes > 0 && roulettes.Tables.Count > 0 && roulettes.Tables[0].Rows.Count > 0)
                {
                    List<string> columns = new();
                    foreach (DataColumn column in roulettes.Tables[0].Columns)
                    {
                        columns.Add(column.ColumnName);
                    }
                    foreach (DataRow item in roulettes.Tables[0].Rows)
                    {
                        JObject roulettedb = new();
                        foreach (string col in columns)
                        {
                            roulettedb.Add(new JProperty(col, item[col]));
                        }
                        roulettesAll.Add(roulettedb);
                    }
                    response.Success = true;
                    response.Message = "Se obtuvó la lista de Ruletas con éxito";
                    response.Count = countRoulettes;
                    response.Data = roulettesAll;
                }
                else
                    throw new Exception("No se encontraron registros de las Ruletas");
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
