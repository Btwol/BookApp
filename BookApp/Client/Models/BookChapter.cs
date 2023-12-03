namespace BookApp.Client.Models
{
    public class BookChapter
    {
        public int ChapterNumber { get; set; }
        public Dictionary<int,string> Paragraphs { get; set; }  //textNode, textContent
    }
}
