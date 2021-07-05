using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ItemHistorical
    {
        public long Id { get; set; }

        public ICollection<ItemPriceSnapshot> historical { get; set; }
    }
}
