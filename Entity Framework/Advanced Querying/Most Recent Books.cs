public static string GetMostRecentBooks(BookShopContext context)
{
	var categories = context
		.Categories
		.Select(c => new
		{
			c.Name,
			Books = c.CategoryBooks
			.OrderByDescending(b => b.Book.ReleaseDate)
			.Take(3)
			.Select(cb => new
			{
				cb.Book.Title,
				cb.Book.ReleaseDate.Value.Year
			})

			.OrderByDescending(b => b.Year)
			.ToArray()
		})
		.OrderBy(c => c.Name)
		.ToArray();

	var sb = new StringBuilder();

	foreach (var c in categories)
	{
		sb.AppendLine($"--{c.Name}");

		foreach (var b in c.Books)
		{
			sb.AppendLine($"{b.Title} ({b.Year})");
		}
	}

	return sb.ToString().TrimEnd();
}