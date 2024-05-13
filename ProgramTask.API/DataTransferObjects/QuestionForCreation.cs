using System.ComponentModel.DataAnnotations;

namespace ProgramTask.API.DataTransferObjects
{
    public class QuestionForCreation
    {
        [Required]
        public string Text { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
        public List<string>? AllowedAnswers { get; set; } = [];
        public int? MinAnswerCount { get; set; }
        public int? MaxAnswerCount { get; set; }
    }
}
