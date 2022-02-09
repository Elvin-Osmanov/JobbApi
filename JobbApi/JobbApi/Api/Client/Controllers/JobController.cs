using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobbApi.Api.Client.DTOs;
using JobbApi.Data;
using JobbApi.Data.Entities;
using JobbApi.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobbApi.Api.Client.Controllers
{
    [Authorize(Roles = "Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public JobController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(JobCreateDto jobCreateDto)
        {
            //404
            #region CheckCategoryNotFound
            if (!await _context.Categories.AnyAsync(x => x.Id == jobCreateDto.CategoryId))
                return NotFound($"Category not found by id: {jobCreateDto.CategoryId}");
            #endregion
            //404
            #region CheckCityNotFound
            if (!await _context.Categories.AnyAsync(x => x.Id == jobCreateDto.CityId))
                return NotFound($"City not found by id: {jobCreateDto.CityId}");
            #endregion
            //404
            #region CheckCountryNotFound
            if (!await _context.Categories.AnyAsync(x => x.Id == jobCreateDto.CountryId))
                return NotFound($"Country not found by id: {jobCreateDto.CountryId}");
            #endregion
            //404
            #region CheckCompanyNotFound
            if (!await _context.Categories.AnyAsync(x => x.Id == jobCreateDto.CompanyId))
                return NotFound($"Company not found by id: {jobCreateDto.CompanyId}");
            #endregion

            Job job = _mapper.Map<Job>(jobCreateDto);
            job.CreatedAt = DateTime.UtcNow.AddHours(4);
            job.ModifiedAt = DateTime.UtcNow.AddHours(4);

            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();

            return StatusCode(201, job.Id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Job job = await _context.Jobs
                .Include(x => x.Category).Include(x => x.City)
                .Include(x => x.Company).Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            //404
            #region CheckJobNotFound
            if (job == null)
                return NotFound();
            #endregion

            JobGetDto jobDto = _mapper.Map<JobGetDto>(job);

            return Ok(jobDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            List<Job> jobs = await _context.Jobs
                 .Include(x => x.Category).Include(x => x.City)
                .Include(x => x.Company).Include(x => x.Country)
                .Skip((page - 1) * 10).Take(10).ToListAsync();

            JobListDto jobDtos = new JobListDto
            {
                Jobs = _mapper.Map<List<JobItemDto>>(jobs),
                TotalCount = await _context.Jobs.CountAsync()
            };

            return Ok(jobDtos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobCreateDto jobEditDto)
        {
            Job job = await _context.Jobs
                .Include(x => x.Category).Include(x => x.City)
                .Include(x => x.Company).Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);
            //404
            #region CheckCategoryNotFound
            if (!await _context.Categories.AnyAsync(x => x.Id == jobEditDto.CategoryId))
                return NotFound($"Category not found by id: {jobEditDto.CategoryId}");
            #endregion
            //404
            #region CheckCityNotFound
            if (!await _context.Categories.AnyAsync(x => x.Id == jobEditDto.CityId))
                return NotFound($"City not found by id: {jobEditDto.CityId}");
            #endregion
            //404
            #region CheckCountryNotFound
            if (!await _context.Categories.AnyAsync(x => x.Id == jobEditDto.CountryId))
                return NotFound($"Country not found by id: {jobEditDto.CountryId}");
            #endregion
            //404
            #region CheckCompanyNotFound
            if (!await _context.Categories.AnyAsync(x => x.Id == jobEditDto.CompanyId))
                return NotFound($"Company not found by id: {jobEditDto.CompanyId}");
            #endregion
            job.Title = jobEditDto.Title;
            job.Address = jobEditDto.Address;
            job.Deadline = jobEditDto.Deadline;
            job.Desc = jobEditDto.Desc;
            job.Gender = jobEditDto.Gender;
            job.JobType = jobEditDto.JobType;
            job.Qualification = jobEditDto.Qualification;
            job.Salary = jobEditDto.Salary;
            job.IsActive = jobEditDto.IsActive;
            job.CompanyId = jobEditDto.CompanyId;
            job.CategoryId = jobEditDto.CategoryId;
            job.CityId = jobEditDto.CityId;
            job.CountryId = jobEditDto.CountryId;
            job.Experience = jobEditDto.Experience;
            job.ModifiedAt = DateTime.UtcNow.AddHours(4);



            await _context.SaveChangesAsync();
            return StatusCode(200);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Job job = await _context.Jobs
                .Include(x => x.Category).Include(x => x.City)
                .Include(x => x.Company).Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            //404
            #region CheckJobNotFound
            if (job == null)
                return NotFound();
            #endregion

            _context.Jobs.Remove(job);
            _context.SaveChanges();

            return NoContent();

        }

        [HttpGet("browse")]
        public async Task<IActionResult> GetAllBrowse(int page = 1)
        {
            List<Job> jobs = await _context.Jobs
                .Include(x => x.City)
                .Include(x => x.Company)
                .Skip((page - 1) * 8).Take(8).Where(x => x.IsActive == true).ToListAsync();

            JobGetListDto jobDtos = new JobGetListDto
            {
                Jobs = _mapper.Map<List<JobGetItemDto>>(jobs),
                TotalCount = await _context.Jobs.CountAsync()
            };

            return Ok(jobDtos);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetAllRecent(int page = 1)
        {
            List<Job> jobs = await _context.Jobs
                .Include(x => x.City)
                .Include(x => x.Company).OrderByDescending(x => x.CreatedAt)
                .Where(x => x.IsActive == true).ToListAsync();

            JobGetListDto jobDtos = new JobGetListDto
            {
                Jobs = _mapper.Map<List<JobGetItemDto>>(jobs)

            };

            return Ok(jobDtos);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(JobFilterDto filter, int page = 1)
        {

            List<Job> jobs = await _context.Jobs.Include(x => x.Country).Include(x => x.Category)
                .Where(j => j.Title.Contains(filter.Keyword)
                || j.Qualification == filter.Qualification
                || j.Gender == filter.Gender
                || (j.Salary > filter.MinSalary && j.Salary < filter.MaxSalary) || j.Experience == filter.Experience || j.JobType == filter.JobType || j.CategoryId == filter.CategoryId || j.CountryId == filter.CountryId).Skip((page - 1) * 8).Take(8)
                .ToListAsync();

            //404
            if (jobs == null)
            {
                return NotFound();
            }

            JobListFilterDto jobsDtos = new JobListFilterDto
            {
                FilteredJobs = _mapper.Map<List<JobFilterDto>>(jobs),
                TotalCount = jobs.Count()
            };




            return Ok(jobsDtos);


        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(JobSearchItemDto search, int page = 1)
        {
            List<Job> jobs = await _context.Jobs
                .Include(x => x.Company).Include(x => x.Country)
                .Where(x => x.Title.Contains(search.Title)
                || x.Company.Name.Contains(search.CompanyName)
                || x.CountryId == search.CountryId)
                .Skip((page - 1) * 8).Take(8)
                .ToListAsync();

            if (jobs == null)
            {
                return NotFound();
            }
            JobSearchDto jobDtos = new JobSearchDto
            {
                SearchedJobs = _mapper.Map<List<JobSearchItemDto>>(jobs),
                TotalCount = jobs.Count()

            };

            return Ok(jobDtos);
        }


        [HttpPut("apply/{id}")]
        public async Task<IActionResult> Apply(int id, JobApplyDto jobApplyDto)
        {
            Job job = await _context.Jobs.Include(x => x.Company).FirstOrDefaultAsync(x => x.Id == id);

            if(job == null)
            {
                return NotFound();
            }

            if (job.IsApplied != jobApplyDto.IsApplied)
            {
                job.IsApplied = jobApplyDto.IsApplied;
            }


            await _context.SaveChangesAsync();

            return StatusCode(200);

        }

        [HttpPut("bookmark/{id}")]
        public async Task<IActionResult> Bookmark(int id, JobBookmarkDto jobBookmarkDto)
        {
            Job job = await _context.Jobs.Include(x => x.Company).FirstOrDefaultAsync(x => x.Id == id);

            if (job == null)
            {
                return NotFound();
            }

            if (job.IsBookmarked!=jobBookmarkDto.IsBookmarked)
            {
                job.IsBookmarked = jobBookmarkDto.IsBookmarked;
            }

           
            


            await _context.SaveChangesAsync();

            return StatusCode(200);

        }


        [HttpGet("getapply")]
        public async Task<IActionResult> GetApplied(int page = 1)
        {
            List<Job> jobs = await _context.Jobs
               .Include(x => x.Company)
               .Where(x => x.IsApplied == true)
               .Skip((page - 1) * 8).Take(8).ToListAsync();

            JobApplyListDto jobDtos = new JobApplyListDto
            {
                Jobs = _mapper.Map<List<JobApplyItemDto>>(jobs),
                TotalCount = jobs.Count()
            };

            return Ok(jobDtos);
        }
    }
}
