using SEDC.PhoneBook2.Domain.Models;
using SEDC.PhoneBook2.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.Mappers
{
    public static class ContactEntryMapper
    {
        public static ContactEntry ToContactEntry(this ContactEntryDto contactEntryDto)
        {
            ContactEntry contactEntry = new ContactEntry()
            {
                Id = contactEntryDto.Id,
                PhoneNumber = contactEntryDto.PhoneNumber,
                Name = contactEntryDto.Name,
                UserId = contactEntryDto.UserId,
                User = new User()
            };
            return contactEntry;
        }
        public static ContactEntryDto ToContactEntryDto(this ContactEntry contactEntry)
        {
            ContactEntryDto contactEntryDto = new ContactEntryDto()
            {
                Id = contactEntry.Id,
                PhoneNumber = contactEntry.PhoneNumber,
                Name = contactEntry.Name,
                UserId = contactEntry.UserId,
                User = new User()
            };
            return contactEntryDto;
        }
    }
}
