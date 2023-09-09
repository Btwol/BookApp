using BookApp.Shared.Interfaces.Model;
using System.ComponentModel.DataAnnotations;

namespace BookApp.Shared.Data
{
    public class TagModel : IClientModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Tag name is too long.")]
        public string Name { get; set; }
    }
}
