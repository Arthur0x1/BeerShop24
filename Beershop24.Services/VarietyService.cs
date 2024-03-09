
using Beershop24.Repositories;
using Beershop24.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beershop24.Services
{
    public class VarietyService : IService<Variety>
    {
        private readonly IDAO<Variety> _varietyDAO;

        public VarietyService(IDAO<Variety> varietyDAO) => _varietyDAO = varietyDAO;

        public async Task<IEnumerable<Variety>> GetAllAsync()
        {
            return await _varietyDAO.GetAllAsync();
        }

        public async Task AddAsync(Variety variety) => throw new NotImplementedException();

        public async Task Delete(Variety variety) => throw new NotImplementedException();

        public async Task DeleteAsync(Variety variety) => throw new NotImplementedException();

        public async Task Update(Variety variety) => throw new NotImplementedException();

        public async Task<Variety?> FindByIdAsync(int id) => throw new NotImplementedException();
    }
}
