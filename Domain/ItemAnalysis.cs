using System.Collections.Generic;

namespace Domain
{
    public class ItemAnalysis
    {
        public long delta { get; set; }

        public ItemPriceSnapshot mostRecentSnapshot { get; set; }

        public ItemDetail itemDetails { get; set; }

        public List<ItemPriceSnapshot> snapshotGraph { get; set; }

        public long prediction { get; set; }
    }
}
