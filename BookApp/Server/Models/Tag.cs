namespace BookApp.Server.Models
{
    public class Tag : IDbModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int BookAnalysisId { get; set; }
        public virtual BookAnalysis BookAnalysis { get; set; }

        public virtual List<Highlight> Highlights { get; set; }
        //notes {}
    }
}
