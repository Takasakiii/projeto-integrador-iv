using JobsApi.Database;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Repositories;

[Repository(typeof(IJobSkillRepository))]
public class JobSkillRepository : BaseRepository<JobSkillModel>, IJobSkillRepository
{
    private readonly AppDbContext _context;

    public JobSkillRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<JobSkillModel>> GetByJob(uint jobId)
    {
        return await _context.JobSkills.AsNoTracking().Where(x => x.JobId == jobId).ToListAsync();
    }

    public void DeleteRange(IEnumerable<JobSkillModel> models)
    {
        _context.JobSkills.RemoveRange(models);
    }
}