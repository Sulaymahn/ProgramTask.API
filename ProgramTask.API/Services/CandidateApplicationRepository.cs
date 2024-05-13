using Microsoft.EntityFrameworkCore;
using ProgramTask.API.Data;
using ProgramTask.API.Interfaces;
using ProgramTask.API.Models;

namespace ProgramTask.API.Services
{
    public class CandidateApplicationRepository(ApplicationDbContext dbContext) : ICandidateApplicationRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task CreateAsync(CandidateApplication model)
        {
            _dbContext.CandidateApplications.Add(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CandidateApplication model)
        {
            _dbContext.CandidateApplications.Remove(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CandidateApplication>> GetAsync()
        {
            return await _dbContext.CandidateApplications
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CandidateApplication?> GetAsync(Guid key)
        {
            return await _dbContext.FindAsync<CandidateApplication>(key);
        }

        public async Task UpdateAsync(CandidateApplication model)
        {
            _dbContext.CandidateApplications.Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAllAsync(IEnumerable<CandidateApplication> models)
        {
            await _dbContext.CandidateApplications.AddRangeAsync(models);
            await _dbContext.SaveChangesAsync();
        }
    }
}
