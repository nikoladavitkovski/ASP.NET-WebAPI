using SEDC.PhoneBook2.Domain.Models;
using SEDC.PhoneBook2.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserDto userDto)
        {
            User user = new User()
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Age = userDto.Age,
                Address = userDto.Address,
                UserName = userDto.UserName
            };
            return user;
        }
        public static UserDto ToUserDto(this User user)
        {
            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Address = user.Address,
                UserName = user.UserName
            };
            return userDto;
        }
    }
}
