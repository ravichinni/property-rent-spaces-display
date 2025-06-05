using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropertyRentSpaces.Core.Models;
using PropertyRentSpaces.Infrastructure.Services;

namespace PropertyRentSpaces.API.Controllers
{
    /// <summary>
    /// Controller for managing property and space rental information
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IAzureBlobStorageService _blobStorageService;

        public PropertiesController(IAzureBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        /// <summary>
        /// Retrieves all properties and their associated spaces
        /// </summary>
        /// <returns>A list of properties with their details</returns>
        /// <response code="200">Returns the list of properties</response>
        /// <response code="500">If there was an error retrieving the properties</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Property>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
        {
            try
            {
                var properties = await _blobStorageService.GetPropertiesAsync();
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error retrieving properties", message = ex.Message });
            }
        }
    }
} 