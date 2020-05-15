public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
{
	var names = context
		.Authors
		.Where(a => a.FirstName.EndsWith(input))
		.Select(a => $"{a.FirstName} {a.LastName}")
		.OrderBy(a => a)
		.ToList();

	return String.Join(Environment.NewLine, names);
}