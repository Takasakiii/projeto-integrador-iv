﻿using JobsApi.Configs;
using JobsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Database;

public class AppDbContext : DbContext
{
    private readonly IAppConfig _appConfig;
    private readonly ILoggerFactory _loggerFactory;

    public AppDbContext(IAppConfig appConfig, ILoggerFactory loggerFactory)
    {
        _appConfig = appConfig;
        _loggerFactory = loggerFactory;
    }

    public DbSet<UserModel> Users => Set<UserModel>();
    public DbSet<SkillModel> Skills => Set<SkillModel>();
    public DbSet<ImageModel> Images => Set<ImageModel>();
    public DbSet<WorkModel> Works => Set<WorkModel>();
    public DbSet<UserSkillModel> UsersSkills => Set<UserSkillModel>();
    public DbSet<WorkSkillModel> WorksSkills => Set<WorkSkillModel>();
    public DbSet<JobModel> Jobs => Set<JobModel>();
    public DbSet<JobSkillModel> JobSkills => Set<JobSkillModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var version = ServerVersion.AutoDetect(_appConfig.Database.Url);
        optionsBuilder.UseMySql(_appConfig.Database.Url, version);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseLoggerFactory(_loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}