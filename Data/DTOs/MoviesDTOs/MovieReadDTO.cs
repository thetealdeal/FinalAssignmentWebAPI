using FinalAssignmentWebAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FinalAssignmentWebAPI.Data.DTOs.MoviesDTOs
{
    public class MovieReadDTO
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

        [AllowNull]
        public int[]? Characters { get; set; }

        [AllowNull]
        public int? FranchiseId { get; set; }

    }
}
