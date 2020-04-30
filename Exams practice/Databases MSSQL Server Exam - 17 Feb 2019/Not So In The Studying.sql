   SELECT CONCAT(s.FirstName, ' ', s.MiddleName + ' ', s.LastName) AS [Full Name] 
     FROM Students s
LEFT JOIN StudentsSubjects ss
	   ON ss.StudentId = s.Id
	WHERE ss.StudentId IS NULL
 ORDER BY [Full Name]