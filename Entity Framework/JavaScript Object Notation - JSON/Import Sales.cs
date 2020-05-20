public static string ImportSales(CarDealerContext context, string inputJson)
{
	var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

	context.AddRange(sales);
	context.SaveChanges();

	return $"Successfully imported {sales.Length}.";
}