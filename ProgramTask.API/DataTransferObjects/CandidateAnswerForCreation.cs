namespace ProgramTask.API.DataTransferObjects
{
    public class CandidateAnswerForCreation
    {
        public Guid QuestionId { get; set; }
        public List<string> Answer { get; set; } = [];
    }
}
