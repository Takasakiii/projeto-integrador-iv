using JobsApi.Database;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Repositories;

[Repository(typeof(ISkillRepository))]
public class SkillRepository : BaseRepository<SkillModel>, ISkillRepository
{
    private readonly AppDbContext _context;

    public SkillRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}