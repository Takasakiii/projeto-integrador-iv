using JobsApi.Database;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Repositories;

[Repository(typeof(IJobRepository))]
public class JobRepository : BaseRepository<JobModel>, IJobRepository
{
    private readonly AppDbContext _context;

    public JobRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}