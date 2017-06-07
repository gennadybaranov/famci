using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameForum.Web.Infrastructure.CustomValidation;

namespace GameForum.Web.Infrastructure.Models
{
    public class GameViewModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        [Remote("CheckName", "Games")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Genre { get; set; }
        [ScaffoldColumn(false)]
        public int GenreId { get; set; }

        [RequiredIfNot("GenreId", 4, ErrorMessage = "Please, enter age")]
        [Range(0, 100, ErrorMessage = "Please, select age between 0 and 200")]
        [Display(Name = "Age Restriction")]
        public int? AgeRestriction { get; set; }
    }
}
