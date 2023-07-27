using System.ComponentModel.DataAnnotations;

namespace FinalAssignmentWebAPI.Data.DTOs.MoviesDTOs
{
    public class MovieCreateDTO
    {

     
        [MaxLength(50)]
        public string? MovieTitle { get; set; }

        [MaxLength(50)]
        public string? Genre { get; set; }

        public int? ReleaseYear { get; set; }

        [MaxLength(50)]
        public string? Director { get; set; }

        [MaxLength(250)]
        public string? Picture { get; set; }

        [MaxLength(150)]
        public string? Trailer { get; set; }


    }
}
