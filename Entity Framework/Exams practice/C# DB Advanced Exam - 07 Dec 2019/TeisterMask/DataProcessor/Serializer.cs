namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using ExportDto;

    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context
                 .Projects
                 .Where(p => p.Tasks.Count() > 0)
                 .Select(p => new ExportProjectWithTaskDto
                 {
                     TasksCount = p.Tasks.Count(),
                     ProjectName = p.Name,
                     HasEndDate = p.DueDate == null ? "No" : "Yes",
                     Tasks = p.Tasks.Select(t => new ExportTaskDto
                     {
                         Name = t.Name,
                         Label = t.LabelType.ToString()
                     })
                     .OrderBy(t => t.Name)
                     .ToArray()
                 })
                 .OrderByDescending(p => p.TasksCount)
                 .ThenBy(p => p.ProjectName)
                 .ToArray();


            var serializer = new XmlSerializer(typeof(ExportProjectWithTaskDto[]), new XmlRootAttribute("Projects"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            serializer.Serialize(new StringWriter(sb), projects, namespaces);

            return sb.ToString().Trim();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
                .Employees
                .Where(e => e.EmployeesTasks.Any(et => et.Task.OpenDate.Ticks >= date.Ticks))
                .Select(e => new ExportMostBusiestEmployeeDto
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                    .Select(et => et.Task)
                    .Where(t => t.OpenDate.Ticks >= date.Ticks)
                    .OrderByDescending(t => t.DueDate)
                    .ThenBy(t => t.Name)
                    .Select(t => new ExportMostBusiestEmployeeTaskDto
                    {
                        TaskName = t.Name,
                        OpenDate = t.OpenDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                        DueDate = t.DueDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                        LabelType = t.LabelType.ToString(),
                        ExecutionType = t.ExecutionType.ToString()
                    })
                    .ToArray()
                })
                .ToArray()
                .OrderByDescending(e => e.Tasks.Count())
                .ThenBy(e => e.Username)
                .Take(10);

            return JsonConvert.SerializeObject(employees, Formatting.Indented);
        }
    }
}