using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using AutoMapper;
using HotelListing.API.Contracts;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository)
        {
            _mapper = mapper;
            _countriesRepository = countriesRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            //if (_context.Countries == null)
            //{
            //    return NotFound();
            //}

            // select * from countries
            //var countries = await _context.Countries.ToListAsync();

            var countries = await _countriesRepository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records);
        }

        // GET: api/Countries/5
        [HttpGet($"{{{nameof(id)}}}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            //if (_context.Countries == null)
            //{
            //    return NotFound();
            //}

            // select * from countries where id = @id
            //var country = await _context.Countries.FindAsync(id);

            //var country = await _context.Countries.Include(q => q.Hotels)
            //    .FirstOrDefaultAsync(q => q.Id == id);
            var country = await _countriesRepository.GetDetailsAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountry)
        {
            //if (_context.Countries == null)
            //{
            //    return Problem("Entity set 'HotelListingDbContext.Countries'  is null.");
            //}

            // worse: 欄位很多的話這邊就要一大堆
            //var country = new Country
            //{
            //    Name = createCountry.Name,
            //    ShortName = createCountry.ShortName,
            //};

            // better
            var country = _mapper.Map<Country>(createCountry);

            //_context.Countries.Add(country);
            //await _context.SaveChangesAsync();
            await _countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            //_context.Entry(country).State = EntityState.Modified;

            // By doing this, we are tracking this 'country' record.
            // Any modification will make its state 'Modified' and apply to database.
            //var country = await _context.Countries.FindAsync(id);
            //if (country is null)
            //{
            //    return NotFound();
            //}
            var country = await _countriesRepository.GetAsync(id);
            if(country is null)
            {
                return NotFound();
            }

            // Map the properties of updateCountryDto to country, the existed properties in country will be replaced.
            _mapper.Map(updateCountryDto, country);

            try
            {
                // EF automatically update the database when 'save' is called because the 'country' object is tracked.
                //await _context.SaveChangesAsync();
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id).Result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //if (_context.Countries == null)
            //{
            //    return NotFound();
            //}
            //var country = await _context.Countries.FindAsync(id);
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            //_context.Countries.Remove(country);
            //await _context.SaveChangesAsync();
            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            //return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
            return await _countriesRepository.Exists(id);
        }
    }
}
