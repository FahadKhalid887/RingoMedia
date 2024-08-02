using AutoMapper;
using RingoMedia.Application.DTOs;
using RingoMedia.Domain.Entities;
using RingoMedia.Web.Models.ViewModels;

namespace RingoMedia.Web.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DepartmentDto, CreateEditDepartmentVM>()
                .ForMember(dest => dest.DepartmentLogo, opt => opt.Ignore());
        }
    }
}
