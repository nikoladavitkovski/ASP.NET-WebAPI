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
    public interface IContactEntryService
    {
        List<ContactEntryDto> GetAllContactEntries();

        ContactEntryDto GetContactEntryById(int id);

        void AddContactEntry(ContactEntryDto contactEntryDto);

        void UpdateContactEntry(ContactEntryDto contactEntryDto);

        void DeleteContactEntry(int id);
        ValidationResponse ValidateContactEntry(ContactEntryDto contactEntryDto);
    }
}
