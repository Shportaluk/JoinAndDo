USE JoinAndDo


CREATE PROC Registration
  @login NVARCHAR(20),
  @pass NVARCHAR(20),
  @firstName NVARCHAR(20),
  @lastName NVARCHAR(20),
  @res NVARCHAR(30) OUTPUT
AS
SELECT @res = 's'
IF ((SELECT COUNT(*) FROM Users where Login = @login ) = 0)
BEGIN
	INSERT INTO Users VALUES( @login, @pass, @firstName, @lastName, null, 0, 0, 0 );
	SELECT @res = 'OK'
END
ELSE
	SELECT @res = 'user is already registered'
GO

DECLARE @res NVARCHAR(30)
EXEC Registration @login = 'Anonymus', @pass = '123456', @firstName = 'Andriy', @lastName = 'Shportaluk', @res = @res OUTPUT
SELECT @res





CREATE PROC GetCountMessages
  @login NVARCHAR(20),
  @hash NVARCHAR(100),
  @res INT OUTPUT
AS
SELECT @res = 0

IF (( SELECT COUNT(*) FROM Users WHERE Login = @login and Hash = @hash ) = 1 )
BEGIN
	SET @res = ( SELECT count(*) FROM Messages WHERE toLogin = @login );
END
SELECT @res
GO


SELECT COUNT(*) FROM Users WHERE Login = 'asd' and Hash = 'GPYymdpnnkue8JfzY/lVCg'
	SELECT count(*) FROM Messages WHERE Login = 'asd'

DECLARE @res INT
EXEC GetCountMessages @login = 'asd', @hash = 'GPYymdpnnkue8JfzY/lVCg', @res = @res OUTPUT
SELECT @res


CREATE PROC SendMsg
  @login NVARCHAR(20),
  @hash NVARCHAR(100),
  @to NVARCHAR(20),
  @text NVARCHAR(1000),
  @res NVARCHAR(20) OUTPUT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	INSERT INTO Messages VALUES( @login, @text, @to, GETDATE() );
	SELECT @res = 'OK'
END
ELSE
	SELECT @res = 'Error !!!'
GO

SELECT * FROM Users
SELECT * FROM Messages WHERE Id_user = 2

DECLARE @res NVARCHAR(20)
EXEC SendMsg @login = 'asd', @hash = 'GPYymdpnnkue8JfzY/lVCg', @to = 'Anonymus', @text = 'hey i am ASD', @res = @res OUTPUT
SELECT @res


SELECT TOP 1 Login, Text  FROM Messages WHERE ToLogin = ( SELECT Login FROM Users WHERE Login = 'asd' and Hash = 'GPYymdpnnkue8JfzY/lVCg' ) ORDER BY Id DESC;



-- GetDialog     Login, Hash, LoginInterlocutor
CREATE PROC  GetDialog
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@loginInterlocutor VARCHAR(20)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	SELECT * FROM 
	(
		SELECT Login, Text, ToLogin, Date FROM Messages WHERE Login = @login and ToLogin = @loginInterlocutor
		UNION ALL
		SELECT Login, Text, ToLogin, Date FROM Messages WHERE Login = @loginInterlocutor and ToLogin = @login
	) Messages ORDER BY Date
END
GO


EXEC GetDialog @login = 'asd', @hash = 'GPYymdpnnkue8JfzY/lVCg', @loginInterlocutor = 'Anonymus' 


CREATE PROC GetInterlocutor 
	@login NVARCHAR(20)
AS

SELECT  DISTINCT * FROM 
(
	SELECT ToLogin FROM Messages WHERE Login = @login
	UNION ALL
	SELECT Login FROM Messages WHERE ToLogin = @login
) Messages
GO


EXEC GetInterlocutor @login = 'asd'



CREATE PROC GetAccessions
	@login NVARCHAR(20),
	@hash NVARCHAR(100)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	SELECT Title, Text, People, AllPeople, IsComplete FROM My_accession WHERE Login = @login
END
GO

SELECT * FROM Users
EXEC GetAccessions @login = 'admin', @hash = 'xR2sWR5wIUq8vsjoisq0w'







CREATE PROC NewJoin
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@title NVARCHAR(20),
	@text NVARCHAR(100),
	@category NVARCHAR(20),
	@needPeople INT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	INSERT INTO Joins VALUES ( @login, @title, @text, @category, 0, @needPeople )
END
GO

EXEC NewJoin @login = '', @hash = '', @title = '', @text = '', @category = '', @needPeople = 0
SELECT * FROM Joins

DROP PROC NewJoin
DROP PROC GetAccessions
DROP PROC GetInterlocutor
DROP PROC SendMsg
DROP PROC Registration
DROP PROC GetCountMessages
DROP PROC GetDialog