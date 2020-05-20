public static string ImportParts(CarDealerContext context, string inputJson)
{
	var parts = JsonConvert
		.DeserializeObject<Part[]>(inputJson)
		.Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
		.ToArray();

	context.AddRange(parts);
	context.SaveChanges();

	return $"Successfully imported {parts.Length}.";
}