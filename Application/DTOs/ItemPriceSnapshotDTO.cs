using System.Collections.Generic;

namespace Application.DTOs
{
    public class ItemPriceSnapshotDTO
    {
        public Dictionary<string, PriceSnapshot> data;
    }

    public class PriceSnapshot
    {
        public long? high;
        public long? highTime;
        public long? low;
        public long? lowTime;
    }
}
