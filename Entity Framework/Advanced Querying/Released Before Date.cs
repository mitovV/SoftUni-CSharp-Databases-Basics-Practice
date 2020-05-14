public static string GetBooksReleasedBefore(BookShopContext context, string date)
{
	var dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", null);

	var books = context
		.Books
		.Where(b => b.ReleaseDate < dateTime)
		.OrderByDescending(b => b.ReleaseDate)
		.Select(b => new
		{
			b.Title,
			b.EditionType,
			b.Price
		})
		.ToArray();

	var sb = new StringBuilder();

	foreach (var b in books)
	{
		sb.AppendLine($"{b.Title} - {b.EditionType} - ${b.Price:F2}");
	}

	return sb.ToString().TrimEnd();
}