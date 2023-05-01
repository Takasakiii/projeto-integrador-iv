using JobsApi.Database;
using JobsApi.Repositories.Interfaces;

namespace JobsApi.Repositories;

public class BaseRepository<TModel, TKey> : IBaseRepository<TModel, TKey> where TModel : class
{
    private readonly AppDbContext _context;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async ValueTask Add(TModel model)
    {
        await _context.Set<TModel>().AddAsync(model);
    }

    public async Task<TModel?> GetById(TKey id)
    {
        return await _context.Set<TModel>().FindAsync(id);
    }

    public void Update(TModel model)
    {
        _context.Set<TModel>().Update(model);
    }

    public void Delete(TModel model)
    {
        _context.Set<TModel>().Remove(model);
    }
}

public class BaseRepository<TModel> : BaseRepository<TModel, uint>, IBaseRepository<TModel> where TModel : class
{
    protected BaseRepository(AppDbContext context) : base(context)
    {
    }
}