  SELECT i.Id,
	     CONCAT(u.Username,' : ', i.Title) AS IssueAssignee
    FROM Users u
    JOIN Issues i
      ON i.AssigneeId = u.Id
ORDER BY i.Id DESC, i.AssigneeId