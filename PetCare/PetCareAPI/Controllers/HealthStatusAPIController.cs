using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Data;
using PetCareAPI.Models;

namespace PetCareAPI.Controllers
{
    //[Route("api/[controller]")]

    [Route("saglikDurumlari")]
    [ApiController]
    public class HealthStatusAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public HealthStatusAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        
        
        [HttpGet("{petId:int}", Name = "GetHealthStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HealthStatus> GetHealthStatus(int petId)
        {
            if (petId == 0)
            {
                return BadRequest();
            }

            var petHealthStatus = _db.HealthStatus.Where(u => u.PetId == petId).ToList();

            if (petHealthStatus == null || !petHealthStatus.Any())
            {
                return NotFound();
            }

            return Ok(petHealthStatus);
        }

        
        [HttpPatch("{id:int}", Name = "UpdatePartialHealthStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialHealthStatus(int id, JsonPatchDocument<HealthStatus> patchHealthStatus)
        {
            if (patchHealthStatus == null || id == 0)
            {
                return BadRequest();
            }

            var healthstatus = _db.HealthStatus.FirstOrDefault(u => u.PetId == id);
            if (healthstatus == null)
            {
                return BadRequest();
            }
            patchHealthStatus.ApplyTo(healthstatus, ModelState);
            _db.HealthStatus.Update(healthstatus);
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }


    }
}
