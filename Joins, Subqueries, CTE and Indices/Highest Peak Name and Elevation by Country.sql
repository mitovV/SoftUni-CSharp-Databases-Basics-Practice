   SELECT TOP(5)c.CountryName AS Country, 
		  ISNULL(p.PeakName, '(no highest peak)'),
          ISNULL(MAX(p.Elevation), 0) AS HighestPeakElevation,
		  ISNULL(m.MountainRange, '(no mountain)')
     FROM Countries c
LEFT JOIN MountainsCountries mc
       ON c.CountryCode = mc.CountryCode
LEFT JOIN Mountains m
	   ON m.Id =  mc.MountainId
LEFT JOIN Peaks p
	   ON p.MountainId = m.Id
 GROUP BY c.CountryName, p.PeakName,m.MountainRange
 ORDER BY c.CountryName, p.PeakName