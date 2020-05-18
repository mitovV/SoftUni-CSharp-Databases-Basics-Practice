public static string GetSoldProducts(ProductShopContext context)
{
	var users = context
		.Users
		.Where(u => u.ProductsSold.Any(s => s.Buyer != null))
		.Select(u => new
		{
			firstName = u.FirstName,
			lastName = u.LastName,
			soldProducts = u.ProductsSold
			.Where(p => p.Buyer != null)
			.Select(p => new
		{
			name = p.Name,
			price = p.Price,
			buyerFirstName = p.Buyer.FirstName,
			buyerLastName = p.Buyer.LastName
		})
		.ToArray()


		})
		.OrderBy(u => u.lastName)
		.ThenBy(u => u.firstName)
		.ToArray();

	return JsonConvert.SerializeObject(users, Formatting.Indented);
}