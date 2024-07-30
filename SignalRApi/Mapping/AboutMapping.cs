using AutoMapper;
using SignalR.DtoLayer.AboutDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping;

public class AboutMapping : Profile
{
    public AboutMapping()
    {
        // About sınıfı ve ResultAboutDto sınıfı arasında mapping işlemi yapılır.
        CreateMap<About, ResultAboutDto>().ReverseMap(); // ReverseMap() metodu ile iki sınıf arasında çift yönlü mapping işlemi yapılır.
        CreateMap<About, CreateAboutDto>().ReverseMap();
        CreateMap<About, GetAboutDto>().ReverseMap();
        CreateMap<About, UpdateAboutDto>().ReverseMap();
    }
}