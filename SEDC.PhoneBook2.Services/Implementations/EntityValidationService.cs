using SEDC.PhoneBook2.DataAccess.Interfaces;
using SEDC.PhoneBook2.Domain.Models;
using SEDC.PhoneBook2.Dto.Models;
using SEDC.PhoneBook2.Dto.ValidationModels;
using SEDC.PhoneBook2.Mappers;
using SEDC.PhoneBook2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.Services.Implementations
{
    public class EntityValidationService : IEntityValidationService
    {
        private IUserRepository _userRepository;

        public EntityValidationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public ValidationResponse ValidateContactEntry(ContactEntryDto contactEntryDto)
        {
            User user = _userRepository.GetById(contactEntryDto.UserId);

            if (user == null)
            {
                return ValidationResponse.CreateErrorValidation($"The user with id {contactEntryDto.UserId} was not found");
            }
            if (string.IsNullOrEmpty(contactEntryDto.Name))
            {
                return ValidationResponse.CreateErrorValidation($"The property Name for the contact entry is required.");
            }
            if(contactEntryDto.PhoneNumber.Length > 9)
            {
                return ValidationResponse.CreateErrorValidation($"The phone number can not contain more than 9 numbers.");
            }

            return ValidationResponse.CreateSuccessValidation();
        }

        public ValidationResponse ValidateUser(UserDto userDto)
        {
            User user = userDto.ToUser();
            ValidationContext validationContext = new ValidationContext(user);
            List<ValidationResult> results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(user, validationContext, results, true))
            {
                return ValidationResponse.CreateErrorValidation(results.FirstOrDefault().ErrorMessage);
            }

            if (_userRepository.IsUserNameInUse(userDto.UserName))
            {
                return ValidationResponse.CreateErrorValidation($"Username: {userDto.UserName} is not available.");
            }
            return ValidationResponse.CreateSuccessValidation();
        }
    }
}
