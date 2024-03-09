
using Beershop24.Repositories;
using Beershop24.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beershop24.Services
{
    public class BeerService : IService<Beer>
    {
        private readonly IDAO<Beer> _beerDAO;

        public BeerService(IDAO<Beer> dao) => _beerDAO = dao;

        public async Task<IEnumerable<Beer>?> GetAllAsync() => await _beerDAO.GetAllAsync();
       
        public async Task Delete(Beer beer) => throw new NotImplementedException();

        public async Task<Beer?> GetAsync(int id) => await _beerDAO.FindByIdAsync(id);

        public async Task AddAsync(Beer entity) => throw new NotImplementedException(); /*await _beerDAO.AddAsync(entity); */

        public Task Update(Beer beer) => throw new NotImplementedException();

		public async Task UpdateAsync(Beer entity) => await _beerDAO.Update(entity);

		public async Task DeleteAsync(Beer beer) => await _beerDAO.DeleteAsync(beer);

        public async Task<Beer?> FindByIdAsync(int id) => throw new NotImplementedException();
    }
}
