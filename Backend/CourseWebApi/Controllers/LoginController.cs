using CourseDtos;
using CourseWebApi.Services;
using CourseWebApi.Utilitys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CourseModels;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using CourseUtility.DataValidator;
using CourseUtility;

namespace CourseWebApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _iConfiguration;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public LoginController(ILoginService loginService,
                            IConfiguration iConfiguration,
                            IMapper mapper,
                            IAuthenticationService authenticationService)
        {
            _loginService = loginService;
            _iConfiguration = iConfiguration;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginInfo)
        {
            var loginValidator = new LoginRequestValidator();
            var validatorResult = loginValidator.Validate(loginInfo);
            if (!validatorResult.IsValid)
            {
                return BadRequest();
            }

            //sign In
            var user = await _loginService.GetUserByEmail(loginInfo.Email,loginInfo.Role);
            //failure of sign in
            if (user == null)
            {
                return StatusCode(401);
            }

            var verifyPassword = PasswordHasher.VerifyHashedPassword(loginInfo.Password, user.Key, user.Password);

            //failure of sign in
            if (user == null || verifyPassword == false)
            {
                return StatusCode(401);
            }

            // generate  token of jwt
            var tokenStr = _authenticationService.GenerateJwtToken(user);

            if (string.IsNullOrWhiteSpace(tokenStr))
            {
                return BadRequest();
            }

            var loginResponseDto = _mapper.Map<LoginResponseDto>(user);
            var result = new Dictionary<string, object>();
            result.Add("user", loginResponseDto);
            result.Add("jwtToken", tokenStr);

            return Ok(result);
        }

        [HttpGet("login")]
        [Authorize]
        public async Task<IActionResult> AutoLogin([FromQuery] string email)
        {
            var user = await _loginService.GetUserByEmail(email);
            if (user == null)
            {
                return StatusCode(401);
            }

            var loginResponseDto = _mapper.Map<LoginResponseDto>(user);

            // generate  token of jwt
            var tokenStr = _authenticationService.GenerateJwtToken(user);

            if (string.IsNullOrWhiteSpace(tokenStr))
            {
                return BadRequest();
            }

            var result = new Dictionary<string, object>();

            result.Add("user", loginResponseDto);
            result.Add("jwtToken", tokenStr);

            return Ok(result);
        }
    }
}
