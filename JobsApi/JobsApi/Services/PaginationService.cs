using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Microsoft.Extensions.Primitives;

namespace JobsApi.Services;

[Service(typeof(IPaginationService))]
public class PaginationService : IPaginationService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public PaginationService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void SetCount(int count)
    {
        _contextAccessor.HttpContext?.Response.Headers.Add(
            new KeyValuePair<string, StringValues>("x-total-items", count.ToString()));
    }
}