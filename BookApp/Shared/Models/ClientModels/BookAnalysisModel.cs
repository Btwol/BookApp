﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared.Models.ClientModels
{
    public class BookAnalysisModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Analysis title is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Analysis title must be between 3 and 30 characters.")]
        public string AnalysisTitle { get; set; }
        public string BookHash { get; set; }
        public string BookTitle { get; set; }
        public List<string> Authors { get; set; } = new();
        public List<TagModel> Tags { get; set; } = new();
        public List<HighlightModel> Highlights { get; set; } = new();
        public List<NoteModel> Notes { get; set; } = new();

        //author (user)
        //book title/author

        public BookAnalysisModel()
        {

        }

        public BookAnalysisModel(BookAnalysisModel bookAnalysisModel)
        {
            this.Id = bookAnalysisModel.Id;
            this.AnalysisTitle = bookAnalysisModel.AnalysisTitle;
            this.BookHash = bookAnalysisModel.BookHash;
            this.BookTitle = bookAnalysisModel.BookHash;
            this.Authors = bookAnalysisModel.Authors;
            this.Tags = bookAnalysisModel.Tags;
            this.Highlights = bookAnalysisModel.Highlights;
            this.Notes = bookAnalysisModel.Notes;
        }
    }
}
