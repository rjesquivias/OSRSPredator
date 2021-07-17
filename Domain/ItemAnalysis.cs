using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ItemAnalysis
    {
        public long Id { get; set; }

        public long delta { get; set; }

        public ItemPriceSnapshot mostRecentSnapshot { get; set; }

        public ItemDetail itemDetails { get; set; }

        public List<ItemPriceSnapshot> snapshotGraph { get; set; }

        public long prediction { get; set; }
    }
}
