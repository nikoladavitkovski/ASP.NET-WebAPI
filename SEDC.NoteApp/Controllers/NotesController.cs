using Microsoft.AspNetCore.Http;
using NotesApp.Domain;
using NotesApp.Services.Implementations;
using NotesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private INoteService _noteService;
        public NotesController()
        {
            _noteService = new NoteService();
        }

        [HttpGet]
        public ActionResult<List<Note>> Get()
        {
            return _noteService.GetAllNotes();
        }
        [HttpGet("{id}")]
        public ActionResult<Note> Get(int id)
        {
            return _noteService.GetNoteById(id);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            _noteService.AddNote(note);
            return StatusCode(StatusCodes.Status201Created, "Note created!");
        }

        [HttpPut]

        public IActionResult Put([FromBody] Note note)
        {
            _noteService.UpdateNote(note);
            return StatusCode(StatusCodes.Status204NoContent, "Note updated!");
        }
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            _noteService.DeleteNote(id);
            return StatusCode(StatusCodes.Status204NoContent, "Note deleted!");
        }
    }
}
