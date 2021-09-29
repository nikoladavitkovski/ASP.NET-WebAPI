using SEDC.NoteApp2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NoteApp2.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetAllIncludeNotes();
        User GetByIdIncludeNotes(int userId);

        bool IsUsernameInUse(string username);

        User GetUserByUserNameAndPassWord(string username,string password);
    }
}
