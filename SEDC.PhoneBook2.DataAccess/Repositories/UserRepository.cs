using Microsoft.EntityFrameworkCore;
using SEDC.PhoneBook2.DataAccess.Interfaces;
using SEDC.PhoneBook2.Domain;
using SEDC.PhoneBook2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserDbContext _userDbContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public void Delete(User entity)
        {
            _userDbContext.Users.Remove(entity);
            _userDbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _userDbContext.Users
                .ToList();
        }

        public User GetById(int id)
        {
            return _userDbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(User entity)
        {
            _userDbContext.Users.Add(entity);
        }

        public void Update(User entity)
        {
            _userDbContext.Users.Update(entity);
            _userDbContext.SaveChanges();
        }

        public bool IsUserNameInUse(string username)
        {
            return _userDbContext.Users.Any(q => q.UserName == username);
        }

        public User GetByIdIncludeContactEntries(int id)
        {
            return _userDbContext.Users
                .Include(x => x.ContactEntries)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetAllIncludeContactEntries()
        {
            return _userDbContext.Users
                .Include(x => x.ContactEntries)
                .ToList();
        }
    }
}
