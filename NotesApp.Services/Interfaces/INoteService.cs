using NotesApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        List<Note> GetAllNotes();
        Note GetNoteById(int id);

        void AddNote(Note note);

        void UpdateNote(Note note);

        void DeleteNote(int id);
    }
}
