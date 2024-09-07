using App.Data.Model.Entities.General;
using App.Data.Model.SystemEntities;
using App.Data.ViewModels.Dto;

namespace App.Data.Mapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<Client, ApiResultPagerModel<ClientDto>>();
        CreateMap<ClientDto, Client>().ForMember(c => c.Id, opt => opt.Ignore());

        CreateMap<Unit, UnitDto>();
        CreateMap<Unit, ApiResultPagerModel<UnitDto>>();
        CreateMap<UnitDto, Unit>().ForMember(c => c.Id, opt => opt.Ignore());

    }
}