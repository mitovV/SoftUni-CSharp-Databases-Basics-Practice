SELECT
  CASE 
  WHEN Age < 11 THEN '[0-10]'
  WHEN Age < 21 THEN '[11-20]'
  WHEN Age < 31 THEN '[21-30]'
  WHEN Age < 41 THEN '[31-40]'
  WHEN Age < 51 THEN '[41-50]'
  WHEN Age < 61 THEN '[51-60]'
  ELSE '[61+]'
END AS AgeGroup,
	COUNT(Age) AS WizardCount
  FROM    WizzardDeposits
GROUP BY  CASE 
  WHEN Age < 11 THEN '[0-10]'
  WHEN Age < 21 THEN '[11-20]'
  WHEN Age < 31 THEN '[21-30]'
  WHEN Age < 41 THEN '[31-40]'
  WHEN Age < 51 THEN '[41-50]'
  WHEN Age < 61 THEN '[51-60]'
  ELSE '[61+]'
END