using SEDC.NoteApp2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NoteApp2.Dto.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

        public List<NoteDto> Notes { get; set; }
    }
}
