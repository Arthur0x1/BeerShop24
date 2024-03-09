
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
    public class BreweryDAO : IDAO<Brewery>
    {
        private readonly BeerDbContext _dbContext;  // Namespace using BierSQL.Domein.Entities; toevoegen bovenaan

        public BreweryDAO(BeerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Brewery?>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Breweries.ToListAsync(); // volgende Namespaces toevoegen bovenaan using System.Linq; using Microsoft.EntityFrameworkCore;
            }
            catch (Exception ex)
            { throw; }
        }

		public async Task Delete(Brewery brewery) => throw new NotImplementedException();

		public async Task DeleteAsync(Brewery brewery) => throw new NotImplementedException();

		public async Task Update(Brewery brewery) => throw new NotImplementedException();

		public async Task<Brewery?> FindByIdAsync(int id) => throw new NotImplementedException();

	}
}
