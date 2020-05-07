public static string GetEmployeesFullInformation(SoftUniContext context)
{
    var sb = new StringBuilder();

    var employyes = context
        .Employees
        .OrderBy(e => e.EmployeeId)
        .Select(e => sb.AppendLine($"{string.Join(" ", e.FirstName, e.LastName, e.MiddleName)} {e.JobTitle} {e.Salary:F2}"))
        .ToArray();

    return sb.ToString().TrimEnd();
}