using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobbApi.Api.Client.DTOs;
using JobbApi.Data;
using JobbApi.Data.Entities;
using JobbApi.Helpers;
using JobbApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobbApi.Api.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
       

        public AccountController(AppDbContext context, UserManager<AppUser> userManager, IJwtService jwtService, RoleManager<IdentityRole> roleManager,IMapper mapper,IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _jwtService = jwtService;
            _roleManager = roleManager;
            _mapper = mapper;
            _env = env;
            
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(registerDto.Email);

            //409
            #region CheckUserAlreadyExist
            if (user != null)
                return StatusCode(409, $"User already exist by email {registerDto.Email}");
            #endregion

            user = new AppUser
            {
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            //402
            #region CheckResultFailed
            if (!result.Succeeded)
            {
                return StatusCode(402, result.Errors.First().Description);
            }
            #endregion

            await _userManager.AddToRoleAsync(user, "Member");

            return StatusCode(201, user.Id);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(MemberLoginDto loginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);

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

            return Ok(new { user.FullName, Token = token });
        }

        [Authorize(Roles = "Member")]
        [HttpPut("updatepassword")]
        public async Task<IActionResult> UpdatePassword(MemberUpdatePasswordDto updatePasswordDto)
        {
            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //404
            #region CheckUserNotFound
            if (existUser == null)
                return NotFound();
            #endregion


            var resultPass = await _userManager.ChangePasswordAsync(existUser, updatePasswordDto.CurrentPassword, updatePasswordDto.NewPassword);
            if (!resultPass.Succeeded)
            {
                return StatusCode(402, resultPass.Errors.First().Description);
            }

            //await _userManager.UpdateAsync(existUser);


            return StatusCode(201, existUser.Id);
        }

        [Authorize(Roles = "Member")]
        [HttpPut("updatedetails")]
        public async Task<IActionResult> UpdateDetails([FromForm]MemberDetailUpdateDto memberDetailUpdate)
        {
            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);


            //404
            #region CheckUserNotFound
            if (existUser == null)
                return NotFound();
            #endregion

            //#region CheckUserAlreadyExist
            //if (await _userManager.Users.AnyAsync(u=>u.Email==memberDetailUpdate.Email))
            //    return StatusCode(409, $"User already exist by email {memberDetailUpdate.Email}");
            //#endregion

            //#region CheckUserAlreadyExist
            //if (await _userManager.Users.AnyAsync(u => u.UserName == memberDetailUpdate.Username))
            //    return StatusCode(409, $"User already exist by username {memberDetailUpdate.Username}");
            //#endregion

            if (memberDetailUpdate.File != null)
            {
                #region PhotoLengthChecking
                if (memberDetailUpdate.File.Length > 3 * (1024 * 1024))
                {
                    return StatusCode(409);

                }
                #endregion
                #region PhotoContentTypeChecking
                if (memberDetailUpdate.File.ContentType != "image/png" && memberDetailUpdate.File.ContentType != "image/jpeg")
                {
                    return StatusCode(409);
                }
                #endregion

                string filename = FileManagerHelper.Save(_env.WebRootPath, "uploads/users", memberDetailUpdate.File);

                if (!string.IsNullOrWhiteSpace(existUser.Photo))
                {
                    FileManagerHelper.Delete(_env.WebRootPath, "uploads/users", existUser.Photo);
                }

                existUser.Photo = filename;
            }

            existUser.UserName = memberDetailUpdate.Username;
            existUser.Email = memberDetailUpdate.Email;
            existUser.PhoneNumber = memberDetailUpdate.PhoneNumber;
            existUser.Occupation = memberDetailUpdate.Occupation;
            existUser.Address = memberDetailUpdate.Address;
            existUser.Desc = memberDetailUpdate.Desc;

            //existUser = _mapper.Map<AppUser>(memberDetailUpdate);
            existUser.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return StatusCode(201,new { existUser.Id, existUser.UserName });



        }
    }
}
