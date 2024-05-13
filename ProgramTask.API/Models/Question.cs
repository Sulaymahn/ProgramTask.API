namespace ProgramTask.API.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public Guid JobProgramId { get; set; }
        public string Text { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public List<string> AllowedAnswers { get; set; } = [];
        public int? MinAnswerCount { get; set; }
        public int? MaxAnswerCount { get; set; }
    }
}
