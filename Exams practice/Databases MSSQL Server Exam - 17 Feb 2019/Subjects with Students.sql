  SELECT CONCAT(t.FirstName, ' ', t.LastName) AS FullName,
	     CONCAT(s.[Name], '-',s.Lessons) AS Subjects,
		 COUNT(st.StudentId) AS Students
    FROM Teachers t
    JOIN Subjects s
	  ON s.Id = t.SubjectId
	JOIN StudentsTeachers st
	  ON st.TeacherId = t.Id
GROUP BY CONCAT(t.FirstName, ' ', t.LastName), CONCAT(s.[Name], '-',s.Lessons)
ORDER BY Students DESC, FullName, Subjects
