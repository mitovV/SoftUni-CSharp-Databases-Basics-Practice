namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;

    using TeisterMask.Data.Models;
    using TeisterMask.DataProcessor.ImportDto;
    using System.Globalization;
    using TeisterMask.Data.Models.Enums;
    using Castle.Core.Internal;
    using Newtonsoft.Json;
    using System.Linq;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportProjectDto[]), new XmlRootAttribute("Projects"));

            var dtos = (ImportProjectDto[])serializer.Deserialize(new StringReader(xmlString));
            var projects = new List<Project>();

            var sb = new StringBuilder();
            var tasks = new List<Task>();

            foreach (var dto in dtos)
            {
                if (IsValid(dto))
                {
                    Project project;

                    if (!dto.DueDate.IsNullOrEmpty())
                    {
                        project = new Project
                        {
                            Name = dto.Name,
                            OpenDate = DateTime.ParseExact(dto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            DueDate = DateTime.ParseExact(dto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        };
                        context.Projects.Add(project);
                    }
                    else
                    {
                        project = new Project
                        {
                            Name = dto.Name,
                            OpenDate = DateTime.ParseExact(dto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        };
                        context.Projects.Add(project);
                    }

                    context.SaveChanges();
                    var tasksCount = 0;

                    foreach (var taskDto in dto.Tasks)
                    {
                        if (IsValid(taskDto))
                        {
                            var task = new Task
                            {
                                Name = taskDto.Name,
                                OpenDate = DateTime.ParseExact(taskDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                DueDate = DateTime.ParseExact(taskDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                ExecutionType = (ExecutionType)taskDto.ExecutionType,
                                LabelType = (LabelType)taskDto.LabelType,
                                ProjectId = project.Id
                            };

                            if (task.OpenDate.Ticks < project.OpenDate.Ticks)
                            {
                                sb.AppendLine(ErrorMessage);
                            }
                            else if (project.DueDate != null)
                            {
                                var ticks = (DateTime)project.DueDate;

                                if (task.DueDate.Ticks > ticks.Ticks)
                                {
                                    sb.AppendLine(ErrorMessage);
                                }
                                else
                                {
                                    tasks.Add(task);
                                    tasksCount++;
                                }
                            }
                            else
                            {
                                tasks.Add(task);
                                tasksCount++;
                            }
                        }
                        else
                        {
                            sb.AppendLine(ErrorMessage);
                        }
                    }

                    sb.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, tasksCount));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Tasks.AddRange(tasks);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employees = JsonConvert.DeserializeObject<ImportEmployeeDto[]>(jsonString);

            var sb = new StringBuilder();
            var employeesTasks = new List<EmployeeTask>();

            foreach (var employeeDto in employees)
            {
                if (IsValid(employeeDto))
                {
                    var taskIds = employeeDto
                        .Tasks
                        .Select(t => t)
                        .Distinct()
                        .ToArray();

                    var employee = new Employee
                    {
                        Username = employeeDto.Username,
                        Email = employeeDto.Email,
                        Phone = employeeDto.Phone
                    };

                    context.Employees.Add(employee);
                    context.SaveChanges();

                    var tasksCount = 0;

                    foreach (var taskId in taskIds)
                    {
                        if (context.Tasks.Any(t => t.Id == taskId))
                        {
                            var et = new EmployeeTask
                            {
                                EmployeeId = employee.Id,
                                TaskId = taskId
                            };

                            tasksCount++;
                            employeesTasks.Add(et);
                        }
                        else
                        {
                            sb.AppendLine(ErrorMessage);
                        }
                    }

                    sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username, tasksCount));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.EmployeesTasks.AddRange(employeesTasks);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}