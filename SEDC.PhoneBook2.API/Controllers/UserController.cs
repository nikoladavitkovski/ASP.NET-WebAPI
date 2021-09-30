using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.PhoneBook2.DataAccess.Interfaces;
using SEDC.PhoneBook2.Domain.Models;
using SEDC.PhoneBook2.Dto.Models;
using SEDC.PhoneBook2.Dto.ValidationModels;
using SEDC.PhoneBook2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("id")]

        public ActionResult<List<UserDto>> GetAllUsers()
        {
            List<UserDto> users = _userService.GetAllUsers();
            return StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpGet("user/{id}")]

        public ActionResult<List<UserDto>> GetUserById(int id)
        {
            UserDto user = _userService.GetUserById(id);
            return StatusCode(StatusCodes.Status200OK, user);
        }

        public ActionResult AddUser(UserDto userDto)
        {
            ValidationResponse validationResponse = _userService.ValidateUser(userDto);
            if (validationResponse.HasError)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResponse);
            }
            _userService.AddUser(userDto);
            return StatusCode(StatusCodes.Status200OK, userDto);
        }

        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        public ActionResult UpdateUser(UserDto userDto)
        {
            _userService.UpdateUser(userDto);
            return StatusCode(StatusCodes.Status202Accepted);
        }
    }
}
