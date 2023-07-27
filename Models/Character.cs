using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FinalAssignmentWebAPI.Models
{
    public class Character
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Alias { get; set; }

        [MaxLength(50)]
        public string? Gender { get; set; }

        [MaxLength(250)]
        public string? Picture { get; set; }

        // many(Character) to many(Movie)
        [AllowNull]
        public ICollection<Movie>? Movies { get; set; } 
    }
}
