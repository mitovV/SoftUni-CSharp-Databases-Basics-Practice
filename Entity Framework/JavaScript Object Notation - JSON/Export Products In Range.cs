public static string GetProductsInRange(ProductShopContext context)
{
	var products = context
		.Products
		.Where(p => p.Price > 500 && p.Price <= 1000)
		.Select(p => new
		{
			name = p.Name,
			price = p.Price,
			seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
		})
		.OrderBy(p => p.price);

	return JsonConvert.SerializeObject(products, Formatting.Indented);
}