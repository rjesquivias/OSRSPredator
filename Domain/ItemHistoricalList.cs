using System;

namespace Domain
{
    // Represents a many-to-many relationship between 
    // ItemPriceSnapshot & ItemDetails
    public class ItemHistoricalList
    {
        public String ItemPriceSnapshotId { get; set; }

        public ItemPriceSnapshot ItemPriceSnapshot { get; set; }

        public long ItemDetailsId { get; set; }

        public ItemDetails ItemDetails { get; set; }
    }
}
