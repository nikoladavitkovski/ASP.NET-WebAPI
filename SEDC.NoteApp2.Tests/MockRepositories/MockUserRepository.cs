using Moq;
using SEDC.NoteApp2.DataAccess.Interfaces;
using SEDC.NoteApp2.Domain.Models;
using SEDC.NoteApp2.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.Tests.MockRepositories
{
    public static class MockUserRepository
    {
        public static IUserRepository GetMockUserRepository()
        {
            List<User> users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Username = "bob007",
                    PassWord = "123456sedc".GenerateMD5(),
                    Address = "Macedonia",
                    Age = 30,
                    Notes = new List<Note>()
                }
            };

            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x
                .GetUserByUserNameAndPassWord(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string username, string password) =>
                {
                    return users.SingleOrDefault(q => q.Username == username && q.PassWord == password);
                });

            mockUserRepository.Setup(x => x
                .GetById(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return users.FirstOrDefault(q => q.Id == id);
                });

            mockUserRepository.Setup(x => x
               .GetById(100))
               .Returns((int id) =>
               {
                   return new User()
                   {
                       Id = 100,
                       FirstName = "I Come From",
                       LastName = "Moq",
                       Username = "username.moq",
                       PassWord = "123456sedc".GenerateMD5(),
                       Address = "Macedonia",
                       Age = 30,
                       Notes = new List<Note>()
                   };
               });

            mockUserRepository.Setup(x => x
                .Add(It.IsAny<User>()))
                .Callback((User user) =>
                {
                    users.Add(user);
                });

            return mockUserRepository.Object;
        }
    }
}
