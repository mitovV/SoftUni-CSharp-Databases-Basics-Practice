public static string GetGoldenBooks(BookShopContext context)
{
	var titles = context
		.Books
		.Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
		.OrderBy(b => b.BookId)
		.Select(b => b.Title)
		.ToArray();

	return String.Join(Environment.NewLine, titles);
}