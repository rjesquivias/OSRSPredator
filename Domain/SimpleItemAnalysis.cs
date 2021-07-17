using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SimpleItemAnalysis
    {
        public long Id { get; set; }
        
        public long delta { get; set; }

        public ItemPriceSnapshot mostRecentSnapshot { get; set; }

        public ItemDetail itemDetails { get; set; }

        public long prediction { get; set; }
    }
}
