using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class ImageModelToImageDto : DynamicMapperProfile<ImageModel, ImageDto>
{
    protected override void Map(IMappingExpression<ImageModel, ImageDto> mappingExpression)
    {
    }
}