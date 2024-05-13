using Microsoft.EntityFrameworkCore;
using ProgramTask.API.Data;
using ProgramTask.API.Interfaces;
using ProgramTask.API.Models;

namespace ProgramTask.API.Services
{
    public class JobProgramRepository(ApplicationDbContext dbContext) : IJobProgramRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;


        public async Task CreateAsync(JobProgram model)
        {
            _dbContext.JobPrograms.Add(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(JobProgram model)
        {
            _dbContext.JobPrograms.Remove(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<JobProgram>> GetAsync()
        {
            return await _dbContext.JobPrograms
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<JobProgram?> GetAsync(Guid key)
        {
            return await _dbContext.FindAsync<JobProgram>(key);
        }

        public async Task UpdateAsync(JobProgram model)
        {
            _dbContext.JobPrograms.Update(model);
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateAllAsync(IEnumerable<JobProgram> models)
        {
            await _dbContext.JobPrograms.AddRangeAsync(models);
            await _dbContext.SaveChangesAsync();
        }
    }
}
