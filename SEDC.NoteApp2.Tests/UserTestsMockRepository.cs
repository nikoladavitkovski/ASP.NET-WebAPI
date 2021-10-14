using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Shared;
using SEDC.NoteApp2.Shared.Exceptions;
using SEDC.NoteApp2.Tests.FakeRepositories;
using SEDC.NoteApp2.Tests.MockRepositories;
using SEDC.NotesApp2.Services.Implementations;
using SEDC.NotesApp2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.Tests
{
    public class UserTestsMockRepository
    {
        [TestMethod]
        public void GetNoteById_ValidId_Note()
        {
            // Arrange 
            INoteService noteService = new NoteService(MockNoteRepository.GetMockUserRepository());
            int noteId = 1;
            string noteText = "My First Note";

            // Act 
            NoteDto result = noteService.GetNoteById(noteId);

            // Assert
            Assert.AreEqual(noteText, result.Text);
        }

        [TestMethod]
        public void GetNoteById_InvalidId_NoteException()
        {
            // Arrange 
            INoteService noteService = new NoteService(MockNoteRepository.GetMockUserRepository());
            int noteId = 100;

            // Act // Assert
            Assert.ThrowsException<NoteException>(() => noteService.GetNoteById(noteId));
        }
    }
}
