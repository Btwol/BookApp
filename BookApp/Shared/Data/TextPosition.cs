using Newtonsoft.Json;

namespace BookApp.Shared.Data
{
    public class TextPosition
    {
        

        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public void RoundNumbers(int spaces)
        {
            Top = Math.Round(Top, spaces);
            Left = Math.Round(Left, spaces);
            Width = Math.Round(Width, spaces);
            Height = Math.Round(Height, spaces);
        }

        public string GetStyleLocation()
        {
            return $"width: {Width.ToString().Replace(",", ".")}px; height: {Height.ToString().Replace(",", ".")}px; " +
                $"top: {Top.ToString().Replace(",", ".")}px; left: {Left.ToString().Replace(",", ".")}px;";
        }
    }
}
