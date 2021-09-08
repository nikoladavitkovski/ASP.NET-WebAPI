using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace UserApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public static List<User> users = new List<User>
        {
            new User
            {
                Age = 28,
                FirstName = "Andrej",
                LastName = "Lazarevski"
            },
            new User
            {
                Age = 31,
                FirstName = "Filip",
                LastName = "Petrovski"
            },
            new User
            {
                Age = 34,
                FirstName = "Viktor",
                LastName = "Dimcevski"
            }
        };

        [HttpGet("all")]
        public ActionResult<List<User>> GetAllUsers(User user)
        {
            return users;
        }

        [HttpGet("getbyindex")]
        public ActionResult<User> GetUserByIndex(int id)
        {
            if (id < 0)
            {
                StatusCode(StatusCodes.Status400BadRequest);
                return BadRequest();
            }

            User user = users.ElementAtOrDefault(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("isAdult")]
        public ActionResult<User> IsUserAnAdult(int id)
        {
            if (id < 0) return BadRequest();

            User user = users.ElementAtOrDefault(id);
            if (user == null) return NotFound();

            return Ok(user.Age >= 28);
        }

        [HttpPost("create")]
        [EnableCors]
        public ActionResult<User> CreateUser(User user)
        {
            users.Add(user);
            return CreatedAtRoute("GetByIndex", new { i = users.Count() - 1 }, user);
        }

        [HttpGet("getlength")]
        public int GetQueryArrayLength(string[] colors)
        {
            return colors.Length;
        }
    }
}
