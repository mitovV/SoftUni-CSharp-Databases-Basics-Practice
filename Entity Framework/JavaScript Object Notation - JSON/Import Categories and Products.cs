public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
{
	var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

	context.AddRange(categoryProducts);
	context.SaveChanges();

	return $"Successfully imported {categoryProducts.Length}";
}