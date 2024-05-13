using System.ComponentModel.DataAnnotations;

namespace ProgramTask.API.DataTransferObjects
{
    public class CandidateApplicationForCreation
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        public List<CandidateAnswerForCreation> Answers { get; set; } = [];
    }
}
