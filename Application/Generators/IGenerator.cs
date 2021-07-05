﻿using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Generators
{
    public interface IGenerator
    {
        Task<List<SimpleItemAnalysis>> Generate();
    }
}
