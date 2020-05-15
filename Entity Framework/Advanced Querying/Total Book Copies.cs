public static string CountCopiesByAuthor(BookShopContext context)
{
	var authors = context
		.Authors
		.Select(a => new
		{
			FullName = $"{a.FirstName} {a.LastName}",
			Copies = a.Books.Sum(b => b.Copies)
		})
		.OrderByDescending(a => a.Copies)
		.ToArray();

	var sb = new StringBuilder();

	foreach (var author in authors)
	{
		sb.AppendLine($"{author.FullName} - {author.Copies}");
	}

	return sb.ToString().TrimEnd();
}