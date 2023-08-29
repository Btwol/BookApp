using Newtonsoft.Json;

namespace BookApp.Shared.Data
{
    public class HighlightModel
    {
        public readonly int Id;
        public int BookAnalysisId { get; set; }
        public int PageNumber { get; set;  }
        public int NodeCount { 
            get
            {
                return LastNodeIndex - FirstNodeIndex + 1;
            }
        }
        public int FirstNodeIndex { get; set; }
        public int FirstNodeCharIndex { get; set; }
        public int LastNodeIndex { get; set; }
        public int LastNodeCharIndex { get; set; }
        public string RawPositionString { get; set; }

        public HighlightModel()
        {

        }

        public HighlightModel(int bookAnalysisId, int pageNumber, string rawPositionString)
        {
            BookAnalysisId = bookAnalysisId;
            PageNumber = pageNumber;
            RawPositionString = rawPositionString;
            Update(this);
        }

        public void Update(HighlightModel newSelectionRange)
        {
            int[,] RawArray = JsonConvert.DeserializeObject<int[,]>(newSelectionRange.RawPositionString);
            FirstNodeIndex = RawArray[0, 0];
            FirstNodeCharIndex = RawArray[0, 1];
            LastNodeIndex = RawArray[RawArray.Length / 2 - 1, 0];
            LastNodeCharIndex = RawArray[RawArray.Length / 2 - 1, 1];
        }

        public string GetElementId()
        {
            return "pernamentHighlight_" + Id;
        }
    }
}
