public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
{
    var sb = new StringBuilder();

    var employees = context
        .Employees
        .Where(e => e.Department.Name == "Research and Development")
        .Select(e => new
        {
            e.FirstName,
            e.LastName,
            departmentName = e.Department.Name,
            e.Salary
        })
        .OrderBy(e => e.Salary)
        .ThenByDescending(e => e.FirstName)
        .ToArray();

    foreach (var employee in employees)
    {
        sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.departmentName} - ${employee.Salary:F2}");
    }

    return sb.ToString().TrimEnd();
}