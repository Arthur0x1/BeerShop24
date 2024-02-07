
using Beershop24.Repositories;
using Beershop24.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beershop24.Services
{
    public class VarietyService
    {
        private VarietyDAO varietyDAO;
        public VarietyService()
        {
            varietyDAO = new VarietyDAO();
        }

        public async Task<IEnumerable<Variety>> GetAllAsync()
        {
            return await varietyDAO.GetAllAsync();
        }
    }
}
