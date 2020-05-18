public static string ImportProducts(ProductShopContext context, string inputJson)
{
	var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

	context.AddRange(products);
	context.SaveChanges();

	return $"Successfully imported {products.Length}";
}