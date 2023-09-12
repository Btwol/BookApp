using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared.Data
{
    public class BookAnalysisModel
    {
        public int Id { get; set; }
        public string AnalysisTitle { get; set; }
        public string BookHash { get; set; }
        public string BookTitle { get; set; }
        public List<string> Authors { get; set; } = new();
        public List<TagModel> Tags { get; set; } = new();
        public List<HighlightModel> Highlights { get; set; } = new();
        public List<NoteModel> Notes { get; set; } = new();

        //author (user)
        //book title/author
    }
}
