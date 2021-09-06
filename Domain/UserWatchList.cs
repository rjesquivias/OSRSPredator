namespace Domain
{
    public class UserWatchList
    {
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public long ItemDetailsId { get; set; }

        public ItemDetails ItemDetails { get; set; }

        public ItemPriceSnapshot MostRecentSnapshot { get; set; }
    }
}
