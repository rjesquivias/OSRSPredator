using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DefaultItemDetails> ItemDetails { get; set; }

        public DbSet<WatchListItemDetails> WatchList { get; set; }

        public DbSet<ItemHistorical> ItemHistoricals { get; set; }

        public DbSet<ItemPriceSnapshot> ItemPriceSnapshots { get; set; }
    }
}
