using AutoMapper;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkingWithEFCore.DataContext;

namespace WorkingWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public WeatherForecastsController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _db.WeatherForecasts.ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode(500,
                    "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(WeatherForecast weatherForecast)
        {
            try
            {
                if (weatherForecast == null) return BadRequest();
                _db.WeatherForecasts.Add(weatherForecast);
                await _db.SaveChangesAsync();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, WeatherForecast weatherForecast)
        {
            try
            {
                var weatherForecastToUpdate = await _db.WeatherForecasts.FirstOrDefaultAsync(_ => _.Id == id);
                if (weatherForecastToUpdate != null)
                {
                    weatherForecastToUpdate = _mapper.Map(weatherForecast, weatherForecastToUpdate);
                    _db.Attach(weatherForecastToUpdate).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return NotFound("we couldn't find details of the specified WeatherForecast");
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
                var weatherForecast = await _db.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == id);
                if (weatherForecast == null)
                {
                    return NotFound("requested WeatherForecast not found");
                }

                return Ok(weatherForecast);
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
                var weatherForecastToDelete = await _db.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == id);
                if (weatherForecastToDelete == null)
                {
                    return NotFound("WeatherForecast not found");
                }
                _db.WeatherForecasts.Remove(weatherForecastToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error deleting specified WeatherForecast");
            }
        }
    }
}
