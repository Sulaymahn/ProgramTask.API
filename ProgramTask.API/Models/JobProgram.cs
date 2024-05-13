namespace ProgramTask.API.Models
{
    public class JobProgram
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Field<string> Phone { get; set; } = new();
        public Field<string> Nationality { get; set; } = new();
        public Field<string> CurrentResidence { get; set; } = new();
        public Field<string> IdNumber { get; set; } = new();
        public Field<DateTime?> DateOfBirth { get; set; } = new();
        public Field<Gender?> Gender { get; set; } = new();
    }
}
