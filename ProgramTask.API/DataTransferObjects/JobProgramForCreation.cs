using System.ComponentModel.DataAnnotations;

namespace ProgramTask.API.DataTransferObjects
{
    public class JobProgramForCreation
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public JobProgramFieldMetadata Phone { get; set; } = new();
        public JobProgramFieldMetadata Nationality { get; set; } = new();
        public JobProgramFieldMetadata CurrentResidence { get; set; } = new();
        public JobProgramFieldMetadata IdNumber { get; set; } = new();
        public JobProgramFieldMetadata DateOfBirth { get; set; } = new();
        public JobProgramFieldMetadata Gender { get; set; } = new();
        public List<QuestionForCreation> Questions { get; set; } = [];
    }
}
