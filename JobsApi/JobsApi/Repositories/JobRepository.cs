using JobsApi.Database;
using JobsApi.Dtos;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Repositories;

[Repository(typeof(IJobRepository))]
public class JobRepository : BaseRepository<JobModel>, IJobRepository
{
    private readonly AppDbContext _context;

    public JobRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobModel>> ListWithIncludes()
    {
        return await _context.Jobs
            .Include(x => x.Skills)!
            .ThenInclude(x => x.Skill)
            .Include(x => x.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<JobLevelCountDto>> GetJobLevelCount()
    {
        return await _context.Jobs
            .Include(x => x.Skills)
            .GroupBy(x => x.Level)
            .Select(x => new JobLevelCountDto((JobDtoLevel)x.Key, x.Count(), (int)x.Average(y => y.Value)))
            .ToListAsync();
    }
}