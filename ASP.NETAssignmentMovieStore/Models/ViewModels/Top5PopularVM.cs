using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace ASP.NETAssignmentMovieStore.Models.ViewModels
{
    public class Top5PopularVM
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public string Director { get; set; }

        public double Price { get; set; }

        public string CoverImageURL { get; set; }
    }
}
