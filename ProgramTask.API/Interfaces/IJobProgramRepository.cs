using ProgramTask.API.Models;

namespace ProgramTask.API.Interfaces
{
    public interface IJobProgramRepository : IAsyncCrudRepository<Guid, JobProgram>
    {
    }
}
