using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobbApi.Api.Manage.DTOs;
using JobbApi.Data;
using JobbApi.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobbApi.Api.Manage.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/manage/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CountryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Create Country
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create(CountryCreateDto createDto)
        {
            #region CheckCityExist
            if (await _context.Countries.AnyAsync(x => x.Name.ToLower() == createDto.Name.Trim().ToLower()))
            {
                return Conflict($"Country already exist by name: {createDto.Name}");
            }
            #endregion

            Country country = _mapper.Map<Country>(createDto);
            country.CreatedAt = DateTime.UtcNow.AddHours(4);
            country.ModifiedAt = DateTime.UtcNow.AddHours(4);

            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();

            return StatusCode(201, country.Id);
        }

        /// <summary>
        /// Get all Countries
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<Country> countries = await _context.Countries.ToListAsync();

            return Ok(countries);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            List<Country> countries = await _context.Countries.Skip((page - 1) * 7).Take(7).ToListAsync();

            CountryListDto countryList = new CountryListDto
            {
                Countries = _mapper.Map<List<CountryItemDto>>(countries),
                TotalCount = await _context.Countries.CountAsync()
            };

            return Ok(countryList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Country country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            //404
            #region CheckCountryNotFound
            if (country == null)
                return NotFound();
            #endregion

            CountryGetDto categoryDto = _mapper.Map<CountryGetDto>(country);

            return Ok(categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CountryCreateDto createDto)
        {
            if (await _context.Countries.AnyAsync(x => x.Name == createDto.Name && x.Id != id))
            {
                return StatusCode(409);
            }

            Country country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (country == null)
                return NotFound();

            country.Name = createDto.Name;

            country.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Country country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            //404
            #region CheckCityNotFound
            if (country == null)
                return NotFound();
            #endregion

            _context.Countries.Remove(country);
            _context.SaveChanges();

            return NoContent();

        }
    }
}
