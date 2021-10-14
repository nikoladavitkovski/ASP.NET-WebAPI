using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Dto.ValidationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp2.Services.Interfaces
{
    public interface IEntityValidationService
    {
        ValidationResponse ValidateNote(NoteDto noteDto);
        ValidationResponse ValidateRegisterUser(RegisterUserDto userDto);
    }
}
