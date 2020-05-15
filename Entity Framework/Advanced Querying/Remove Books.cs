public static int RemoveBooks(BookShopContext context)
{
	var booksForDelete = context
		.Books
		.Where(b => b.Copies < 4200)
		.ToList();

	context.RemoveRange(booksForDelete);
	context.SaveChanges();

	return booksForDelete.Count;
}