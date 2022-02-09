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
    [Authorize(Roles = "Admin")]
    [Route("api/manage/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CityController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(CityCreateDto createDto)
        {
            #region CheckCityExist
            if (await _context.Cities.AnyAsync(x => x.Name.ToLower() == createDto.Name.Trim().ToLower()))
            {
                return Conflict($"City already exist by name: {createDto.Name}");
            }
            #endregion

            City city = _mapper.Map<City>(createDto);
            city.CreatedAt = DateTime.UtcNow.AddHours(4);
            city.ModifiedAt = DateTime.UtcNow.AddHours(4);

            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();

            return StatusCode(201, city.Id);
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<City> cities = await _context.Cities.ToListAsync();

            return Ok(cities);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            List<City> cities = await _context.Cities.Skip((page - 1) * 7).Take(7).ToListAsync();

            CityListDto cityList  = new CityListDto
            {
                Cities = _mapper.Map<List<CityItemDto>>(cities),
                TotalCount = await _context.Cities.CountAsync()
            };

            return Ok(cityList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            City city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);

            //404
            #region CheckCityNotFound
            if (city == null)
                return NotFound();
            #endregion

            CityGetDto categoryDto = _mapper.Map<CityGetDto>(city);

            return Ok(categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CityCreateDto createDto)
        {
            if (await _context.Cities.AnyAsync(x => x.Name == createDto.Name && x.Id != id))
            {
                return StatusCode(409);
            }

            City city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);

            if (city == null)
                return NotFound();

            city.Name = createDto.Name;

            city.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            City city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);

            //404
            #region CheckCityNotFound
            if (city == null)
                return NotFound();
            #endregion

            _context.Cities.Remove(city);
            _context.SaveChanges();

            return NoContent();

        }
    }
}
