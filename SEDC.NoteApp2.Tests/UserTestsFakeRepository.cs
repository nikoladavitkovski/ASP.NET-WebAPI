using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Shared;
using SEDC.NoteApp2.Tests.FakeRepositories;
using SEDC.NotesApp2.Services.Implementations;
using SEDC.NotesApp2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.Tests
{
    [TestClass]
    public class UserTestsFakeRepository
    {
        [TestMethod]
        public void Authenticate_ValidUsernamePassword_ValidToken()
        {
            // Arrange
            IOptions<AppSettings> options =
                Options.Create(
                    new AppSettings
                    {
                        Secret = "DkYzU7ypt2UhywG3"
                    });

            IUserService userService = new UserService(new FakeUserRepository(), options);
            string username = "bob007";
            string password = "123456sedc";

            // Act
            TokenDto result = userService.Authenticate(username, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Token));
        }

        [TestMethod]
        public void Authenticate_InvalidUsernamePassword_Null()
        {
            // Arrange
            IOptions<AppSettings> options =
                Options.Create(
                    new AppSettings
                    {
                        Secret = "DkYzU7ypt2UhywG3"
                    });

            IUserService userService = new UserService(new FakeUserRepository(), options);
            string username = "NonExisting";
            string password = "1235456789876543";

            // Act
            TokenDto result = userService.Authenticate(username, password);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Register_ValidData_RegisterUser()
        {
            // Arrange
            IOptions<AppSettings> options =
                Options.Create(
                    new AppSettings
                    {
                        Secret = "DkYzU7ypt2UhywG3"
                    });

            IUserService userService = new UserService(new FakeUserRepository(), options);

            RegisterUserDto registerUserDto = new RegisterUserDto()
            {
                Id = 10,
                FirstName = "Greg",
                PassWord = "123456greg",
                UserName = "gregsuper",
                Address = "Macedonia",
                Age = 30,
            };

            // Act
            userService.AddUser(registerUserDto);

            // Assert
            UserDto userDto = userService.GetUserById(registerUserDto.Id);
            Assert.AreEqual(registerUserDto.FirstName, userDto.FirstName);
            Assert.AreEqual(registerUserDto.LastName, userDto.LastName);
        }
    }
}
