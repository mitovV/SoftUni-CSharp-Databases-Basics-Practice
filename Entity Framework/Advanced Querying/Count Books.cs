public static int CountBooks(BookShopContext context, int lengthCheck)
{
	var count = context
		.Books
		.Count(b => b.Title.Length > lengthCheck);

	return count;
}