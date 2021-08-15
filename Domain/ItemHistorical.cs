using System.Collections.Generic;

namespace Domain
{
    public class ItemHistorical
    {
        public long Id { get; set; }

        public List<ItemPriceSnapshot> historical { get; set; }
    }
}
