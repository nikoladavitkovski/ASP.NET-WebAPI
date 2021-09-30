using Microsoft.EntityFrameworkCore;
using SEDC.PhoneBook2.Domain.Models;
using SEDC.PhoneBook2.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserNameInUse(string username);

        List<User> GetAllIncludeContactEntries();

        User GetByIdIncludeContactEntries(int id);
    }
}
