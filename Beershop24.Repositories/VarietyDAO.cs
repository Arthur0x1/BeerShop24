
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
    public class VarietyDAO : IDAO<Variety>
    {
        private readonly BeerDbContext _dbContext;  // Namespace using BierSQL.Domein.Entities; toevoegen bovenaan

        public VarietyDAO(BeerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Variety>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Varieties.ToListAsync(); // volgende Namespaces toevoegen bovenaan using System.Linq; using Microsoft.EntityFrameworkCore;
            }
            catch (Exception ex)
            { throw; }
        }

        public async Task Delete(Variety variety) => throw new NotImplementedException();
		
        public async Task DeleteAsync(Variety variety) => throw new NotImplementedException();
		
        public async Task Update(Variety variety) => throw new NotImplementedException();
		
        public async Task<Variety?> FindByIdAsync(int id) => throw new NotImplementedException();
	}
}
