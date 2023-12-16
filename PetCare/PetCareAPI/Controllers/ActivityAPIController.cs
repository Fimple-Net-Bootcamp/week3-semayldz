using PetCareAPI.Data;
using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Models;

namespace PetCareAPI.Controllers
{
    [Route("aktiviteler")]
    [ApiController]
    public class ActivityAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ActivityAPIController(ApplicationDbContext db)
        {
            _db = db;
        }



        [HttpGet("{petId:int}", Name = "GetActivity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Activity> GetActivity(int petId)
        {
            if (petId == 0)
            {
                return BadRequest();
            }

            var petActivity = _db.Activity.Where(u => u.PetId == petId).ToList();

            if (petActivity == null || !petActivity.Any())
            {
                return NotFound();
            }

            return Ok(petActivity);
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public ActionResult<Activity> CreateActivity([FromBody] Activity activity)
        {
            if (_db.Activity.FirstOrDefault(u => u.ActivityName.ToLower() == activity.ActivityName.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Activity already exists!");
                return BadRequest(ModelState);
            }
            if (activity == null)
            {
                return BadRequest(activity);
            }

            _db.Activity.Add(activity);
            _db.SaveChanges();

            return CreatedAtRoute("GetActivity", new { id = activity.ActivityId }, activity);
        }
    }
}
