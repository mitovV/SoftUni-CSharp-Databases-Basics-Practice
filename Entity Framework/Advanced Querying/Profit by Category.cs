public static string GetTotalProfitByCategory(BookShopContext context)
{
	var result = context
		.Categories
		.Select(c => new
		{ 
			c.Name,
			Profit = c.CategoryBooks
			.Select(cb => cb.Book.Price * cb.Book.Copies)
			.Sum()
		})
		.OrderByDescending(r => r.Profit)
		.ThenBy(r => r.Name)
		.ToArray();

	var sb = new StringBuilder();

	foreach (var item in result)
	{
		sb.AppendLine($"{item.Name} ${item.Profit:F2}");
	}

	return sb.ToString().TrimEnd();
}