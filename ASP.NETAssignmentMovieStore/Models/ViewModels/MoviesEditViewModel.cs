using System.ComponentModel;

namespace ASP.NETAssignmentMovieStore.Models.ViewModels
{
    public class MoviesEditViewModel : MoviesCreateViewModel
    {
        public int Id { get; set; }

        public string ExistingCoverImageURL { get; set; }

        [DisplayName("Movie Image")]
        public override IFormFile CoverMovieImage { get; set; }

    }
}
