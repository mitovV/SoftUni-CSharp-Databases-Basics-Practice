public static string GetUsersWithProducts(ProductShopContext context)
{
	var users = context
		.Users
		.Where(u => u.ProductsSold.Any(p => p.Buyer != null))
		.OrderByDescending(u => u.ProductsSold.Count(p => p.Buyer != null))
		.Select(u => new
		{
			firstName = u.FirstName,
			lastName = u.LastName,
			age = u.Age,
			soldProducts = new
			{
				count = u.ProductsSold.Count(p => p.Buyer != null),
                products = u.ProductsSold
                .Where(p => p.Buyer != null)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price
                })
                .ToArray()
            }
        })
		.ToArray();

	var result = new
	{
			usersCount = users.Length,
			users = users
	};

	return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
	{
                NullValueHandling = NullValueHandling.Ignore
	});
}