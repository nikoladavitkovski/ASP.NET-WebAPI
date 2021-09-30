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
    public class ContactEntryService : IContactEntryService
    {
        private IContactEntryRepository _contactEntryRepository;

        public ContactEntryService(IContactEntryRepository contactEntryRepository)
        {
            _contactEntryRepository = contactEntryRepository;
        }
        public void AddContactEntry(ContactEntryDto contactEntryDto)
        {
            ContactEntry contactEntry = contactEntryDto.ToContactEntry();
            _contactEntryRepository.Insert(contactEntry);
        }

        public void DeleteContactEntry(int id)
        {
            ContactEntry contactEntry = _contactEntryRepository.GetById(id);
            _contactEntryRepository.Delete(contactEntry);
        }

        public List<ContactEntryDto> GetAllContactEntries()
        {
            List<ContactEntry> contactEntries = _contactEntryRepository.GetAll();

            List<ContactEntryDto> contactEntryDtos = new List<ContactEntryDto>();

            foreach(ContactEntry contactEntry in contactEntries)
            {
                contactEntryDtos.Add(contactEntry.ToContactEntryDto());
            }
            return contactEntryDtos;
        }

        public ContactEntryDto GetContactEntryById(int id)
        {
            ContactEntry contactEntry = _contactEntryRepository.GetById(id);
            ContactEntryDto contactEntryDto = ContactEntryMapper.ToContactEntryDto(contactEntry);
            return contactEntryDto;
        }

        public void UpdateContactEntry(ContactEntryDto contactEntryDto)
        {
            ContactEntry contactEntry = contactEntryDto.ToContactEntry();
            _contactEntryRepository.Update(contactEntry);
        }

        public ValidationResponse ValidateContactEntry(ContactEntryDto contactEntryDto)
        {
            throw new NotImplementedException();
        }
    }
}
