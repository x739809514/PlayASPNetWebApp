using api.Dtos.Account;
using api.Model;
using api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.controller
{
    [Route("api/account")]
    [ApiController]
    public class AccoutController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        public AccoutController(UserManager<AppUser> userManager, ITokenServices tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDto { UserName = appUser.UserName, Email = appUser.Email, Token = _tokenService.CreateToken(appUser) });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName==loginDto.UserName);
            if(user == null) return Unauthorized("Invalid Username");
            var result = await _signinManager.CheckPasswordSignInAsync(user,loginDto.Password,false);
            if (result.Succeeded == false)
            {
                return Unauthorized("Username not found or Password is wrong");
            }

            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }

    }
}
