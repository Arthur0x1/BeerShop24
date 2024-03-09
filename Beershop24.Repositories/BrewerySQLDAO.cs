using Beershop24.Domains.Entities;
using Microsoft.Data.SqlClient;

namespace Beershop24.Repositories
{
	public class BrewerySQLDAO
	{
		private const string connstring = "Server=.\\SQL22_VIVES; Database=BierSQL;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
		private readonly SqlConnection _conn;

		public BrewerySQLDAO() => _conn = new SqlConnection(connstring);

		public Task Add(Brewery entity)
		{
			throw new NotImplementedException();
		}

		public Task Delete(Brewery entity)
		{
			throw new NotImplementedException();
		}

		public Task<Brewery> FindById(int Id)
		{
			throw new NotImplementedException();
		}

		public Task Update(Brewery entity)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Brewery>?> GetAllAsync()
		{
			// Set up your connection  
			await _conn.OpenAsync();

			SqlCommand cmd = new SqlCommand("SELECT * FROM Breweries", _conn);
			SqlDataReader reader = await cmd.ExecuteReaderAsync();
			List<Brewery> breweryObjects = new List<Brewery>();
			while (await reader.ReadAsync())
			{
				var brewery = new Brewery
				{
					Brouwernr = Convert.ToInt16(reader["Brouwernr"]),
					Naam = reader["Naam"].ToString() ?? string.Empty, // Null-Coalescing Operator
					Adres = reader["Adres"].ToString() ?? string.Empty,
					Postcode = reader["Postcode"].ToString() ?? string.Empty,
					Gemeente = reader["Gemeente"].ToString() ?? string.Empty,
				};

				breweryObjects.Add(brewery);

			}

			_conn.Close();
			return breweryObjects;
		}

		public Task AddAsync(Brewery entity)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(Brewery entity)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Brewery entity)
		{
			throw new NotImplementedException();
		}

		public Task<Brewery?> FindByIdAsync(int Id)
		{
			throw new NotImplementedException();
		}
	}
}