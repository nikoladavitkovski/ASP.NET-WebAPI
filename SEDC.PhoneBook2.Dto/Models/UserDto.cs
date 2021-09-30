﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.Dto.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        public string UserName { get; set; }
        public List<ContactEntryDto> ContactEntries { get; set; }
    }
}
