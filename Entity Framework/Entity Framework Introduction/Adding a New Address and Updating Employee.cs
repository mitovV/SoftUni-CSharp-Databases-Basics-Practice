 public static string AddNewAddressToEmployee(SoftUniContext context)
{
    var sb = new StringBuilder();

    var address = new Address()
    {
        AddressText = "Vitoshka 15",
        TownId = 4
    };

    var employee = context
        .Employees
        .First(e => e.LastName == "Nakov");

        employee.Address = address;

        context.SaveChanges();

    var employeesTexts = context
        .Employees
        .OrderByDescending(e => e.AddressId)
        .Take(10)
        .Select(e => e.Address.AddressText)
        .ToArray();

    sb.Append(string.Join(Environment.NewLine, employeesTexts));

    return sb.ToString().TrimEnd();
}