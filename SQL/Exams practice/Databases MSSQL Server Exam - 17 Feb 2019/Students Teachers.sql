   SELECT FirstName, LastName,
		  COUNT(TeacherId) AS TeachersCount
     FROM Students s
LEFT JOIN StudentsTeachers st
	   ON st.StudentId = s.Id
 GROUP BY FirstName, LastName