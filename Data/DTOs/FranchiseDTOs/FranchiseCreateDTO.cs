using System.ComponentModel.DataAnnotations;

namespace FinalAssignmentWebAPI.Data.DTOs.FranchiseDTOs
{
    public class FranchiseCreateDTO
    {

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }
    }
}
