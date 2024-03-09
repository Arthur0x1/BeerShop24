namespace Beershop24.Services
{
	public interface IService<T> where T : class
	{
		Task AddAsync(T entity);
		Task<IEnumerable<T>?> GetAllAsync();
		Task Delete(T entity);
		Task DeleteAsync(T entity);
		Task Update(T entity);
		Task<T?> FindByIdAsync(int id);
	}
}
