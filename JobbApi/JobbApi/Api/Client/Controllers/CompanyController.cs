using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobbApi.Api.Client.DTOs;
using JobbApi.Data;
using JobbApi.Data.Entities;
using JobbApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobbApi.Api.Client.Controllers
{
    [Authorize(Roles = "Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CompanyController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CompanyCreateDto companyCreateDto)
        {
            if (companyCreateDto.File != null)
            {
                #region CheckPhotoLength
                if (companyCreateDto.File.Length > 3 * (1024 * 1024))
                {


                    return NotFound();

                }
                #endregion
                #region CheckPhotoContentType
                if (companyCreateDto.File.ContentType != "image/png" && companyCreateDto.File.ContentType != "image/jpeg")
                {


                    return NotFound();

                }
                #endregion

                string filename = FileManagerHelper.Save(_env.WebRootPath, "uploads/companies", companyCreateDto.File);

                companyCreateDto.Photo = filename;
            }
            Company company = new Company
            {
                Name = companyCreateDto.Name,
                Desc = companyCreateDto.Desc,
                Phone = companyCreateDto.Phone,
                Category = companyCreateDto.Category,
                Address = companyCreateDto.Address,
                Email = companyCreateDto.Email,
                Photo = companyCreateDto.Photo
            };

            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            return StatusCode(201, company.Id);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            List<Company> companies = await _context.Companies.Skip((page - 1) * 7).Take(7).ToListAsync();


            CompanyListDto companiesDto = new CompanyListDto
            {
                Companies = _mapper.Map<List<CompanyItemDto>>(companies),
                TotalCount = await _context.Companies.CountAsync()
            };


            return Ok(companiesDto);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            List<Company> companies = await _context.Companies.ToListAsync();

            return Ok(_mapper.Map<List<CompanyItemDto>>(companies));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Company company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            //404
            #region CheckCompanyNotFound
            if (company == null)
                return NotFound();
            #endregion

            CompanyGetDto companyDto = _mapper.Map<CompanyGetDto>(company);

            return Ok(companyDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] CompanyCreateDto companyCreate)
        {
            if (await _context.Companies.AnyAsync(x => x.Name == companyCreate.Name && x.Id != id))
            {
                return StatusCode(409);
            }

            Company company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (company == null)
                return NotFound();

            if (companyCreate.File != null)
            {
                #region PhotoLengthChecking
                if (companyCreate.File.Length > 3 * (1024 * 1024))
                {
                    return StatusCode(409);

                }
                #endregion
                #region PhotoContentTypeChecking
                if (companyCreate.File.ContentType != "image/png" && companyCreate.File.ContentType != "image/jpeg")
                {
                    return StatusCode(409);
                }
                #endregion

                string filename = FileManagerHelper.Save(_env.WebRootPath, "uploads/companies", companyCreate.File);

                if (!string.IsNullOrWhiteSpace(company.Photo))
                {
                    FileManagerHelper.Delete(_env.WebRootPath, "uploads/companies", company.Photo);
                }

                company.Photo = filename;
            }

            company.Name = companyCreate.Name;
            company.Email = companyCreate.Email;
            company.Desc = companyCreate.Desc;
            company.Category = companyCreate.Category;
            company.Phone = companyCreate.Phone;
            company.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Company company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            //404
            #region CheckCompanyNotFound
            if (company == null)
                return NotFound();
            #endregion

            _context.Companies.Remove(company);
            _context.SaveChanges();

            return NoContent();

        }
    }
}
