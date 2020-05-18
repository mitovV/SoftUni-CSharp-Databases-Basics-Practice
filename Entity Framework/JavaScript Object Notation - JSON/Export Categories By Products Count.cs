public static string GetCategoriesByProductsCount(ProductShopContext context)
{
	var categories = context
		.Categories
		.OrderByDescending(c => c.CategoryProducts.Count)
		.Select(c => new
		{
			category = c.Name,
			productsCount = c.CategoryProducts.Count,
			averagePrice = $"{c.CategoryProducts.Sum(cp => cp.Product.Price) / c.CategoryProducts.Count:F2}",
			totalRevenue = $"{c.CategoryProducts.Sum(cp => cp.Product.Price):F2}"
		})
		.ToArray();
	
	return JsonConvert.SerializeObject(categories, Formatting.Indented);
}