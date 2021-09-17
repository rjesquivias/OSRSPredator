using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ItemPriceSnapshot
    {
        [MaxLength(70)]
        public String Id { get; set; }

        public long high { get; set; }

        public long highTime { get; set; }

        public long low { get; set; }

        public long lowTime { get; set; }

        public ICollection<ItemHistoricalList> ItemHistoricalList { get; set; } = new List<ItemHistoricalList>();
    }
}
