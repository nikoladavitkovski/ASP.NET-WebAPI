using Microsoft.IdentityModel.Tokens;
using SEDC.NoteApp2.DataAccess.Interfaces;
using SEDC.NoteApp2.Domain.Models;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Mappers;
using SEDC.NoteApp2.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace SEDC.NotesApp2.Services.Implementations
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(UserDto userDto)
        {
            User user = userDto.ToUser();
            _userRepository.Add(user);
        }

        public TokenDto Authenticate(string username,string password)
        {
            User user = _userRepository.GetUserByUserNameAndPassWord(username, password.GenerateMD5());

            if(user == null)
            {
                return null;
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("v9pU6HkfcZst3ksP");

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Name, user.PassWord),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("CustomClaimTypeAddress", user.Address),
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            TokenDto tokenDto = new TokenDto()
            {
                Token = tokenHandler.WriteToken(token)
            };
            return tokenDto;
        }

        public bool ChangeUserPassWord(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            Match match = passwordRegex.Match(password);
            if (!ChangeUserPassWord(password))
            {
                return ChangeUserPassWord($"The password {password} you entered can not be found.");
            }
            if (password.Length > 20)
            {
                return ChangeUserPassWord($"The password {password.Length} must contain up to 20 characters.");
            }
            if(password.StartsWith("b") && password.EndsWith("y"))
            {
                return ChangeUserPassWord($"The password {password} you have entered matches correctly.");
            }
            return match.Success;
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

            foreach (User item in users)
            {
                userDtos.Add(item.ToUserDto());
            }

            return userDtos;
        }

        public List<UserDto> GetAllUsersIncludeNotes()
        {
            List<User> users = _userRepository.GetAllIncludeNotes();
            List<UserDto> userDtos = new List<UserDto>();

            foreach (User item in users)
            {
                userDtos.Add(item.ToUserDto());
            }

            return userDtos;
        }

        public UserDto GetUserById(int id)
        {
            User user = _userRepository.GetById(id);
            UserDto userDto = UserMapper.ToUserDto(user);
            return userDto;
        }

        public UserDto GetUserByIdIncludeNotes(int id)
        {
            User user = _userRepository.GetByIdIncludeNotes(id);
            UserDto userDto = UserMapper.ToUserDto(user);
            return userDto;
        }

        public void UpdateUser(UserDto userDto)
        {
            User user = userDto.ToUser();
            _userRepository.Update(user);
        }
    }
}
