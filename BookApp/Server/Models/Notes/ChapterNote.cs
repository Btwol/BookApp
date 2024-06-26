﻿namespace BookApp.Server.Models.Notes
{
    public class ChapterNote : Note
    {
        public int Chapter { get; set; }
        public int BookAnalysisId { get; set; }
        public virtual BookAnalysis BookAnalysis { get; set; }
    }
}
