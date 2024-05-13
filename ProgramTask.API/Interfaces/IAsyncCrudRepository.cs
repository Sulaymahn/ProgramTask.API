namespace ProgramTask.API.Interfaces
{
    public interface IAsyncCrudRepository<TKey, TModel>
    {
        Task<List<TModel>> GetAsync();
        Task<TModel?> GetAsync(TKey key);
        Task DeleteAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task CreateAsync(TModel model);
        Task CreateAllAsync(IEnumerable<TModel> models);
    }
}
