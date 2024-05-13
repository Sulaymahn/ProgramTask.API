namespace ProgramTask.API.Models
{
    public class CandidateApplication
    {
        public Guid Id { get; set; }
        public Guid JobProgramId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public List<Answer> Answers { get; set; } = [];
    }
}
