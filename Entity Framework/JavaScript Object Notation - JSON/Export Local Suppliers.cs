public static string GetLocalSuppliers(CarDealerContext context)
{
	var suppliers = context
		.Suppliers
		.Where(s => s.IsImporter == false)
		.Select(s => new
		{
			s.Id,
			s.Name,
			PartsCount = s.Parts.Count
		})
		.ToArray();

	return JsonConvert.SerializeObject(suppliers, Formatting.Indented);
}