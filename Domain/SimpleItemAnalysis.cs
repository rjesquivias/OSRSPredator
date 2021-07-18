using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SimpleItemAnalysis
    {
        public long Id { get; set; }
        
        public long delta { get; set; }

        [ForeignKey("snapshotId")]
        public ItemPriceSnapshot mostRecentSnapshot { get; set; }

        [ForeignKey("detailsId")]
        public ItemDetail itemDetails { get; set; }

        public long prediction { get; set; }

        public string snapshotId { get; set; }

        public long detailsId { get; set; }
    }
}
