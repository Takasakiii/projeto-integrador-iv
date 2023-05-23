using Microsoft.EntityFrameworkCore.Storage;

namespace JobsApi.Repositories.Interfaces;

public interface IUnitOfWork
{
    ValueTask SaveChanges();
    Task<IDbContextTransaction> Begin();
}