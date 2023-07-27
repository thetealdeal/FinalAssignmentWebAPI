using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FinalAssignmentWebAPI.Data.DTOs.CharacterDTOs
{
    public class CharacterCreateDTO
    {
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Alias { get; set; }

        [MaxLength(50)]
        public string? Gender { get; set; }

        [MaxLength(250)]
        public string? Picture { get; set; }
    }
}
