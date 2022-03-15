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
    public class RouletteController : Controller
    {
        #region Constructor - Interfaces
        private readonly ILogger<RouletteController> _logger;
        public IRouletteService RouletteService { get; set; }
        public RouletteController(ILogger<RouletteController> logger, IRouletteService RouletteService)
        {
            _logger = logger;
            this.RouletteService = RouletteService;
        }
        #endregion Constructor - Interfaces

        #region Methods
        [HttpPost]
        public IActionResult CreateRoulette(JObject rouletteJson)
        {
            try
            {
                if (rouletteJson == null && !rouletteJson.HasValues)
                    throw new Exception("Información de la Ruleta a crear esta vacía");

                Roulette roulette = rouletteJson.ToObject<Roulette>();
                if (string.IsNullOrWhiteSpace(roulette.TitleRoulette))
                    throw new Exception("Se debe ingresar parámetro TitleRoulette con el titulo de la Ruleta a crear");
                
                ResponseServiceModel response = this.RouletteService.CreateRoulette(roulette);
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
        public IActionResult GetRoulette(JObject rouletteJson)
        {
            try
            {
                if (rouletteJson == null && !rouletteJson.HasValues)
                    throw new Exception("Información de la Ruleta a consultar esta vacía");

                Roulette roulette = rouletteJson.ToObject<Roulette>();

                if (roulette.IdRoulette < 1)
                    throw new Exception("Id de la Ruleta inválido para realizar la consulta");

                ResponseServiceModel response = this.RouletteService.GetRoulette(roulette.IdRoulette);
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
        public IActionResult ModifyRoulette(JObject rouletteJson)
        {
            try
            {
                if (rouletteJson == null && !rouletteJson.HasValues)
                    throw new Exception("Información de la Ruleta a modificar esta vacía");
                if (rouletteJson["TitleRoulette"] == null && rouletteJson["StateRoulette"] == null)
                    throw new Exception("Se debe ingresar algún parámetro de la Ruleta a modificar");

                Roulette roulette = rouletteJson.ToObject<Roulette>();
                if (roulette.IdRoulette < 1)
                    throw new Exception("Id de la Ruleta inválido para la modificación");

                if (rouletteJson["TitleRoulette"] == null)
                    roulette.TitleRoulette = null;
                if (rouletteJson["StateRoulette"] == null)
                    roulette.StateRoulette = null;
                ResponseServiceModel response = this.RouletteService.ModifyRoulette(roulette);
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
        public IActionResult GetAllRoulettes()
        {
            try
            {
                ResponseServiceModel response = this.RouletteService.GetAllRoulettes();
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
