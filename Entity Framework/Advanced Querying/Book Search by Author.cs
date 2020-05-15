public static string GetBooksByAuthor(BookShopContext context, string input)
{
	input = input.ToLower();

	var books = context
		.Books
		.Where(b => b.Author.LastName.ToLower().StartsWith(input))
		.OrderBy(b => b.BookId)
		.Select(b => new
		{
			b.Title,
			FullName = $"{b.Author.FirstName} {b.Author.LastName}"
		})
		.ToArray();

	var sb = new StringBuilder();

	foreach (var book in books)
	{
		sb.AppendLine($"{book.Title} ({book.FullName})");
	}

	return sb.ToString().TrimEnd();
}