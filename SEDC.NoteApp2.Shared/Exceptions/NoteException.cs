using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NoteApp2.Shared.Exceptions
{
    public class NoteException: Exception
    {
        public int? NoteId { get; set; }

        public NoteException() : base("There has been an issue with Note")
        {

        }

        public NoteException(int? noteId, string message) : base(message)
        {
            NoteId = noteId;
        }


    }
}
