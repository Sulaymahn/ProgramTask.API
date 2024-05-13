using ProgramTask.API.Models;

namespace ProgramTask.API.DataTransferObjects
{
    public class QuestionForClient
    {
        public string Text { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<string> AllowedAnswers { get; set; } = [];
        public int? MinAnswerCount { get; set; }
        public int? MaxAnswerCount { get; set; }
    }
}
