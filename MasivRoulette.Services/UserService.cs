using MasivRoulette.DataAccess.Interfaces;
using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;
using MasivRoulette.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MasivRoulette.Services
{
    public class UserService : IUserService
    {
        #region Constructor - Interfaces
        public IUserDac UserDac { get; set; }
        public UserService(IUserDac UserDac)
        {
            this.UserDac = UserDac;
        }
        #endregion Constructor - Interfaces

        #region Methods
        public ResponseServiceModel CreateUser(User user)
        {
            ResponseServiceModel response = new();
            try
            {
                bool rpta = this.UserDac.CreateUser(user);
                if (rpta)
                {
                    response.Success = true;
                    response.Message = "Se creó el Usuario con éxito";
                    response.Count = 1;
                    response.Data = (object)user;
                }
                else
                    throw new Exception("No se pudo crear el Usuario, por favor intente más tarde");
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

        public ResponseServiceModel GetUser(long idUser)
        {
            ResponseServiceModel response = new();
            try
            {
                User rpta = this.UserDac.GetUser(idUser);
                if (rpta.IdUser != 0)
                {
                    response.Success = true;
                    response.Message = "Se obtuvó el Usuario con éxito";
                    response.Count = 1;
                    response.Data = rpta;
                }
                else
                    throw new Exception("No se pudo obtener el Usuario por id, por favor verifique el Id del Usuario");
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

        public ResponseServiceModel ModifyUser(User user)
        {
            ResponseServiceModel response = new();
            try
            {
                User rpta = this.UserDac.ModifyUser(user);
                if (rpta.IdUser != 0)
                {
                    response.Success = true;
                    response.Message = "Se modificó el Usuario con éxito";
                    response.Count = 1;
                    response.Data = rpta;
                }
                else
                    throw new Exception("No se pudo modificar el Usuario, por favor verifique los datos del Usuario a modificar");
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
