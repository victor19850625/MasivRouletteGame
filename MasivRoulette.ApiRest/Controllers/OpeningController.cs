using System;
using MasivRoulette.Entities.BindingModels;
using MasivRoulette.Entities.Models;
using MasivRoulette.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MasivRoulette.ApiRest.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OpeningController : Controller
    {
        #region Constructor - Interfaces
        private readonly ILogger<OpeningController> _logger;
        public IOpeningService OpeningService { get; set; }
        public OpeningController(ILogger<OpeningController> logger, IOpeningService OpeningService)
        {
            _logger = logger;
            this.OpeningService = OpeningService;
        }
        #endregion Constructor - Interfaces

        #region Methods
        [HttpPost]
        public IActionResult OpenOpening()
        {
            try
            {
                Request.Headers.TryGetValue("IdRoulette", out var headerValue);
                if (headerValue.Count == 0)
                    throw new Exception("No se encontró el IdRoulette en el header, debe ser ingresado con el número de Ruleta para crear la apertura");
                Opening opening = new() {
                    IdRoulette = Convert.ToInt64(headerValue[0])
                };
                if (opening.IdRoulette < 1)
                    throw new Exception("Id de la Ruleta invalido, no se puede crear la apertura");
                ResponseServiceModel response = this.OpeningService.OpenOpening(opening);
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
        public IActionResult GetOpening(JObject openingJson)
        {
            try
            {
                if (openingJson == null && !openingJson.HasValues)
                    throw new Exception("Información de la Apertura de la Ruleta a consultar esta vacía");

                Opening opening = openingJson.ToObject<Opening>();

                if(opening.IdOpening < 1)
                    throw new Exception("Idde la Apertura de la Ruleta inválido para realizar la consulta ");

                ResponseServiceModel response = this.OpeningService.GetOpening(opening.IdOpening);
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
        public IActionResult CloseOpening(JObject openingJson)
        {
            try
            {
                if (openingJson == null && !openingJson.HasValues)
                    throw new Exception("Información de la Apertura de la Ruleta a cerrar esta vacía");

                Opening opening = openingJson.ToObject<Opening>();

                if (opening.IdOpening < 1)
                    throw new Exception("Id de la Apertura de la Ruleta a cerrar inválido de consulta");

                ResponseServiceModel response = this.OpeningService.CloseOpening(opening);
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
