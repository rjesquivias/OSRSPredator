using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ItemDetail
    {
        [Required]
        [MaxLength(200)]
        public string examine { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [Required]
        public bool members { get; set; }

        [Required]
        public long lowalch { get; set; }

        [Required]
        public long limit { get; set; }

        [Required]
        public long value { get; set; }

        [Required]
        public long highalch { get; set; }

        [Required]
        [MaxLength(50)]
        public string icon { get; set; }

        [Required]
        [MaxLength(50)]
        public string name { get; set; }
    }
}
