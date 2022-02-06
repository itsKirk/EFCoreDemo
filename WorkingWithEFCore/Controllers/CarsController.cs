using AutoMapper;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkingWithEFCore.DataContext;

namespace WorkingWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CarsController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _db.Cars.ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode(500,
                    "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Car car)
        {
            try
            {
                if (car == null) return BadRequest();
                _db.Cars.Add(car);
                await _db.SaveChangesAsync();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Car car)
        {
            try
            {
                var carToUpdate = await _db.Cars.FirstOrDefaultAsync(_ => _.Id == id);
                if (carToUpdate != null)
                {
                    carToUpdate = _mapper.Map(car, carToUpdate);
                    _db.Attach(carToUpdate).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return NotFound("we couldn't find details of the specified car");
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
                var car = await _db.Cars.FirstOrDefaultAsync(x => x.Id == id);
                if (car == null)
                {
                    return NotFound("requested car not found");
                }

                return Ok(car);
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
                var carToDelete = await _db.Cars.FirstOrDefaultAsync(x => x.Id == id);
                if (carToDelete == null)
                {
                    return NotFound("car not found");
                }
                _db.Cars.Remove(carToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error deleting specified car");
            }
        }
    }
}

