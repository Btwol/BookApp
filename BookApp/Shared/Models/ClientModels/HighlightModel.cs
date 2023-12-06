using BookApp.Shared.Interfaces.Model;
using BookApp.Shared.Models.ClientModels.Notes;
using Newtonsoft.Json;

namespace BookApp.Shared.Models.ClientModels
{
    public class HighlightModel : ITagableItemModel, IBoundToChapter
    {
        public int Id { get; set; } = 0;
        public int BookAnalysisId { get; set; }
        public int Chapter { get; set; }
        public int NodeCount
        {
            get
            {
                return LastNodeIndex - FirstNodeIndex + 1;
            }
        }
        public int FirstNodeIndex { get; set; }
        public int FirstNodeCharIndex { get; set; }
        public int LastNodeIndex { get; set; }
        public int LastNodeCharIndex { get; set; }
        public string? RawPositionString { get; set; }
        public List<TagModel> Tags { get; set; } = new();
        public List<HighlightNoteModel> HighlightNotes { get; set; } = new();

        public HighlightModel()
        {

        }

        public HighlightModel(int bookAnalysisId, int pageNumber, string rawPositionString)
        {
            BookAnalysisId = bookAnalysisId;
            Chapter = pageNumber;
            RawPositionString = rawPositionString;
            Update(this);
        }

        public void Update(HighlightModel newSelectionRange)
        {
            RawPositionString = newSelectionRange.RawPositionString;
            if(newSelectionRange.RawPositionString is not null)
            {
                int[,] RawArray = JsonConvert.DeserializeObject<int[,]>(newSelectionRange.RawPositionString);
                FirstNodeIndex = RawArray[0, 0];
                FirstNodeCharIndex = RawArray[0, 1];
                LastNodeIndex = RawArray[(RawArray.Length / 2) - 1, 0];
                LastNodeCharIndex = RawArray[(RawArray.Length / 2) - 1, 1];
            }
        }

        public string GetElementId()
        {
            return "pernamentHighlight_" + Id;
        }

        public static int GetElementId(string id)
        {
            string numberPart = id.Replace("pernamentHighlight_", "");
            return int.Parse(numberPart);
        }
    }
}
