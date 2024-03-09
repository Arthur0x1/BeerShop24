

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
    public class BeerDAO : IDAO<Beer>
    {
        private readonly BeerDbContext _dbContext;  // Namespace using BierSQL.Domein.Entities; toevoegen bovenaan

        public BeerDAO(BeerDbContext dbContext) => _dbContext = dbContext; 

        public async Task<IEnumerable<Beer>?> GetAllAsync()
        {
            try
            {// select * from Bieren
                return await _dbContext.Beers
                    .Include(b => b.BrouwernrNavigation)
                    .Include(b => b.SoortnrNavigation)
                    .ToListAsync(); // volgende Namespaces toevoegen bovenaan using System.Linq; using Microsoft.EntityFrameworkCore;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw;

            }
        }


        public async Task<Beer?> GetAsync(int id)
        {
            try
            {

                return await _dbContext.Beers.Where(b => b.Biernr == id).Include(b => b.BrouwernrNavigation).Include(b => b.SoortnrNavigation).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO beer"); }
        }

        //        An entity may be in one of the following states:

        //Added.The entity does not yet exist in the database.The SaveChanges method must issue an INSERT statement.
        //Unchanged.Nothing needs to be done with this entity by the SaveChanges method.When you read an entity from the database, the entity starts out with this status.
        //Modified.Some or all of the entity's property values have been modified. The SaveChanges method must issue an UPDATE statement.
        //Deleted.The entity has been marked for deletion.The SaveChanges method must issue a DELETE statement.
        //Detached.The entity isn't being tracked by the database context.



        public async Task AddAsync(Beer entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }


        }

        public Task Update(Beer beer) => throw new NotImplementedException();

        public async Task UpdateAsync(Beer entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task Delete(Beer beer)
        {
            _dbContext.Entry(beer).State = EntityState.Deleted;
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task DeleteAsync(Beer beer)
        {
            _dbContext.Entry(beer).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Beer?> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}




