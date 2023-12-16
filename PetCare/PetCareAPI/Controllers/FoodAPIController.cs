using PetCareAPI.Data;
using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PetCareAPI.Controllers
{
    [Route("besinler")]
    [ApiController]
    public class FoodAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public FoodAPIController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Food>> GetFoods()
        {
            return Ok(_db.Food.ToList());

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<Food> CreateFood([FromBody] Food food, int petId)
        {
            var pet = _db.Pet.FirstOrDefault(u => u.PetId == petId);

            if (pet == null)
            {
                return NotFound("Evcil hayvan bulunamadı");
            }
            
            food.PetId = petId;
            _db.Food.Add(food);
            _db.SaveChanges();

            return Ok($"Evcil hayvana {food.FoodName} verildi.");
        }
    }
}
