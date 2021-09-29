using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using SEDC.NoteApp2.DataAccess.Interfaces;
using SEDC.NoteApp2.Domain.Models;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Dto.ValidationModels;
using SEDC.NoteApp2.Mappers;
using SEDC.NotesApp2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SEDC.NotesApp2.Services.Implementations
{
    public class EntityValidationService : IEntityValidationService
    {
        private IUserRepository _userRepository;

        public EntityValidationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResponse ValidateNote(NoteDto noteDto)
        {
            User user = _userRepository.GetById(noteDto.UserId);

            if (user == null)
            {
                return ValidationResponse.CreateErrorValidation($"The user with id {noteDto.UserId} was not found");
            }

            if (string.IsNullOrEmpty(noteDto.Text))
            {
                return ValidationResponse.CreateErrorValidation("The property Text for note is required");
            }

            if (noteDto.Text.Length > 100)
            {
                return ValidationResponse.CreateErrorValidation("The property Text can not contain more than 100 caracters");
            }

            return ValidationResponse.CreateSuccessValidation("");
        }

        public ValidationResponse ValidateRegisterUser(RegisterUserDto userDto)
        {
            if (!ValidPassWord(userDto.PassWord))
            {
                return ValidationResponse.CreateErrorValidation($"The password {userDto.PassWord} does not match the character length.");
            }
            User user = userDto.ToUser();
            ValidationContext validationContext = new ValidationContext(user);
            List<ValidationResult> results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(user, validationContext, results, true))
            {
                return ValidationResponse.CreateErrorValidation(results.FirstOrDefault().ErrorMessage); // this is example
            }

            if (_userRepository.IsUsernameInUse(userDto.UserName))
            {
                return ValidationResponse.CreateErrorValidation($"Username: {userDto.UserName} is not available.");
            }

            return ValidationResponse.CreateSuccessValidation("");
        }

        private static bool ValidPassWord(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            Match match = passwordRegex.Match(password);
            if (!ValidPassWord(password))
            {
                return ValidPassWord($"The password {password} does not exist.");
            }
            if(password.Length > 20)
            {
                return ValidPassWord($"The password {password.Length} must contain up to 20 characters.");
            }
            return match.Success;
        }
    }
}
