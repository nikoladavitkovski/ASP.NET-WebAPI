using SEDC.NoteApp2.DataAccess.Interfaces;
using SEDC.NoteApp2.Domain.Models;
using SEDC.NoteApp2.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.Tests.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        private List<User> _users;

        public FakeUserRepository()
        {
            _users = new List<User>()
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
        }

        public void Add(User entity)
        {
            _users.Add(entity);
        }

        public void ChangePassWord(int userId, string newPasswordHashed)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllIncludeNotes()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(q => q.Id == id);
        }

        public User GetByIdIncludeNotes(int userId)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return _users.SingleOrDefault(q => q.Username == username && q.PassWord == password);
        }

        public User GetUserByUserNameAndPassWord(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsUsernameInUse(string username)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            
        }
    }
}
