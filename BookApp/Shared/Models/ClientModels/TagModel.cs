using BookApp.Shared.Interfaces.Model;
using System.ComponentModel.DataAnnotations;

namespace BookApp.Shared.Models.ClientModels
{
    public class TagModel : IClientModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Tag name is too long.")]
        public string Name { get; set; }

        public TagModel()
        {

        }

        public TagModel(TagModel tagModel)
        {
            this.Id = tagModel.Id;
            this.Name = tagModel.Name;
        }
    }
}
