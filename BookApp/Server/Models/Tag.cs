namespace BookApp.Server.Models
{
    public class Tag : IDbModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public TaggedType TaggedType { get; set; }
        public int TaggedId { get; set; }
    }
}
