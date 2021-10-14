using SEDC.NoteApp2.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp2.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAllNotes();

        List<NoteDto> GetAllNotesByUserId(int userId);

        NoteDto GetNoteById(int id);

        void AddNote(NoteDto noteDto);

        void UpdateNote(NoteDto noteDto);

        void DeleteNote(int id);
        NoteDto GetNoteForUserById(int userId);
    }
}
