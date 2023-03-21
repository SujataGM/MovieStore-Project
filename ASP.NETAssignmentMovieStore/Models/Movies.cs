#nullable disable
using ASP.NETAssignmentMovieStore.Models.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETAssignmentMovieStore.Models


{
    public class Movies
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength =3)]
        public string Title { get; set; }

        [Required]
        [StringLength(100,MinimumLength =5)]
        public string Director { get; set; }

        [Required]
        [Display(Name ="Release Year")]
        public int ReleaseYear {get;set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        [StringLength(100,MinimumLength =3)]
        public string Category { get; set; }

        [Required]
        [StringLength(500,MinimumLength =5)]
        public string Description { get; set; }


        [Required]
        [DisplayName("Movie Time")]
        public int MovieTime { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }=DateTime.Now;

        public string CoverImageURL { get; set; }
        public virtual ICollection<OrderRows> OrderRows { get; set; }      

       
    }
   
    
}
