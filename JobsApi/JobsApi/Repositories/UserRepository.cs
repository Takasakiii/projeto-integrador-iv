﻿using JobsApi.Database;
using JobsApi.Dtos;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Lina.UtilsExtensions;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Repositories;

[Repository(typeof(IUserRepository))]
public class UserRepository : BaseRepository<UserModel>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserModel?> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<(IEnumerable<UserModel>, int)> Filter(UserFilterDto filter)
    {
        var query = _context.Users.Where(x => !filter.Type.HasValue || (UserModelType)filter.Type.Value == x.Type);

        var users = await query
            .Paginate(filter.Page ?? 1, filter.PageSize ?? 30)
            .ToListAsync();
        var count = await query.CountAsync();

        return (users, count);
    }

    public new async Task<UserModel?> GetById(uint id)
    {
        return await _context.Users
            .Include(x => x.Skills)!
            .ThenInclude(x => x.Skill)
            .Include(x => x.Works)!
            .ThenInclude(x => x.Skills)!
            .ThenInclude(x => x.Skill)
            .Include(x => x.Jobs)!
            .ThenInclude(x => x.Skills)!
            .ThenInclude(x => x.Skill)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<UserModel?> GetByIdNoIncludes(uint id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<SkillCountDto>> GetSkillCount()
    {
        return await _context.Users.Include(x => x.Skills)
            .Where(x => x.Type == UserModelType.Professional)
            .GroupBy(x => x.Skills == null ? 0 : x.Skills.ToList().Count)
            .Select(x => new SkillCountDto($"{x.Key} Skill{(x.Key > 1 ? "s" : "")}", x.Count()))
            .ToListAsync();
    }
}