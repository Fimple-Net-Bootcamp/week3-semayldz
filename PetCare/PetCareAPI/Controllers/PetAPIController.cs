using PetCareAPI.Data;
using PetCareAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareAPI.Data;

namespace PetCareAPI.Controllers
{
    //[Route("api/[controller]")]
    
    [Route("evcilHayvanlar")]
    [ApiController]
    public class PetAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PetAPIController(ApplicationDbContext db)
        {
            _db = db;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Pet>> GetPets()
        {
            var pets = _db.Pet
                        .Include(p => p.Activities)
                        .Include(p => p.Foods)
                        .Include(p => p.User)
                        .Include(p => p.HealthStatus)
                        .ToList();

            // User ve HealthStatus için gerekli bilgileri doldur
            var result = pets.Select(pet => new
            {
                pet.PetId,
                pet.UserId,
                user = pet.User != null ? $"{pet.User.FirstName} {pet.User.LastName}" : null,
                pet.HealthStatusId,
                healthStatus = pet.HealthStatus?.Status,
                pet.Type,
                pet.Name,
                activities = pet.Activities.Select(activity => new
                {
                    activity.ActivityName
                }).ToList(),
                foods = pet.Foods.Select(food => new
                {
                    food.FoodName
                }).ToList()
            });

            return Ok(result);

        }



        
        [HttpGet("{id:int}", Name = "GetPet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Pet> GetPet(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var pets = _db.Pet.Where(p => p.PetId == id)
                        .Include(p => p.Activities)
                        .Include(p => p.Foods)
                        .Include(p => p.User)
                        .Include(p => p.HealthStatus)
                        .ToList();

            // User ve HealthStatus için gerekli bilgileri doldur
            var result = pets.Select(pet => new
            {
                pet.PetId,
                pet.UserId,
                user = pet.User != null ? $"{pet.User.FirstName} {pet.User.LastName}" : null,
                pet.HealthStatusId,
                healthStatus = pet.HealthStatus?.Status,
                pet.Type,
                pet.Name,
                activities = pet.Activities.Select(activity => new
                {
                    activity.ActivityName
                }).ToList(),
                foods = pet.Foods.Select(food => new
                {
                    food.FoodName
                }).ToList()
            });

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<Pet> CreatePet([FromBody] Pet pet)
        {
            if (_db.Pet.FirstOrDefault(u => u.Name.ToLower() == pet.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Pet already exists!");
                return BadRequest(ModelState);
            }
            if (pet == null)
            {
                return BadRequest(pet);
            }


            // Activities ve Foods listelerini ekleyip eklememe kontrolü
            if (pet.Activities == null)
                pet.Activities = new List<Activity>();

            if (pet.Foods == null)
                pet.Foods = new List<Food>();

            if (pet.User == null)
                pet.User = new User();

            if (pet.HealthStatus == null)
                pet.HealthStatus = new HealthStatus();
            
            return CreatedAtRoute("GetPets", new { id = pet.PetId }, pet);
        }



        [HttpPut("{id:int}", Name = "UpdatePet")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateVilla(int id, [FromBody] Pet pet)
        {
            if (pet == null || id != pet.PetId)
            {
                return BadRequest();
            }

            _db.Pet.Update(pet);
            _db.SaveChanges();
            return NoContent();
        }



        //Patch için paketler Microsoft.AspNetCore.JsonPatch,Microsoft.AspNetCore.Mvc.NewtonsoftJson,
        [HttpPatch("{id:int}", Name = "UpdatePartialPet")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialPet(int id, JsonPatchDocument<Pet> patchPet)
        {
            if (patchPet == null || id == 0)
            {
                return BadRequest();
            }

            var pet = _db.Pet.FirstOrDefault(u => u.PetId == id);
            if (pet == null)
            {
                return BadRequest();
            }
            patchPet.ApplyTo(pet, ModelState);
            _db.Pet.Update(pet);
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

    }
}
