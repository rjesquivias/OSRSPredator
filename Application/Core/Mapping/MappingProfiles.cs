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
            CreateMap<ItemHistoricalList, SnapshotDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(
                    s => s.ItemPriceSnapshot.Id
                ))
                .ForMember(d => d.high, o => o.MapFrom(
                    s => s.ItemPriceSnapshot.high
                ))
                .ForMember(d => d.highTime, o => o.MapFrom(
                    s => s.ItemPriceSnapshot.highTime
                ))
                .ForMember(d => d.low, o => o.MapFrom(
                    s => s.ItemPriceSnapshot.low
                ))
                .ForMember(d => d.lowTime, o => o.MapFrom(
                    s => s.ItemPriceSnapshot.lowTime
                ));

            CreateMap<Domain.ItemDetails, ItemDetailsDTO>();

            CreateMap<ItemPriceSnapshot, SnapshotDTO>();

            CreateMap<Domain.ItemDetails, ItemDto>()
                .ForMember(d => d.mostRecentSnapshot, o => o.MapFrom(
                    s => s.UserWatchList.FirstOrDefault(item => item.ItemDetailsId == s.Id).MostRecentSnapshot));
        }
    }
}