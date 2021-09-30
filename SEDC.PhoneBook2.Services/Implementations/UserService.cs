using Microsoft.EntityFrameworkCore;
using SEDC.PhoneBook2.DataAccess.Interfaces;
using SEDC.PhoneBook2.Domain.Models;
using SEDC.PhoneBook2.Dto.Models;
using SEDC.PhoneBook2.Dto.ValidationModels;
using SEDC.PhoneBook2.Mappers;
using SEDC.PhoneBook2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void AddUser(UserDto userDto)
        {
            User user = userDto.ToUser();
            _userRepository.Insert(user);
        }

        public void DeleteUser(int id)
        {
            User user = _userRepository.GetById(id);
            _userRepository.Delete(user);
        }

        public List<UserDto> GetAllUsers()
        {
            List<User> users = _userRepository.GetAll();
            List<UserDto> userDtos = new List<UserDto>();

            foreach(User user in users)
            {
                userDtos.Add(user.ToUserDto());
            }
            return userDtos;
        }

        public List<UserDto> GetAllUsersIncludeContactEntries()
        {
            List<User> users = _userRepository.GetAllIncludeContactEntries();

            List<UserDto> userDtos = new List<UserDto>();

            foreach(User user in users)
            {
                userDtos.Add(user.ToUserDto());
            }
            return userDtos;
        }

        public UserDto GetUserById(int id)
        {
            User user = _userRepository.GetById(id);
            UserDto userDto = UserMapper.ToUserDto(user);
            return userDto;
        }

        public UserDto GetUserByIdIncludeContactEntries(int id)
        {
            User user = _userRepository.GetByIdIncludeContactEntries(id);
            UserDto userDto = UserMapper.ToUserDto(user);
            return userDto;
        }

        public void UpdateUser(UserDto userDto)
        {
            User user = userDto.ToUser();
            _userRepository.Update(user);
        }

        public ValidationResponse ValidateUser(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
