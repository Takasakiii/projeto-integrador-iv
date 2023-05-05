using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Exceptions;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Visus.Cuid;

namespace JobsApi.Services;

[Service(typeof(IImageService))]
public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ImageService(IImageRepository imageRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _imageRepository = imageRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Create(IFormFile file)
    {
        if (file.Length > 1024 * 1024 * 4) throw new ImageException("Image too large");

        if (!file.ContentType.StartsWith("image/")) throw new ImageException("Not sported type");

        var buffer = new byte[file.Length];
        _ = await file.OpenReadStream().ReadAsync(buffer);

        {
            using var imageEditor = Image.Load(buffer);

            var stream = new MemoryStream();
            await imageEditor.SaveAsJpegAsync(stream);
            buffer = stream.ToArray();
        }

        var id = new Cuid2(32);

        await _imageRepository.Add(new ImageModel(id.ToString(), "image/jpeg", buffer));
        await _unitOfWork.SaveChanges();

        return id.ToString();
    }

    public async Task<ImageDto> GetById(string id)
    {
        var model = await _imageRepository.GetById(id);

        if (model is null)
            throw new NotFoundException("Image", id);

        return _mapper.Map<ImageDto>(model);
    }
}