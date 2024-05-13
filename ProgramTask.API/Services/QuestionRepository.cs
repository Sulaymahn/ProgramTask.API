using Microsoft.EntityFrameworkCore;
using ProgramTask.API.Data;
using ProgramTask.API.Interfaces;
using ProgramTask.API.Models;

namespace ProgramTask.API.Services
{
    public class QuestionRepository(ApplicationDbContext dbContext) : IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task CreateAsync(Question model)
        {
            _dbContext.Questions.Add(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Question model)
        {
            _dbContext.Questions.Remove(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Question>> GetAsync()
        {
            return await _dbContext.Questions
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Question?> GetAsync(Guid key)
        {
            return await _dbContext.FindAsync<Question>(key);
        }

        public async Task UpdateAsync(Question model)
        {
            _dbContext.Questions.Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAllAsync(IEnumerable<Question> models)
        {
            await _dbContext.Questions.AddRangeAsync(models);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Question?> FindFromProgramId(Guid questionId, Guid programId)
        {
            return await _dbContext.Questions.FirstOrDefaultAsync(q => q.Id == questionId && q.JobProgramId == programId);
        }

        public async Task<List<Question>> FindFromProgramId(Guid programId)
        {
            return await _dbContext.Questions
                .Where(q => q.JobProgramId == programId)
                .ToListAsync();
        }
    }
}
