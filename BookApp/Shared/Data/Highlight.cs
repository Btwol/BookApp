using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared.Data
{
    public class Highlight
    {
        public int PageNumber { get; }
        public int NodeCount { get; set; }
        public int FirstNodeIndex { get; set; }
        public int FirstNodeCharIndex { get; set; }
        public int LastNodeIndex { get; set; }
        public int LastNodeCharIndex { get; set; }
        public Dictionary<int, int> Nodes { get; set; } = new();
        public int[,] RawArray { get; set; }
        public string RawPositionString { get; set; }
        private Guid _ElementId;
        public List<Note> Notes { get; set; } = new();
        public string ElementId
        {
            get
            {
                return "pernamentHighlight_" + _ElementId.ToString();
            }
        }

        public Highlight(string positionString, int pageNumber)
        {
            _ElementId = Guid.NewGuid();
            RawPositionString = positionString;
            RawArray = JsonConvert.DeserializeObject<int[,]>(positionString);
            for (int i = RawArray[0, 0]; i < RawArray.Length / 2; i++)
            {
                Nodes.Add(RawArray[i, 0], RawArray[i, 1]);
            }
            FirstNodeIndex = RawArray[0, 0];
            FirstNodeCharIndex = RawArray[0, 1];
            LastNodeIndex = RawArray[RawArray.Length / 2 - 1, 0];
            LastNodeCharIndex = RawArray[RawArray.Length / 2 - 1, 1];
            NodeCount = LastNodeIndex - FirstNodeIndex + 1;
            PageNumber = pageNumber;
        }

        public void Update(Highlight newSelectionRange)
        {
            RawPositionString = newSelectionRange.RawPositionString;
            RawArray = JsonConvert.DeserializeObject<int[,]>(newSelectionRange.RawPositionString);
            Nodes.Clear();
            for (int i = RawArray[0, 0]; i < RawArray.Length / 2; i++)
            {
                Nodes.Add(RawArray[i, 0], RawArray[i, 1]);
            }
            FirstNodeIndex = RawArray[0, 0];
            FirstNodeCharIndex = RawArray[0, 1];
            LastNodeIndex = RawArray[RawArray.Length / 2 - 1, 0];
            LastNodeCharIndex = RawArray[RawArray.Length / 2 - 1, 1];
            NodeCount = LastNodeIndex - FirstNodeIndex + 1;
        }
    }
}
