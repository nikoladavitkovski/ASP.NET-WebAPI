using Microsoft.EntityFrameworkCore;
using SEDC.PhoneBook2.Domain.Models;
using SEDC.PhoneBook2.Dto.Models;
using SEDC.PhoneBook2.Dto.ValidationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();

        List<UserDto> GetAllUsersIncludeContactEntries();

        UserDto GetUserById(int id);

        UserDto GetUserByIdIncludeContactEntries(int id);

        void AddUser(UserDto userDto);

        void UpdateUser(UserDto userDto);

        void DeleteUser(int id);
        ValidationResponse ValidateUser(UserDto userDto);
    }
}
