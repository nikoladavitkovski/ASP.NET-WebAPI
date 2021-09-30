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
    public class ContactEntryRepository : IContactEntryRepository
    {
        private UserDbContext _userDbContext;

        public ContactEntryRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public void Delete(ContactEntry contactEntry)
        {
            _userDbContext.ContactEntries.Remove(contactEntry);
            _userDbContext.SaveChanges();
        }

        public List<ContactEntry> GetAll()
        {
            return _userDbContext.ContactEntries
                .ToList();
        }
        public ContactEntry GetById(int id)
        {
            return _userDbContext.ContactEntries.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(ContactEntry entity)
        {
            _userDbContext.ContactEntries.Add(entity);
            _userDbContext.SaveChanges();
        }

        public void Update(ContactEntry entity)
        {
            _userDbContext.ContactEntries.Update(entity);
            _userDbContext.SaveChanges();
        }
        public List<ContactEntry> GetAllByUserId(int userId)
        {
            return _userDbContext.ContactEntries
                .Where(q => q.UserId == userId)
                .Include(x => x.User)
                .ToList();
        }
    }
}
