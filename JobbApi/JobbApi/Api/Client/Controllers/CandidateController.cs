using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobbApi.Api.Client.DTOs;
using JobbApi.Data;
using JobbApi.Data.Entities;
using JobbApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobbApi.Api.Client.Controllers
{
    [Authorize(Roles = "Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;


        public CandidateController(AppDbContext context, IMapper mapper,UserManager<AppUser> userManager,IMailService mailService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _mailService = mailService;
  
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CandidateCreateDto candidateCreate)
        {
            //AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            ////404
            //#region CheckUserNotFound
            //if (existUser == null)
            //    return NotFound();
            //#endregion
            //404
            #region CheckJobNotFound
            if (!await _context.Jobs.AnyAsync(x => x.Id == candidateCreate.JobId))
                return NotFound($"Job not found by id: {candidateCreate.JobId}");
            #endregion

            Candidate candidate = _mapper.Map<Candidate>(candidateCreate);
           
            candidate.ModifiedAt = DateTime.UtcNow.AddHours(4);
            candidate.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        public async Task<IActionResult> GetAll(int page = 1)
        {
            List<Candidate> candidates = await _context.Candidates.Include(c=>c.AppUser)
                .Include(ca=>ca.Job).Skip((page - 1) * 7).Take(7).ToListAsync();


            CandidateListDto candidateListDto = new CandidateListDto 

            {
                Candidates = _mapper.Map<List<CandidateItemDto>>(candidates),
                TotalCount = await _context.Candidates.CountAsync()
            };


            return Ok(candidateListDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Candidate candidate = await _context.Candidates
                 .Include(x => x.Job).Include(x => x.AppUser)
                 .FirstOrDefaultAsync(x => x.Id == id);

            if (candidate == null)
                return NotFound();

            CandidateGetDto candidateDto = _mapper.Map<CandidateGetDto>(candidate);

            return Ok(candidateDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CandidateCreateDto candidateEditDto)
        {
            Candidate candidate = await _context.Candidates
                .Include(x => x.Job).ThenInclude(xx=>xx.Company).Include(x => x.AppUser)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (candidate == null)
                return NotFound();

            #region CheckJobNotFound
            if (!await _context.Jobs.AnyAsync(x => x.Id == candidateEditDto.JobId))
                return NotFound($"Job not found by id: {candidateEditDto.JobId}");
            #endregion

            candidate.Status = candidateEditDto.Status;
            candidate.ModifiedAt = DateTime.UtcNow.AddHours(4);

            if (candidateEditDto.Status == Data.Enums.CandidateStatus.Accepted)
            {
                await _mailService.SendEmailAsync(candidate.AppUser.Email, "Congratulations!!!You got the job", $"As a {candidate.Job.Company.Name} we are pleased to inform that you are our new employee");
            }
            else if (candidateEditDto.Status == Data.Enums.CandidateStatus.Rejected)
            {
                await _mailService.SendEmailAsync(candidate.AppUser.Email, "We are sorry", $"As a {candidate.Job.Company.Name} we are grateful for your spent time on application.Unfortunately we cannot offer you a job.Good Luck!!!");
            }

            await _context.SaveChangesAsync();

            return StatusCode(200);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            Candidate candidate = await _context.Candidates
                .Include(x => x.Job).Include(x => x.AppUser)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (candidate == null)
                return NotFound();

            _context.Candidates.Remove(candidate);

            await _context.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
