﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Entities;
namespace Tournaments.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAllAsync(bool IncludeGames=false);
        
        Task<Tournament> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Tournament tournament);
        void Update(Tournament tournament);
        void Remove(Tournament tournament);
    }
}
