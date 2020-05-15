public static void IncreasePrices(BookShopContext context)
{
	context
		.Books
		.Where(b => b.ReleaseDate.Value.Year < 2010)
		.ToList()
		.ForEach(b => b.Price += 5);

	context.SaveChanges();
}