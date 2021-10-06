using SEDC.NoteApp2.DataAccess.Interfaces;
using SEDC.NoteApp2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.Tests.FakeRepositories
{
    public class FakeNoteRepository : INoteRepository
    {
        private List<Note> _notes;

        public FakeNoteRepository()
        {
            _notes = new List<Note>()
            {
                new Note()
                {
                    Id = 1,
                    UserId = 3456,
                    Tag = Shared.Enums.TagType.Work,
                    Color = "Green",
                    Text = "The note is found",
                }
            };
        }
        public void Add(Note entity)
        {
            _notes.Add(entity);
        }

        public void Delete(Note entity)
        {
            _notes.Remove(entity);
        }

        public List<Note> GetAll()
        {
            return _notes.ToList();
        }

        public List<Note> GetAllByUserId(int userId)
        {
            return _notes.Where(x => x.UserId == userId).ToList();
        }

        public Note GetById(int id)
        {
            return _notes.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
