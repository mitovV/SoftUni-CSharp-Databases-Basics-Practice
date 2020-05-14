 public static string GetBooksByPrice(BookShopContext context)
{
	var sb = new StringBuilder();

	var books = context
		.Books
		.Where(b => b.Price > 40)
		.Select(b => new
		{
			b.Title,
			b.Price
		})
		.OrderByDescending(b => b.Price)
		.ToArray();

	foreach (var book in books)
	{
		sb.AppendLine($"{book.Title} - ${book.Price:F2}");
	}

	return sb.ToString().TrimEnd();
}