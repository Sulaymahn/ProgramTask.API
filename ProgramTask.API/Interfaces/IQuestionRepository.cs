using ProgramTask.API.Models;

namespace ProgramTask.API.Interfaces
{
    public interface IQuestionRepository : IAsyncCrudRepository<Guid, Question>
    {
        Task<Question?> FindFromProgramId(Guid questionId, Guid programId);
        Task<List<Question>> FindFromProgramId(Guid programId);
    }
}
