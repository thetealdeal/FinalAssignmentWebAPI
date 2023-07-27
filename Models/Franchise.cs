using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FinalAssignmentWebAPI.Models
{
    public class Franchise
    {
        
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }



        // FK franchise one(franchise) to many(movies)
        [AllowNull]
        public ICollection<Movie>? Movies { get; set; }
    }
}
