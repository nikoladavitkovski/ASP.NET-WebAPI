using SEDC.NoteApp2.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp2.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        List<UserDto> GetAllUsersIncludeNotes();
        UserDto GetUserById(int id);
        UserDto GetUserByIdIncludeNotes(int id);
        void AddUser(UserDto userDto);
        void UpdateUser(UserDto userDto);
        void DeleteUser(int id);
        TokenDto Authenticate(string userName, string passWord);
        bool ChangeUserPassWord(int userId,string password, string newPassWord);
        void AddUser(RegisterUserDto userDto);
    }
}
