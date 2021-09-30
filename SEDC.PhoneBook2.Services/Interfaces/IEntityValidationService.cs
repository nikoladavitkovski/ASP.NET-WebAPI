using SEDC.PhoneBook2.Dto.Models;
using SEDC.PhoneBook2.Dto.ValidationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.Services.Interfaces
{
    public interface IEntityValidationService
    {
        ValidationResponse ValidateUser(UserDto userDto);

        ValidationResponse ValidateContactEntry(ContactEntryDto contactEntryDto);
    }
}
