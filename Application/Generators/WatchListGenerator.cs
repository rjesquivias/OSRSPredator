using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Generators
{
    public interface IWatchListGenerator : IGenerator
    {
    }

    public class WatchListGenerator : IWatchListGenerator
    {
        private readonly DataContext _context;
        public WatchListGenerator(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.SimpleItemAnalysis>> Generate(int pageSize, int page)
        {
            return await _context.WatchList.ToListAsync();
        }
    }
}