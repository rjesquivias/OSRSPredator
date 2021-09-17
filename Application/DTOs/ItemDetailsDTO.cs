using System.Collections.Generic;
using Domain;

namespace Application.DTOs
{
    public class ItemDetailsDTO
    {
        public string examine { get; set; }

        public long Id { get; set; }

        public bool members { get; set; }

        public long lowalch { get; set; }

        public long limit { get; set; }

        public long value { get; set; }

        public long highalch { get; set; }

        public string icon { get; set; }

        public string name { get; set; }

        public long prediction { get; set; }

        public ICollection<SnapshotDTO> ItemHistoricalList { get; set; } = new List<SnapshotDTO>();
    }
}