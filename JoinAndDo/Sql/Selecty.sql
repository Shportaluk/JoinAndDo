USE JoinAndDo

SELECT * FROM Messages
SELECT DISTINCT ToLogin FROM Messages WHERE Login = 'Anonymus'

SELECT Login FROM Users WHERE Id = 2

-- вибирає повідомлення які надіслав 'Anonymus' -> 'asd'
SELECT * FROM Messages WHERE Id_user = 2 and Name = 'Anonymus'
SELECT * FROM Messages WHERE Id_user = 1 and Name = 'asd'
