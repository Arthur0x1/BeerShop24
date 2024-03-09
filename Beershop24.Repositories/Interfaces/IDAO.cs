using System.Collections.Generic;

namespace Beershop24.Repositories
{
	public interface IDAO<T> where T : class
	{
		Task<IEnumerable<T>?> GetAllAsync();
		Task Delete(T entity);
		Task DeleteAsync(T entity);
		Task Update(T entity);
		Task<T?> FindByIdAsync(int id);
	}
}