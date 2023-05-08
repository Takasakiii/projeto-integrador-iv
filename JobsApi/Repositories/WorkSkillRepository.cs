using JobsApi.Database;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Repositories;

[Repository(typeof(IWorkSkillRepository))]
public class WorkSkillRepository : BaseRepository<WorkSkillModel>, IWorkSkillRepository
{
    private readonly AppDbContext _context;

    public WorkSkillRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}