using ProgramTask.API.Models;

namespace ProgramTask.API.Interfaces
{
    public interface ICandidateApplicationRepository : IAsyncCrudRepository<Guid, CandidateApplication>
    {
    }
}
