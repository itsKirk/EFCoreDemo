using AutoMapper;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkingWithEFCore.DataContext;

namespace WorkingWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PeopleController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _db.People.ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode(500,
                    "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            try
            {
                if (person == null) return BadRequest();
                _db.People.Add(person);
                await _db.SaveChangesAsync();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Person person)
        {
            try
            {
                var personToUpdate = await _db.People.FirstOrDefaultAsync(_ => _.Id == id);
                if (personToUpdate != null)
                {
                    personToUpdate = _mapper.Map(person, personToUpdate);
                    _db.Attach(personToUpdate).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return NotFound("we couldn't find details of the specified person");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var person = await _db.People.FirstOrDefaultAsync(x => x.Id == id);
                if (person == null)
                {
                    return NotFound("requested person not found");
                }

                return Ok(person);
            }
            catch (Exception)
            {
                return StatusCode(500,
                    "Error retrieving data from the database");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var personToDelete = await _db.People.FirstOrDefaultAsync(x => x.Id == id);
                if (personToDelete == null)
                {
                    return NotFound("person not found");
                }
                _db.People.Remove(personToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error deleting specified person");
            }
        }
    }
}
