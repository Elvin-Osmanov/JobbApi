using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobbApi.Api.Client.DTOs;
using JobbApi.Data;
using JobbApi.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobbApi.Api.Client.Controllers
{
    [Authorize(Roles = "Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public DashboardController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpGet("getbookmark")]
        public async Task<IActionResult> GetBookmarks(int page = 1)
        {
            List<Job> jobs = await _context.Jobs
               .Include(x => x.Company)
               .Where(x => x.IsApplied == true)
               .Skip((page - 1) * 8).Take(8).ToListAsync();

            JobBookmarkListDto jobDtos = new JobBookmarkListDto
            {
                Jobs = _mapper.Map<List<JobBookmarkItemDto>>(jobs),
                TotalCount = jobs.Count()
            };

            return Ok(jobDtos);
        }
    }
}
