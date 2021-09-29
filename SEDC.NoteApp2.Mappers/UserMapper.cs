using SEDC.NoteApp2.Domain.Models;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.NoteApp2.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserDto userDto)
        {
            User user = new User
            {
                Id = userDto.Id,
                Username = userDto.UserName,
                Address = userDto.Address,
                Age = userDto.Age,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Notes = new List<Note>()
            };

            if (userDto.Notes != null)
            {
                user.Notes = userDto.Notes.Select(x => x.ToNote()).ToList();
            }
            return user;
        }
        public static UserDto ToUserDto(this User user)
        {
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Address = user.Address,
                Age = user.Age,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username,
                Notes = new List<NoteDto>()
            };
            if (user.Notes != null)
            {
                userDto.Notes = user.Notes.Select(x => x.ToNoteDto()).ToList();
            }
            return userDto;
        }
        public static User ToUser(this RegisterUserDto userDto)
        {
            User user = new User
            {
                Id = userDto.Id,
                Username = userDto.UserName,
                Address = userDto.Address,
                Age = userDto.Age,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PassWord = userDto.PassWord.GenerateMD5(),
                Notes = new List<Note>()
            };
            return user;
        }
    }
}
