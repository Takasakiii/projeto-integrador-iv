using JobsApi.Database;
using JobsApi.Dtos;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Repositories;

[Repository(typeof(IWorkRepository))]
public class WorkRepository : BaseRepository<WorkModel>, IWorkRepository
{
    private readonly AppDbContext _context;

    public WorkRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkModel>> List(WorkFilterDto workFilter)
    {
        return await _context.Works.Where(x => !workFilter.UserId.HasValue || x.UserId == workFilter.UserId)
            .ToListAsync();
    }
}