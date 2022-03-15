using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;
using MasivRoulette.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MasivRoulette.ApiRest.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        #region Constructor - Interfaces
        private readonly ILogger<UserController> _logger;
        public IUserService UserService { get; set; }
        public UserController(ILogger<UserController> logger, IUserService UserService)
        {
            _logger = logger;
            this.UserService = UserService;
        }
        #endregion Constructor - Interfaces

        #region Methods
        [HttpPost]
        public IActionResult CreateUser(JObject userJson)
        {
            try
            {
                if (userJson == null && !userJson.HasValues)
                    throw new Exception("Información del Usuario a crear esta vacía");

                User user = userJson.ToObject<User>(); 
                if (string.IsNullOrWhiteSpace(user.NameUser))
                    throw new Exception("Se debe ingresar parámetro NameUser con el nombre del usuario a crear");
                if (string.IsNullOrWhiteSpace(user.EmailUser))
                    throw new Exception("Se debe ingresar parámetro EmailUser con el correo del usuario a crear");

                ResponseServiceModel response = this.UserService.CreateUser(user);
                if (response.Success)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseServiceModel() {
                    Count = 0,
                    Message = ex.Message,
                    Data = null,
                    Success = false
                });
            }
        }

        [HttpGet]
        public IActionResult GetUser(JObject userJson)
        {
            try
            {
                if (userJson == null && !userJson.HasValues)
                    throw new Exception("Información del Usuario a consultar esta vacía");

                User user = userJson.ToObject<User>();

                if (user.IdUser < 1)
                    throw new Exception("Id de Usuario inválido para realizar la consulta");

                ResponseServiceModel response = this.UserService.GetUser(user.IdUser);
                if (response.Success)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseServiceModel() {
                    Count = 0,
                    Message = ex.Message,
                    Data = null,
                    Success = false
                });
            }
        }

        [HttpPut]
        public IActionResult ModifyUser(JObject userJson)
        {
            try
            {
                if (userJson == null && !userJson.HasValues)
                    throw new Exception("Información de Usuario a modificar esta vacía");
                if (userJson["NameUser"] == null && userJson["EmailUser"] == null && userJson["CreditUser"] == null && userJson["StateUser"] == null)
                    throw new Exception("Se debe ingresar algún parámetro del Usuario a modificar");

                User user = userJson.ToObject<User>();
                if (user.IdUser < 1)
                    throw new Exception("Id de Usuario inválido para la modificación");
                if (userJson["NameUser"] == null)
                    user.NameUser = null;
                if (userJson["EmailUser"] == null)
                    user.EmailUser = null;
                if (userJson["CreditUser"] == null)
                    user.CreditUser = null;
                if (userJson["StateUser"] == null)
                    user.StateUser = null;

                ResponseServiceModel response = this.UserService.ModifyUser(user);
                if (response.Success)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseServiceModel() {
                    Count = 0,
                    Message = ex.Message,
                    Data = null,
                    Success = false
                });
            }
        }
        #endregion Methods
    }
}
