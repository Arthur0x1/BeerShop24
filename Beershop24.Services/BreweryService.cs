

using Beershop24.Repositories;
using Beershop24.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beershop24.Services
{
    public class BreweryService : IService<Brewery>
    {
        private readonly IDAO<Brewery> _breweryDAO;

        public BreweryService(IDAO<Brewery> breweryDAO) => _breweryDAO = breweryDAO;

        public async Task AddAsync(Brewery brewery) => throw new NotImplementedException();

        public async Task<IEnumerable<Brewery?>> GetAllAsync()
        {
            return await _breweryDAO.GetAllAsync();
        }

        public async Task Delete(Brewery brewery) => throw new NotImplementedException();

        public async Task DeleteAsync(Brewery brewery) => throw new NotImplementedException();

        public async Task Update(Brewery brewery) => throw new NotImplementedException();

		public async Task<Brewery?> FindByIdAsync(int id) => throw new NotImplementedException();
	}
}
