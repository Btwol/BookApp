﻿namespace BookApp.Server.Models
{
    public class BookAnalysis : IDbModel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string AnalysisTitle { get; set; }

        [Required]
        public string BookHash { get; set; }

        [Required, MaxLength(200)]
        public string BookTitle { get; set; }

        [MaxLength(500)]
        public string Authors { get; set; }

        public virtual List<Tag> Tags { get; set; }
        public virtual List<Highlight> Highlights { get; set; }
        public virtual List<AppUser> Users { get; set; } = new();
        //public virtual List<Note> Notes { get; set; }
    }
}