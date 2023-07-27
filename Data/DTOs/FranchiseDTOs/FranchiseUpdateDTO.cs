using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FinalAssignmentWebAPI.Data.DTOs.FranchiseDTOs
{
    public class FranchiseUpdateDTO
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }

    }
}
