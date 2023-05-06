using JobsApi.Dtos;
using JobsApi.Models;

namespace JobsApi.Repositories.Interfaces;

public interface IWorkRepository : IBaseRepository<WorkModel>
{
    Task<IEnumerable<WorkModel>> List(WorkFilterDto workFilter);
}