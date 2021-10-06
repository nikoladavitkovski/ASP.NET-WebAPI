using Moq;
using SEDC.NoteApp2.DataAccess.Interfaces;
using SEDC.NoteApp2.Domain.Models;
using SEDC.NoteApp2.Shared.Enums;
using SEDC.NoteApp2.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.Tests.MockRepositories
{
    public static class MockNoteRepository
    {
        public static INoteRepository GetMockUserRepository()
        {
            List<Note> notes = new List<Note>()
            {
                new Note
                {
                    Id = 1,
                    Color = "Red",
                    Tag = TagType.Health,
                    Text = "My First Note",
                    UserId = 1
                }
            };

            Mock<INoteRepository> mockNoteRepository = new Mock<INoteRepository>();

            mockNoteRepository.Setup(x => x
                .GetById(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return notes.FirstOrDefault(q => q.Id == id);
                });

            return mockNoteRepository.Object;
        }
    }
}
