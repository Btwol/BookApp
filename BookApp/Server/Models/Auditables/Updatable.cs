﻿using BookApp.Server.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookApp.Server.Models.Auditables
{
    public class Updatable : Creatable
    {
        public int? UpdaterId { get; set; }
        public virtual AppUser? Updater { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}