using SEDC.NoteApp2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NoteApp2.DataAccess.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        List<Note> GetAllByUserId(int userId);
    }
}
