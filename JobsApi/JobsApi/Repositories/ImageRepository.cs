using JobsApi.Database;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Repositories;

[Repository(typeof(IImageRepository))]
public class ImageRepository : BaseRepository<ImageModel, string>, IImageRepository
{
    private readonly AppDbContext _context;

    public ImageRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}