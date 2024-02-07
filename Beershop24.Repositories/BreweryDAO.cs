
using Beershop24.Domains.Data;
using Beershop24.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beershop24.Repositories
{
    public class BreweryDAO
    {
        private readonly BeerDbContext _db;  // Namespace using BierSQL.Domein.Entities; toevoegen bovenaan

        public BreweryDAO()
        {
            _db = new BeerDbContext();
        }
        public async Task<IEnumerable<Brewery?>> GetAllAsync()
        {
            try
            {
                return await _db.Breweries.ToListAsync(); // volgende Namespaces toevoegen bovenaan using System.Linq; using Microsoft.EntityFrameworkCore;
            }
            catch (Exception ex)
            { throw; }
        }
    }
}
