public static string GetBookTitlesContaining(BookShopContext context, string input)
{
	input = input.ToLower();

	var titles = context
		.Books
		.Where(b => b.Title.ToLower().Contains(input))
		.Select(b => b.Title)
		.OrderBy(b => b)
		.ToArray();

	return String.Join(Environment.NewLine, titles);
}