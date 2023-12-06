namespace BookApp.Client.Models
{
    public class ReaderPosition
    {
        public double ScrollPosition { get; set; } = 0;
        public int Page { get; set; } = 0;
        public int? TextNode { get; set; }
        public string ElementIdToScrollTo { get; set; } = "";
    }
}
