using AutoMapper;
using RingoMedia.Application.DTOs;
using RingoMedia.Domain.Entities;

namespace RingoMedia.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DepartmentEntity, DepartmentDto>()
                .ForMember(dest => dest.SubDepartments, opt => opt.MapFrom(src => src.SubDepartments));
            CreateMap<DepartmentDto, DepartmentEntity>()
                .ForMember(dest => dest.SubDepartments, opt => opt.Ignore());

            CreateMap<ReminderEntity, ReminderDto>()
                .ForMember(dest => dest.DateTime,
                       opt => opt.MapFrom(src => src.DateTime.ConvertUtcToTimeZone()));

            CreateMap<ReminderDto, ReminderEntity>();
        }
    }
}
