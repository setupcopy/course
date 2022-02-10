using AutoMapper;
using CourseDtos;
using CourseModels;
using CourseUserApi.Services;
using CourseUtility;
using CourseUtility.DataValidator;
using CourseUtility.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseUserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService
                            ,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]UserAddDto userAddDto)
        {
            try
            {
                // validate data of user
                if (!_userService.ValidateDataOfUserRequest(userAddDto.Email, userAddDto.Password, 
                                    userAddDto.PasswordConfirmed))
                {
                    return ValidationProblem();
                }

                var user = _mapper.Map<User>(userAddDto);

                //set password and key 
                _userService.SetHashedPassword(user);

                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;

                var result = await _userService.AddUser(user);

                if (result is null)
                {
                    return BadRequest();
                }

                var userResponse = _mapper.Map<UserAddRepDto>(result);

                return Ok(userResponse);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound();
                }

                var user = await _userService.GetUserById(id);
                if (user is null)
                {
                    return NotFound();
                }

                var result = await _userService.DeleteUser(user);

                if (!result)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id,
                                        [FromBody] UserUpdateDto userUpdateDto)
        {
            try
            {
                // validate data of user
                if (!_userService.ValidateDataOfUserRequest(userUpdateDto.Email, userUpdateDto.Password,
                                      userUpdateDto.PasswordConfirmed))
                {
                    return ValidationProblem();
                }

                var user = await _userService.GetUserById(id);
                if (user is null)
                {
                    return NotFound();
                }

                //set password and key 
                _userService.SetHashedPassword(user);

                user.UpdatedAt = DateTime.UtcNow;

                _mapper.Map(userUpdateDto, user);
                var result = await _userService.UpdateUser(user);

                if (result is null)
                {
                    return BadRequest();
                }

                var userResponse = _mapper.Map<UserUpdateDto>(result);

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PaginationResourceParameters paginationParam)
        {
            try
            {
                var users = await _userService.GetUsers(paginationParam.PageSize, paginationParam.PageNumber);

                var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

                if (users == null)
                {
                    return NoContent();
                }
                else
                {
                    var result = new
                    {
                        users = userDtos,
                        totalCount = users.TotalCount,
                        pageSize = users.PageSize,
                        currentPage = users.CurrentPage,
                        totalPages = users.TotalPages
                    };

                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
