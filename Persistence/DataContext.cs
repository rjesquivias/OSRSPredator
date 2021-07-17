using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ItemDetail> ItemDetails { get; set; }

        public DbSet<ItemHistorical> ItemHistoricals { get; set; }

        public DbSet<ItemPriceSnapshot> ItemPriceSnapshots { get; set; }

        public DbSet<SimpleItemAnalysis> WatchList { get; set; }
    }
}
