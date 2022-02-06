using AutoMapper;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkingWithEFCore.DataContext;

namespace WorkingWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PhonesController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _db.Phones.ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode(500,
                    "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Phone phone)
        {
            try
            {
                if (phone == null) return BadRequest();
                _db.Phones.Add(phone);
                await _db.SaveChangesAsync();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Phone phone)
        {
            try
            {
                var phoneToUpdate = await _db.Phones.FirstOrDefaultAsync(_ => _.Id == id);
                if (phoneToUpdate != null)
                {
                    phoneToUpdate = _mapper.Map(phone, phoneToUpdate);
                    _db.Attach(phoneToUpdate).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return NotFound("we couldn't find details of the specified phone");
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
                var phone = await _db.Phones.FirstOrDefaultAsync(x => x.Id == id);
                if (phone == null)
                {
                    return NotFound("requested phone not found");
                }

                return Ok(phone);
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
                var phoneToDelete = await _db.Phones.FirstOrDefaultAsync(x => x.Id == id);
                if (phoneToDelete == null)
                {
                    return NotFound("Phone not found");
                }
                _db.Phones.Remove(phoneToDelete);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error deleting specified phone");
            }
        }
    }
}
