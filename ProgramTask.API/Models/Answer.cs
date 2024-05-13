namespace ProgramTask.API.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public List<string> Value { get; set; } = [];
    }
}
