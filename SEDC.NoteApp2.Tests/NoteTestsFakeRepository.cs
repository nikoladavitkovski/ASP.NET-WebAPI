using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Shared;
using SEDC.NoteApp2.Tests.FakeRepositories;
using SEDC.NotesApp2.Services.Implementations;
using SEDC.NotesApp2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.Tests
{
    [TestClass]
    public class NoteTestsFakeRepository
    {
        [TestMethod]

        public void Update_ValidNote()
        {
            //Arrange

            IOptions<AppSettings> options = Options
                .Create(
                new AppSettings
                {
                    Secret = "DkYzU7ypt2UhywG3"
                }
                );
            INoteService noteService = new NoteService(new FakeNoteRepository(), options);
            int noteId = 3456;

            //Act

            NoteDto result = noteService.GetNoteById(noteId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Text));
        }
        [TestMethod]

        public void Update_InvalidNote_Null()
        {
            //Arrange

            IOptions<AppSettings> options = Options
                .Create(
                new AppSettings
                {
                    Secret = "DkYzU7ypt2UhywG3"
                }
                );
            INoteService noteService = new NoteService(new FakeNoteRepository(), options);
            int noteId = 0;

            //Act
            NoteDto result = noteService.GetNoteById(noteId);

            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]

        public void Add_ValidNote_ValidData()
        {
            //Arrange

            IOptions<AppSettings> options = Options
                .Create(
                new AppSettings
                {
                    Secret = "DkYzU7ypt2UhywG3"
                }
                );
            INoteService noteService = new NoteService(new FakeNoteRepository(), options);

            NoteDto noteDto = new NoteDto()
            {
                Id = 1,
                UserId = 3456,
                Tag = Shared.Enums.TagType.Work,
                Color = "Green",
                Text = "The note is found",
                UserFullName = "Gorgi Dimitrievski"
            };

            //Act

            noteService.AddNote(noteDto);

            //Assert
            NoteDto note = noteService.GetNoteById(noteDto.Id);
            Assert.AreEqual(noteDto.Id, note.Id);
            Assert.AreEqual(noteDto.UserId, note.UserId);
        }
    }
}
