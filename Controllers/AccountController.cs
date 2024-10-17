using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Account;
using api.Interfaces;
using api.models;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase

    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager=signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var AppUser = new AppUser
                {
                    UserName = registerDto.username,
                    Email = registerDto.Email
                };
                var createUser = await _userManager.CreateAsync(AppUser, registerDto.Password);
                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(AppUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                         new NewUserDto
                         {
                             UserName = AppUser.UserName,
                             Email = AppUser.Email,
                             Token = _tokenService.CreateToken(AppUser)
                         }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);

                }



            }
            catch (Exception e)
            {
                return StatusCode(500, e);

            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
       //find the user using _userManager(without going to the DBContext)    
            var user = await _userManager.Users.FirstOrDefaultAsync(x =>x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

//check the password
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);// false is for (bool lockoutOnFailure)

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");
            
            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
    }
}