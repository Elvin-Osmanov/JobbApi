using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
      

        public CategoryController(AppDbContext context)
        {
            _context = context;
          
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Category> categories = await _context.Categories.ToListAsync();

            return Ok(categories);
        }
    }
}
