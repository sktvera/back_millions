using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Million.Application.DTOs;
using Million.Application.Services;
using Million.Domain.Entities;
using System.Linq; // <- necesario por .Select()

namespace Million.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyService _propertyService;
        private readonly ILogger<PropertiesController> _logger;

        public PropertiesController(PropertyService propertyService, ILogger<PropertiesController> logger)
        {
            _propertyService = propertyService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las propiedades (con filtros opcionales).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? name,
            [FromQuery] string? address,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var items = await _propertyService.GetAllAsync(name, address, minPrice, maxPrice);
            var result = items.Select(MapToReadDto);
            return Ok(result);
        }

        /// <summary>
        /// Obtiene una propiedad por su ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest(new { message = "El ID debe ser un ObjectId válido de 24 caracteres." });

            var property = await _propertyService.GetByIdAsync(id);
            if (property == null)
                return NotFound(new { message = $"No se encontró la propiedad con ID {id}" });

            return Ok(MapToReadDto(property));
        }

        /// <summary>
        /// Crea una nueva propiedad.
        /// </summary>

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _propertyService.CreateAsync(dto);
            return NoContent();
        }


        /// <summary>
        /// Actualiza una propiedad existente (parcial o total).
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePropertyDto dto)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest(new { message = "El ID debe ser un ObjectId válido de 24 caracteres." });

            try
            {
                await _propertyService.UpdateAsync(id, dto);
                _logger.LogInformation("Propiedad actualizada: {Id}", id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "No existe la propiedad con ID {Id}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la propiedad con ID {Id}", id);
                return Problem(title: "Error al actualizar", detail: ex.Message, statusCode: 500);
            }
        }

        /// <summary>
        /// Elimina una propiedad.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest(new { message = "El ID debe ser un ObjectId válido de 24 caracteres." });

            try
            {
                await _propertyService.DeleteAsync(id);
                _logger.LogInformation("Propiedad eliminada: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la propiedad con ID {Id}", id);
                return NotFound(new { message = ex.Message });
            }
        }

        // ----------------------
        // Mapeo de salida (DTO lectura)
        // ----------------------
       private static object MapToReadDto(Property p)
        {
            string? finalUrl = p.ImageUrl;
            if (string.IsNullOrWhiteSpace(finalUrl) && !string.IsNullOrWhiteSpace(p.Image))
                finalUrl = $"data:image/jpeg;base64,{p.Image}";

            return new {
                id = p.Id,
                idOwner = p.IdOwner,
                name = p.Name,
                address = p.Address,
                price = p.Price,
                imageUrl = finalUrl,
                imageKey = p.ImageKey
            };
        }

    }
}
