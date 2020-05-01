  SELECT q.FirstName, q.LastName , q.Grade
    FROM (SELECT FirstName, LastName, Grade,
				 ROW_NUMBER() OVER(PARTITION BY FirstName, LastName ORDER BY Grade DESC) AS RankGrade
		    FROM Students s
			JOIN StudentsSubjects ss
			  ON ss.StudentId = s.Id) AS q
   WHERE q.RankGrade = 2
ORDER BY q.FirstName, q.LastName