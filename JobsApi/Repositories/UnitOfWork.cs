using JobsApi.Database;
using JobsApi.Exceptions;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Lina.UtilsExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;

namespace JobsApi.Repositories;

[Repository(typeof(IUnitOfWork))]
public class UnitOfWork : IUnitOfWork
{
    private const int Duplicate = 1062;
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask SaveChanges()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            var sqlException = ex.GetInnerException<MySqlException>();

            if (sqlException is null || sqlException.Number != Duplicate) throw;

            var message = sqlException.Message;
            throw new DuplicateException(message);
        }
    }
    
    public async Task<IDbContextTransaction> Begin()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }
    
    public void ClearContext()
    {
        _dbContext.ChangeTracker.Clear();
    }
}