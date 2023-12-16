using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareAPI.Data;
using PetCareAPI.Models;

namespace PetCareAPI.Controllers
{
    //[Route("api/[controller]")]

    [Route("kullanicilar")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public UserAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var user = _db.User.Include(u => u.Pet)
                       .ThenInclude(p => p.Activities)
                       .Include(u => u.Pet)
                       .ThenInclude(p => p.Foods).FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if (_db.User.FirstOrDefault(u => u.FirstName.ToLower() == user.FirstName.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Pet already exists!");
                return BadRequest(ModelState);
            }
            if (user == null)
            {
                return BadRequest(user);
            }

            _db.User.Add(user);
            _db.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = user.UserId }, user);
        }
    }
}
