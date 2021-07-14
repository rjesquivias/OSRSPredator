using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application.Generators
{
    public interface IWatchListGenerator : IGenerator
    {    
    }

    public class WatchListGenerator : IWatchListGenerator
    {
        public Task<List<SimpleItemAnalysis>> Generate(int pageSize, int page)
        {
            return Task.Run(() => {
                List<SimpleItemAnalysis> simpleItemAnalyses = new List<SimpleItemAnalysis> {
                    new SimpleItemAnalysis {
                        delta = 20,
                        mostRecentSnapshot = new ItemPriceSnapshot
                        {
                            Id = Guid.NewGuid(),
                            high = 29798000,
                            highTime = 1625275729,
                            low = 29500001,
                            lowTime = 1625269077
                        },
                        itemDetails = new ItemDetail
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
                        prediction = 999
                    }
                };
                return simpleItemAnalyses;
            });
        }
    }
}