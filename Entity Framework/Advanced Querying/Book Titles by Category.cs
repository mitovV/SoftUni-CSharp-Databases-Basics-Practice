public static string GetBooksByCategory(BookShopContext context, string input)
{
	var categories = input
		.ToLower()
		.Split(" ", StringSplitOptions.RemoveEmptyEntries);

	var titles = context
		.Books
		.Where(c => c.BookCategories.Any(bc =>categories.Contains( bc.Category.Name.ToLower())))
		.Select(b => b.Title)
		.OrderBy(b => b)
	.ToArray();

	return String.Join(Environment.NewLine, titles);
}