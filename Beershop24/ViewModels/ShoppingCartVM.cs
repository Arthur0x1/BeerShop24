namespace Beershop24.ViewModels
{
	public class ShoppingCartVM
	{
		public List<CartVM>? Cart { get; set; }
	}

	public class CartVM
	{
		public int Biernr { get; set; }
		public string? Naam { get; set; }
		public int Aantal { get; set; }
		public decimal Prijs { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
