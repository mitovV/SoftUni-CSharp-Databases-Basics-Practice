public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
{
    var sb = new StringBuilder();

    var employees = context
        .Employees
        .Where(e => e.Salary > 50000)
        .Select(e => new 
        { 
            e.FirstName,
            e.Salary
        })
        .ToArray()
        .OrderBy(e => e.FirstName);


    foreach (var employee in employees)
    {
        sb.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
    }
                

    return sb.ToString().TrimEnd();
}