using System;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.WebAPI.DTO;
using LibraryService.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FraudsController : ControllerBase
    {
        private readonly IFraudService _fraudService;

        public FraudsController(IFraudService fraudService)
        {
            _fraudService = fraudService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var frauds = await _fraudService.GetAllAsync();
            return Ok(frauds);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FraudForm dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Los datos del reporte no son validos. Revise los campos obligatorios.",
                    errors = ModelState
                        .Where(entry => entry.Value?.Errors.Count > 0)
                        .ToDictionary(
                            entry => entry.Key,
                            entry => entry.Value!.Errors.Select(error => error.ErrorMessage).ToArray())
                });
            }

            try
            {
                var fraud = await _fraudService.CreateAsync(dto);

                return Created($"/api/frauds/{fraud.Id}", fraud);
            }
            catch (ArgumentNullException)
            {
                return BadRequest(new
                {
                    message = "Debe enviar los datos del reporte en el cuerpo de la solicitud."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = MapValidationMessage(ex.ParamName),
                    field = ex.ParamName
                });
            }
        }

        private static string MapValidationMessage(string? fieldName)
        {
            return fieldName switch
            {
                "ImpostorDetails" => "Los detalles del impostor son obligatorios.",
                "ContactInfo" => "La informacion de contacto es obligatoria.",
                "Comments" => "La descripcion del caso es obligatoria.",
                _ => "Los datos del reporte no son validos."
            };
        }
    }
}
