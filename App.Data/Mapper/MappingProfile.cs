using App.Data.Model.Entities.General;
using App.Data.ViewModels.Dto;
using AutoMapper;

namespace App.Data.Mapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<ClientDto, Client>().ForMember(c => c.Id, opt => opt.Ignore());

    }
}