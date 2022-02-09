using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobbApi.Api.Manage.DTOs;
using JobbApi.Data;
using JobbApi.Data.Entities;
using JobbApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobbApi.Api.Manage.Controllers
{
    [Route("api/manage/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext context, UserManager<AppUser> userManager, IJwtService jwtService, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _jwtService = jwtService;
            _roleManager = roleManager;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);

            //404
            #region CheckUserNotFound
            if (user == null)
                return NotFound();
            #endregion

            //404
            #region CheckPasswordIncorrect
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return NotFound();
            #endregion

            #region JWT Generate
            var roleNames = await _userManager.GetRolesAsync(user);
            string token = _jwtService.Generate(user, roleNames);
            #endregion

            return Ok(token);
        }


        //[HttpGet]
        //public async Task Test()
        //{
        //    //await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    //await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });

        //    AppUser user = new AppUser
        //    {
        //        UserName = "SuperAdmin",
        //    };

        //    //AppUser user = await _userManager.FindByNameAsync("SuperAdmin");
            

        //    await _userManager.CreateAsync(user, "Admin1234");
        //    await _userManager.AddToRoleAsync(user, "Admin");
        //}
    }
}
