using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {

        private readonly FoodDbContext _context;
        public FoodController(FoodDbContext context) => _context = context;

        // GET: api/<FoodController>
        [HttpGet]
        public async Task<IEnumerable<FoodModels>> Get()
        {
            return await _context.Foods.ToListAsync();
            //return _db.Foods.ToList(); 
        }

        // GET api/<FoodController>/5
       [HttpGet("{id}")]
       [ProducesResponseType(typeof(FoodModels), StatusCodes.Status200OK)]
       [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Foods.FindAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        // POST api/<FoodController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(FoodModels Food)
        {
            await _context.Foods.AddAsync(Food);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id = Food.Id}, Food);
        }

        // PUT api/<FoodController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id,FoodModels Food)
        {
            if (id !=Food.Id) return BadRequest();
            _context.Entry(Food).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<FoodController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Foods.FindAsync(id);
            if (result == null) return NotFound();
            _context.Foods.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

