using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Dto.ValidationModels;
using SEDC.NoteApp2.Shared.Exceptions;
using SEDC.NotesApp2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private INoteService _noteService;
        private IEntityValidationService _entityValidationService;

        public NoteController(INoteService noteService, IEntityValidationService entityValidationService)
        {
            _noteService = noteService;
            _entityValidationService = entityValidationService;
        }

        [HttpGet("")]
        public ActionResult<List<NoteDto>> GetAllNotes()
        {
            List<NoteDto> notes = _noteService.GetAllNotes();
            return StatusCode(StatusCodes.Status200OK, notes);
        }

        [HttpGet("user/{id}")]
        public ActionResult<List<NoteDto>> GetNoteUserById(int id)
        {
            List<NoteDto> note = _noteService.GetAllNotesByUserId(id);
            return StatusCode(StatusCodes.Status200OK, note);
        }

        [HttpGet("{id}")]
        public ActionResult<List<NoteDto>> GetNoteById(int id)
        {
            try
            {
                NoteDto note = _noteService.GetNoteById(id);
                return StatusCode(StatusCodes.Status200OK, note);
            }
            catch (NoteException noteEx)
            {
                return StatusCode(StatusCodes.Status404NotFound, noteEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("")]
        public ActionResult AddNote(NoteDto noteDto)
        {
            ValidationResponse validationResponse = _entityValidationService.ValidateNote(noteDto);

            if (validationResponse.HasError)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResponse);
            }

            _noteService.AddNote(noteDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("update")]
        public ActionResult UpdateNote(NoteDto noteDto)
        {
            _noteService.UpdateNote(noteDto);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        [HttpDelete("{id}/delete")]
        public ActionResult DeleteNote(int id)
        {
            _noteService.DeleteNote(id);
            return StatusCode(StatusCodes.Status202Accepted);
        }
    }
}
