using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
public class ItemDetails
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

        public long prediction { get; set; }

        public ICollection<UserWatchList> UserWatchList { get; set; } = new List<UserWatchList>();

        public ICollection<ItemHistoricalList> ItemHistoricalList { get; set; } = new List<ItemHistoricalList>();
    }
}
