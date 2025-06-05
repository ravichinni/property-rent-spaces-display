using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropertyRentSpaces.Core.Models;
using PropertyRentSpaces.Infrastructure.Services;

namespace PropertyRentSpaces.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IAzureBlobStorageService _blobStorageService;

        public PropertiesController(IAzureBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpGet]
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