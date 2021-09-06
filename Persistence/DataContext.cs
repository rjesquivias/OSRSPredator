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

        public DbSet<ItemDetails> ItemDetails { get; set; }

        public DbSet<ItemHistorical> ItemHistoricals { get; set; }

        public DbSet<ItemPriceSnapshot> ItemPriceSnapshots { get; set; }

        public DbSet<UserWatchList> UserWatchList { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserWatchList>(x => x.HasKey(aa => new { aa.AppUserId, aa.ItemDetailsId}));

            builder.Entity<UserWatchList>()
                .HasOne(u => u.AppUser)
                .WithMany(a => a.UserWatchList)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<UserWatchList>()
                .HasOne(u => u.ItemDetails)
                .WithMany(a => a.UserWatchList)
                .HasForeignKey(aa => aa.ItemDetailsId);
        }
    }
}
