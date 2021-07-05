using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.ItemDetails.Any() || context.ItemHistoricals.Any() || context.ItemPriceSnapshots.Any()) return;

            var itemDetails = new List<ItemDetail>
            {
                new ItemDetail
                {
                    examine = "Fabulously ancient mage protection enchanted in the 3rd Age.",
                    Id = 10344,
                    members = true,
                    lowalch = 20200,
                    limit = 8,
                    value = 50500,
                    highalch = 30300,
                    icon = "3rd age amulet.png",
                    name = "3rd age amulet"
                },
                new ItemDetail
                {
                    examine = "A beautifully crafted axe, shaped by ancient smiths.",
                    Id = 20011,
                    members = true,
                    lowalch = 22000,
                    limit = 40,
                    value = 55000,
                    highalch = 33000,
                    icon = "3rd age axe.png",
                    name = "3rd age axe"
                },
                new ItemDetail
                {
                    examine = "A beautifully crafted bow carved by ancient archers.",
                    Id = 12424,
                    members = true,
                    lowalch = 60000,
                    limit = 8,
                    value = 150000,
                    highalch = 90000,
                    icon = "3rd age bow.png",
                    name = "3rd age bow"
                },
                new ItemDetail
                {
                    examine = "A beautiful cloak woven by ancient tailors.",
                    Id = 12437,
                    members = true,
                    lowalch = 34000,
                    limit = 8,
                    value = 85000,
                    highalch = 51000,
                    icon = "3rd age cloak.png",
                    name = "3rd age cloak"
                },
                new ItemDetail
                {
                    examine = "A fabulously ancient vine cloak as worn by the druids of old.",
                    Id = 23345,
                    members = true,
                    lowalch = 80000,
                    limit = 8,
                    value = 200000,
                    highalch = 120000,
                    icon = "3rd age druidic cloak.png",
                    name = "3rd age druidic cloak"
                }
            };

            var ItemHistoricals = new List<ItemHistorical>
            {
                new ItemHistorical
                {
                    Id = 10344,
                    historical = new List<ItemPriceSnapshot>
                    {
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 29798000,
                            highTime = 1625275729,
                            low = 29500001,
                            lowTime = 1625269077
                        },
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 29798000,
                            highTime = 1625275729,
                            low = 29500001,
                            lowTime = 1625269077
                        },
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 29798000,
                            highTime = 1625275729,
                            low = 29500001,
                            lowTime = 1625269077
                        }
                    }
                },
                new ItemHistorical
                {
                    Id = 20011,
                    historical = new List<ItemPriceSnapshot>
                    {
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 864998998,
                            highTime = 1625273362,
                            low = 860050001,
                            lowTime = 1625259125
                        },
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 864998998,
                            highTime = 1625273362,
                            low = 860050001,
                            lowTime = 1625259125
                        },
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 864998998,
                            highTime = 1625273362,
                            low = 860050001,
                            lowTime = 1625259125
                        }
                    }
                },
                new ItemHistorical
                {
                    Id = 12424,
                    historical = new List<ItemPriceSnapshot>
                    {
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 1682221999,
                            highTime = 1625259934,
                            low = 1650000000,
                            lowTime = 1625260623
                        },
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 1682221999,
                            highTime = 1625259934,
                            low = 1650000000,
                            lowTime = 1625260623
                        },
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 1682221999,
                            highTime = 1625259934,
                            low = 1650000000,
                            lowTime = 1625260623
                        }
                    }
                },
                new ItemHistorical
                {
                    Id = 12437,
                    historical = new List<ItemPriceSnapshot>
                    {
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 430999988,
                            highTime = 1625285270,
                            low = 420566000,
                            lowTime = 1625224325
                        },
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 430999988,
                            highTime = 1625285270,
                            low = 420566000,
                            lowTime = 1625224325
                        },
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 430999988,
                            highTime = 1625285270,
                            low = 420566000,
                            lowTime = 1625224325
                        }
                    }
                },
                new ItemHistorical
                {
                    Id = 23345,
                    historical = new List<ItemPriceSnapshot>
                    {
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 1370000000,
                            highTime = 1625246789,
                            low = 1338832836,
                            lowTime = 1625245351
                        },
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 1370000000,
                            highTime = 1625246789,
                            low = 1338832836,
                            lowTime = 1625245351
                        },
                        new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 1370000000,
                            highTime = 1625246789,
                            low = 1338832836,
                            lowTime = 1625245351
                        }
                    }
                }
            };

            await context.ItemDetails.AddRangeAsync(itemDetails);
            await context.ItemHistoricals.AddRangeAsync(ItemHistoricals);
            await context.SaveChangesAsync();
        }
    }
}
