using System.Linq;
using Application.ItemDetails;
using AutoMapper;

namespace Application.Core.Mapping {
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Domain.ItemDetails, ItemDto>()
                .ForMember(d => d.mostRecentSnapshot, o => o.MapFrom(
                    s => s.UserWatchList.FirstOrDefault(item => item.ItemDetailsId == s.Id).MostRecentSnapshot));
        }
    }
}