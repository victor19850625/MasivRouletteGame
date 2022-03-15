using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;
using MasivRoulette.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;

namespace MasivRoulette.ApiRest.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BetController : Controller
    {
        #region Constructor - Interfaces
        private readonly ILogger<BetController> _logger;
        public IBetService BetService { get; set; }
        public BetController(ILogger<BetController> logger, IBetService BetService)
        {
            _logger = logger;
            this.BetService = BetService;
        }
        #endregion Constructor - Interfaces

        #region Methods
        [HttpPost]
        public IActionResult CreateBet(JObject betJson)
        {
            try
            {
                Request.Headers.TryGetValue("IdOpening", out var headerOpening);
                Request.Headers.TryGetValue("IdUser", out var headerUser);
                if (headerOpening.Count == 0)
                    throw new Exception("No se encontró el IdOpening en el header, debe ser ingresado con el número de la apertura de la Ruleta");
                long idOpening = Convert.ToInt64(headerOpening[0]);
                if (idOpening < 1)
                    throw new Exception("Id de la apertura de Ruleta invalido, no se puede crear la apuesta");
                
                if (headerUser.Count == 0)
                    throw new Exception("No se encontró el IdUser en el header, debe ser ingresado con el id del usuario que realiza la apuesta");
                long idUser = Convert.ToInt64(headerUser[0]);
                if (idUser < 1)
                    throw new Exception("Id del Usuario invalido, no se puede crear la apuesta");
                
                if (betJson == null && !betJson.HasValues)
                    throw new Exception("Información de la Apuesta a crear esta vacía");

                Bet bet = betJson.ToObject<Bet>();
                if (betJson["ValueBet"] == null)
                    throw new Exception("Se debe ingresar parámetro ValueBet valido entre 0 y 1000 de la apuesta");

                if (betJson["ColorBet"] == null && betJson["NumberBet"] == null)
                    throw new Exception("Se debe ingresar parámetro NumberBet valido entre 0 y 36 o el Color de apuesta");
                else
                {
                    if (betJson["NumberBet"] != null && bet.NumberBet < 0 || bet.NumberBet > 36)
                        throw new Exception("Se debe ingresar parámetro NumberBet valido entre 0 y 36");
                    else if (betJson["ColorBet"] != null && bet.ColorBet != "R" && bet.ColorBet != "B")
                        throw new Exception("Se debe ingresar parámetro ColorBet valido, las opciones validas son 'R' (Rojo) y 'B' (Negro)");
                }
                if (betJson["NumberBet"] == null)
                    bet.NumberBet = null;

                bet.IdUser = idUser;
                bet.IdOpening = idOpening;
                ResponseServiceModel response = this.BetService.CreateBet(bet);
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
        public IActionResult GetBet(JObject betJson)
        {
            try
            {
                if (betJson == null && !betJson.HasValues)
                    throw new Exception("Información de la apuesta a consultar esta vacía");

                Bet bet = betJson.ToObject<Bet>();

                if (bet.IdBet < 1)
                    throw new Exception("Id de la apuesta inválido para realizar la consulta");

                ResponseServiceModel response = this.BetService.GetBet(bet.IdBet);
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
