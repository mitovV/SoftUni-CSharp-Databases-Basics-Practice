public static string GetBooksNotReleasedIn(BookShopContext context, int year)
{
	var titles = context
		.Books
		.OrderBy(b => b.BookId)
		.Where(b => b.ReleaseDate.Value.Year != year)
		.Select(b => b.Title)
		.ToArray();

	return String.Join(Environment.NewLine, titles);
}