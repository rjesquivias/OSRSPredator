using System.Linq;
using Application.DTOs;
using Application.ItemDetails;
using AutoMapper;
using Domain;

namespace Application.Core.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ItemPriceSnapshot, SnapshotDTO>();

            CreateMap<Domain.ItemDetails, ItemDto>()
                .ForMember(d => d.mostRecentSnapshot, o => o.MapFrom(
                    s => s.UserWatchList.FirstOrDefault(item => item.ItemDetailsId == s.Id).MostRecentSnapshot));
        }
    }
}