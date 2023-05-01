namespace JobsApi.Repositories.Interfaces;

public interface IUnityOfWork
{
    ValueTask SaveChanges();
}