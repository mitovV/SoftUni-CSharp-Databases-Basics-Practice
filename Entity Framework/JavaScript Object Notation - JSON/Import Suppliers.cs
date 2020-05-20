public static string ImportSuppliers(CarDealerContext context, string inputJson)
{
	var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

	context.AddRange(suppliers);
	context.SaveChanges();

	return $"Successfully imported {suppliers.Length}.";
}