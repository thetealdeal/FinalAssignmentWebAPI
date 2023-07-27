using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FinalAssignmentWebAPI.Models
{
    public class Movie
    {
        
        public int Id { get; set; }

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

        // FK franchise one(franchise) to many(movies) 
        [AllowNull]
        public int? FranchiseId { get; set; }

        [AllowNull]
        public Franchise? Franchise { get; set; }

        // many(Character) to many(Movie)
        [AllowNull]
        public ICollection<Character>? Characters { get; set; }

    }
}
