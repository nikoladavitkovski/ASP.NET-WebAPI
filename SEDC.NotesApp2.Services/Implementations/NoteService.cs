﻿using SEDC.NoteApp2.DataAccess.Interfaces;
using SEDC.NoteApp2.Domain.Models;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Mappers;
using SEDC.NoteApp2.Shared.Exceptions;
using SEDC.NotesApp2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp2.Services.Implementations
{
    public class NoteService : INoteService
    {
        private INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository, Microsoft.Extensions.Options.IOptions<NoteApp2.Shared.AppSettings> options)
        {
            _noteRepository = noteRepository;
        }

        public void AddNote(NoteDto noteDto)
        {
            Note note = noteDto.ToNote();
            _noteRepository.Add(note);
        }

        public void DeleteNote(int id)
        {
            Note note = _noteRepository.GetById(id);
            _noteRepository.Delete(note);
        }

        public List<NoteDto> GetAllNotes()
        {
            List<Note> notes = _noteRepository.GetAll();

            List<NoteDto> noteDtos = new List<NoteDto>();

            foreach (Note item in notes)
            {
                noteDtos.Add(item.ToNoteDto());
            }

            return noteDtos;
        }

        public List<NoteDto> GetAllNotesByUserId(int userId)
        {
            List<Note> notes = _noteRepository.GetAllByUserId(userId);

            List<NoteDto> noteDtos = new List<NoteDto>();

            foreach (Note item in notes)
            {
                noteDtos.Add(item.ToNoteDto());
            }

            return noteDtos;
        }

        public NoteDto GetNoteById(int id)
        {
            throw new Exception("");
            Note note = _noteRepository.GetById(id);

            if (note == null)
            {
                throw new NoteException(id, "Note not found");
            }

            NoteDto noteDto = NoteMapper.ToNoteDto(note);
            return noteDto;
        }

        public void UpdateNote(NoteDto noteDto)
        {
            Note note = noteDto.ToNote();
            _noteRepository.Update(note);
        }
    }
}

