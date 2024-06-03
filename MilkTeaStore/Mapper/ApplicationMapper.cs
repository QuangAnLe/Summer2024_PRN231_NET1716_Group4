using AutoMapper;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaStore.DTO.Create;
using MilkTeaStore.DTO.Update;
using MilkTeaStore.ViewModels;

namespace MilkTeaStore.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<TeaVM, Tea>().ReverseMap();
            CreateMap<TeaCreateDTO, Tea>().ReverseMap();
            CreateMap<TeaUpdateDTO, Tea>().ReverseMap();

            CreateMap<MaterialVM, Material>().ReverseMap();
            CreateMap<MaterialCreateDTO, Material>().ReverseMap();
            CreateMap<MaterialUpdateDTO, Material>().ReverseMap();

            CreateMap<UserVM, User>().ReverseMap().ForMember(dest => dest.RoleName,
                                       opt => opt.MapFrom(src => src.Role!.RoleName))
                                                    .ForMember(dest => dest.DistrictName,
                                       opt => opt.MapFrom(src => src.District!.DistrictName))
                                                    .ForMember(dest => dest.WardName,
                                       opt => opt.MapFrom(src => src.District!.WardName));
            CreateMap<UserCreateDTO, User>().ReverseMap();
            CreateMap<UserUpdateDTO, User>().ReverseMap();

            CreateMap<DistrictVM, District>().ReverseMap();
            CreateMap<DistrictCreateDTO, District>().ReverseMap();
            CreateMap<DistrictUpdateDTO, District>().ReverseMap();


        }
    }
}
