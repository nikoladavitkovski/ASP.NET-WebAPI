using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public ActionResult<List<User>> GetAllUsers(User user)
        {
            User user1 = new User {
                Age = 28,
                FirstName = "Andrej",
                LastName = "Lazarevski"
            };
            User user2 = new User
            {
                Age = 31,
                FirstName = "Filip",
                LastName = "Petrovski"
            };
            User user3 = new User
            {
                Age = 34,
                FirstName = "Viktor",
                LastName = "Dimcevski"
            };
            return users;
        }
        public ActionResult<List<User>> GetUserByIndex(User user)
        {
            if(users.Count() == 1)
            {
                User user1 = new User
                {
                    Age = 28,
                    FirstName = "Andrej",
                    LastName = "Lazarevski"
                };
            }
            if(users.Count() == 2)
            {
                User user2 = new User
                {
                    Age = 31,
                    FirstName = "Filip",
                    LastName = "Petrovski"
                };
            }
            if(users.Count() == 3)
            {
                User user3 = new User
                {
                    Age = 34,
                    FirstName = "Viktor",
                    LastName = "Dimcevski"
                };
            }
            return users;
        }
        public ActionResult<User> CheckUsers(User user)
        {
            if(user.Age >= 28)
            {
                System.Console.WriteLine("The user is an adult.");
            }
            return user;
        }
        public ActionResult<List<User>> GetAll(User user)
        {
            if(user == null)
            {
                StatusCode(StatusCodes.Status404NotFound);
                return NotFound();
            }
            return BadRequest();
        }
    }
}
