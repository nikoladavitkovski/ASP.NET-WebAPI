using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Dto.ValidationModels;
using SEDC.NotesApp2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IEntityValidationService _entityValidationService;

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            List<UserDto> users = _userService.GetAllUsers();
            return StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpGet("all/notes")]
        public ActionResult<List<UserDto>> GetAllUsersIncludeProperties()
        {
            List<UserDto> users = _userService.GetAllUsersIncludeNotes();
            return StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpGet("{id}")]
        public ActionResult<List<UserDto>> GetUserById(int id)
        {
            UserDto user = _userService.GetUserById(id);
            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpGet("{id}/notes")]
        public ActionResult<List<UserDto>> GetUserByIdIncludeProperties(int id)
        {
            UserDto user = _userService.GetUserByIdIncludeNotes(id);
            return StatusCode(StatusCodes.Status200OK, user);
        }

        [AllowAnonymous]
        [HttpPost("")]
        public ActionResult AddUser(RegisterUserDto userDto)
        {
            ValidationResponse validationResponse = _entityValidationService.ValidateRegisterUser(userDto);

            if (validationResponse.HasError)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResponse);
            }

            _userService.AddUser(userDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("update")]
        public ActionResult UpdateUser(UserDto userDto)
        {
            _userService.UpdateUser(userDto);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        [HttpDelete("{id}/delete")]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return StatusCode(StatusCodes.Status202Accepted);
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]

        public ActionResult<TokenDto> AuthenticateUser(LogInDto model)
        {
            TokenDto token = _userService.Authenticate(model.UserName, model.PassWord);

            if (token == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Username Or Password");
            }

            return StatusCode(StatusCodes.Status200OK, token);
        }
        [HttpGet("whoami")]

        public ActionResult<string> WhoAmI()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            string username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string userAddress = User.FindFirst("CustomClaimTypeUserAddress").Value;
            string password = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return StatusCode(StatusCodes.Status200OK, $"{userId} - {username} {userAddress} {password}");
        }
        //Add ChangeUserPassWord Logic
    }
}
