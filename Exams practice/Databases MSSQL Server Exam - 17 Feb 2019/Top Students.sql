  SELECT TOP(10) FirstName,LastName, CAST(AVG(Grade) AS DECIMAL(3,2)) AS Grade
    FROM Students s
    JOIN StudentsExams se
	  ON se.StudentId = s.Id
GROUP BY FirstName,LastName
ORDER BY Grade DESC, FirstName,LastName