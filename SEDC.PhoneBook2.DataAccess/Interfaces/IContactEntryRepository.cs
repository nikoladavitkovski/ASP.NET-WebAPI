using Microsoft.EntityFrameworkCore;
using SEDC.PhoneBook2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.DataAccess.Interfaces
{
    public interface IContactEntryRepository : IRepository<ContactEntry>
    {
        public List<ContactEntry> GetAllByUserId(int userId);
    }
}
