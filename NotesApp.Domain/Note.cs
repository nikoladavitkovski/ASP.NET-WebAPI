using System;
using System.Collections.Generic;
using System.Text;

namespace NotesApp.Domain
{
    public class Note
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Color { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
