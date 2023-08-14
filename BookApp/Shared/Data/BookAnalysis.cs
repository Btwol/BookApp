using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared.Data
{
    public class BookAnalysis
    {
        public Guid AnalysisId { get; set; }
        public string AnalysisTitle { get; set; }
        public string BookHash { get; set; }
        public string BookTitle { get; set; }
        public List<string> Authors { get; set; }
        public List<Highlight> Highlights { get; set; } = new();
        public List<Note> Notes { get; set; } = new();

        //author (user)
        //book title/author
        public BookAnalysis()
        {
            AnalysisId = new Guid();
        }
    }
}
