 public static string GetBooksByAgeRestriction(BookShopContext context, string command)
{
	command = command.ToLower();

	var titles = context
		.Books
		.Where(b => b.AgeRestriction.ToString().ToLower() == command)
		.Select(b => b.Title)
		.OrderBy(b => b)
		.ToArray();

	return String.Join(Environment.NewLine, titles);
}