   SELECT CONCAT(s.FirstName, ' ', s.LastName) AS [Full Name] 
     FROM Students s
LEFT JOIN StudentsExams se
       ON se.StudentId = s.Id
	WHERE se.StudentId IS NULL
 ORDER BY [Full Name]
