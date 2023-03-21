using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP.NETAssignmentMovieStore.Models.ViewModels
{
    public class MoviesCreateViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string Director { get; set; }

        [Required]
        [DisplayName("Release Year")]
        public int ReleaseYear { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Category { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Movie Time")]
        public int MovieTime { get; set; }

        [NotMapped]
        [Required]
        [DisplayName("Movie Image")]
        public virtual IFormFile CoverMovieImage { get; set; }

    }
}
