DELETE 
  FROM RepositoriesContributors
 WHERE RepositoryId IN (SELECT Id 
						  FROM Repositories
						 WHERE [Name] = 'Softuni-Teamwork')

DELETE 
  FROM Issues
 WHERE RepositoryId IN (SELECT Id 
						  FROM Repositories
						 WHERE [Name] = 'Softuni-Teamwork')

UPDATE Files
   SET ParentId = NULL
 WHERE Id IN (SELECT ContributorId 
				FROM Commits
				WHERE RepositoryId  = (SELECT Id 
										 FROM Repositories
										WHERE [Name] = 'Softuni-Teamwork'))

DELETE 
  FROM Files
 WHERE CommitId IN (SELECT Id 
					  FROM Commits
					 WHERE RepositoryId  = (SELECT Id 
											  FROM Repositories
											 WHERE [Name] = 'Softuni-Teamwork'))

DELETE
  FROM Commits
 WHERE RepositoryId IN (SELECT Id 
						  FROM Repositories
						 WHERE [Name] = 'Softuni-Teamwork')

DELETE 
  FROM Repositories
 WHERE [Name] = 'Softuni-Teamwork'