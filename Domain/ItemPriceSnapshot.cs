using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ItemPriceSnapshot
    {
        [MaxLength(70)]
        public Guid Id { get; set; }

        public long high { get; set; }

        public long highTime { get; set; }

        public long low { get; set; }

        public long lowTime { get; set; }
    }
}
