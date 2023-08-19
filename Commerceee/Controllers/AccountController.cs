using AutoMapper;
using Commerceee.DTOS;
using Commerceee.Extentions;
using Core.Idenrity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Commerceee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManaget;
        private readonly ITokenService _tokenService;
        private readonly IMapper _map;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManaget, ITokenService tokenService, IMapper map)
        {
            _userManager = userManager;
            _signInManaget = signInManaget;
            _tokenService = tokenService;
            _map = map;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized();
            var result = await _signInManaget.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return Unauthorized();

            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
               
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return Unauthorized();
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurreentUser()
        {
            var Email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(Email);
            return new UserDto()
            {
                Email = Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
        [HttpGet("GetUserAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress([FromQuery] string email)
        {
            var Email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(Email);
            return _map.Map<Address,AddressDto>(user.Address);
        }
        [Authorize]
        [HttpPut("updateAddress")]
        public async Task<ActionResult<AddressDto>> updateUserAddress(AddressDto addressParam)
        {
            var user = await _userManager.findUserByClaimsPrinciplesWithAddress(User);
            user.Address = _map.Map<AddressDto, Address>(addressParam);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_map.Map<Address, AddressDto>(user.Address));
            else
            {
                return BadRequest("problem update user");
            }
        }
    }
}
