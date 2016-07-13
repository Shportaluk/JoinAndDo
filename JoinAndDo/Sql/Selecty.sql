USE JoinAndDo



SELECT * FROM Messages
SELECT LastName, FirstName FROM Users

SELECT DISTINCT ToLogin FROM Messages WHERE Login = 'asd'
SELECT DISTINCT Login FROM Messages WHERE ToLogin = 'asd'

SELECT * FROM My_accession WHERE Login = 'Anonymus'
