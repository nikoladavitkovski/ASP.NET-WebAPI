using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.PhoneBook2.Domain.Models;
using SEDC.PhoneBook2.Dto.Models;
using SEDC.PhoneBook2.Dto.ValidationModels;
using SEDC.PhoneBook2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactEntryController : ControllerBase
    {
        private IContactEntryService _contactEntryService;

        public ContactEntryController(IContactEntryService contactEntryService)
        {
            _contactEntryService = contactEntryService;
        }

        [HttpGet]

        public ActionResult<List<ContactEntryDto>> GetAllContactEntries()
        {
            List<ContactEntryDto> contactEntryDtos = _contactEntryService.GetAllContactEntries();
            return StatusCode(StatusCodes.Status200OK, contactEntryDtos);
        }

        [HttpPost("")]

        public ActionResult AddContactEntry(ContactEntryDto contactEntryDto)
        {
            ValidationResponse validationResponse = _contactEntryService.ValidateContactEntry(contactEntryDto);
            if (validationResponse.HasError)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResponse);
            }
            _contactEntryService.AddContactEntry(contactEntryDto);
            return StatusCode(StatusCodes.Status200OK, contactEntryDto);
        }
        [HttpPost("update")]

        public ActionResult UpdateContactEntry(ContactEntryDto contactEntryDto)
        {
            _contactEntryService.UpdateContactEntry(contactEntryDto);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        [HttpDelete("{id}/delete")]

        public ActionResult DeleleContactEntry(int id)
        {
            _contactEntryService.DeleteContactEntry(id);
            return StatusCode(StatusCodes.Status202Accepted);
        }        
    }
}
