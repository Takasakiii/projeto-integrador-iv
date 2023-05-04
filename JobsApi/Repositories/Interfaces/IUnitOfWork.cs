namespace JobsApi.Repositories.Interfaces;

public interface IUnitOfWork
{
    ValueTask SaveChanges();
}