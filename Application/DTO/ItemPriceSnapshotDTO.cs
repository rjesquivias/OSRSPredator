using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
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
