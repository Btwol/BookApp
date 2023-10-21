using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared.Models.ClientModels.Notes
{
    public class ParagraphNoteModel : NoteModel
    {
        public int TextNodeNumber { get; set; }
        public int PageNumber { get; set; }
    }
}
