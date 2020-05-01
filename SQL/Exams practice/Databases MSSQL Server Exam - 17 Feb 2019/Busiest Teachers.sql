  SELECT TOP(10) t.FirstName, t.LastName,
		 COUNT(st.StudentId) AS StudentsCount
    FROM Teachers t
    JOIN StudentsTeachers st
	  ON st.TeacherId = t.Id
GROUP BY FirstName, LastName
ORDER BY StudentsCount DESC, FirstName, LastName