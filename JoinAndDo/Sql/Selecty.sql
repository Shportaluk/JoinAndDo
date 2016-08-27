USE JoinAndDo


SELECT * FROM Users
SELECT * FROM Joins
SELECT * FROM Messages
SELECT * FROM My_accession
SELECT * FROM Deals_accession
SELECT * FROM Accession
SELECT * FROM RoleOfUserInAccession
SELECT Login, RoleName FROM Role WHERE IdAccession = 45 and Login IS NOT NULL
SELECT * FROM RequestJoinToAccession
SELECT * FROM MessagesInAccession

SELECT Login, RoleName FROM Role WHERE IdAccession = 1

SELECT * FROM Messages
SELECT LastName, FirstName FROM Users

SELECT DISTINCT ToLogin FROM Messages WHERE Login = 'asd'
SELECT DISTINCT Login FROM Messages WHERE ToLogin = 'asd'


SELECT 
    'ALTER TABLE ' +  OBJECT_SCHEMA_NAME(parent_object_id) +
    '.[' + OBJECT_NAME(parent_object_id) + 
    '] DROP CONSTRAINT ' + name
FROM sys.foreign_keys
WHERE referenced_object_id = object_id('Accession')

SELECT 
    'ALTER TABLE ' +  OBJECT_SCHEMA_NAME(parent_object_id) +
    '.[' + OBJECT_NAME(parent_object_id) + 
    '] DROP CONSTRAINT ' + name
FROM sys.foreign_keys
WHERE referenced_object_id = object_id('RequestJoinToAccession')


SELECT * FROM My_accession WHERE Login = 'Anonymus'


SELECT * FROM Accession WHERE Login = 'qwe'
SELECT Login FROM Accession WHERE Id = 6

SELECT * FROM RequestJoinToAccession WHERE ToIdAccession = 7
