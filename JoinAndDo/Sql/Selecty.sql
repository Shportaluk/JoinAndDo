USE JoinAndDo


SELECT * FROM Users
SELECT * FROM Joins
SELECT * FROM Messages
SELECT * FROM My_accession
SELECT * FROM Deals_accession
SELECT * FROM Accession
SELECT * FROM Role
SELECT * FROM ListUsersInAccession

SELECT Login, RoleName FROM Role WHERE IdAccession = 1

SELECT * FROM Messages
SELECT LastName, FirstName FROM Users

SELECT DISTINCT ToLogin FROM Messages WHERE Login = 'asd'
SELECT DISTINCT Login FROM Messages WHERE ToLogin = 'asd'

select CONSTRAINT_NAME
from INFORMATION_SCHEMA.TABLE_CONSTRAINTS
where TABLE_NAME = 'Users'

SELECT * FROM My_accession WHERE Login = 'Anonymus'
