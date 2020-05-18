public static string ImportCategories(ProductShopContext context, string inputJson)
{
	var products = JsonConvert.DeserializeObject<Category[]>(inputJson)
		.Where(p => p.Name != null)
		.ToArray();

	context.AddRange(products);
	context.SaveChanges();

	return $"Successfully imported {products.Length}";
}