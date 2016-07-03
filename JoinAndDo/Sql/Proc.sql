USE JoinAndDo


CREATE PROC registration
  @login NVARCHAR(20),
  @pass NVARCHAR(20),
  @firstName NVARCHAR(20),
  @lastName NVARCHAR(20),
  @res NVARCHAR(30) OUTPUT
AS
SELECT @res = 's'
IF ((SELECT COUNT(*) FROM Users where Login = @login ) = 0)
BEGIN
	INSERT INTO Users VALUES( @login, @pass, @firstName, @lastName, null );
	SELECT @res = 'OK'
END
ELSE
	SELECT @res = 'user is already registered'
GO

DECLARE @res NVARCHAR(30)
EXEC registration @login = 'Anonymus', @pass = '123456', @firstName = 'Andriy', @lastName = 'Shportaluk', @res = @res OUTPUT
SELECT @res





CREATE PROC GetCountMessages
  @login NVARCHAR(20),
  @hash NVARCHAR(100),
  @res INT OUTPUT
AS
SELECT @res = 0
DECLARE @id INT
SET @id = ( SELECT Id FROM Users WHERE Login = @login and Hash = @hash )
SELECT count(*) FROM Messages WHERE Id_user = @id

DECLARE @res INT
EXEC GetCountMessages @login = 'asd', @hash = 'onWGh8qZEE6TjmTlQwOE6w', @res = @res OUTPUT
SELECT @res




CREATE PROC SendMsg
  @login NVARCHAR(20),
  @hash NVARCHAR(100),
  @to NVARCHAR(20),
  @text NVARCHAR(100),
  @res NVARCHAR(20) OUTPUT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	DECLARE @id INT
	SET @id = ( SELECT Id FROM Users WHERE Login = @to )
	
	INSERT INTO Messages VALUES( @login, @text, @id );
	SELECT @res = 'OK'
END
ELSE
	SELECT @res = 'Error !!!'
GO

SELECT * FROM Users
SELECT * FROM Messages WHERE Id_user = 2

DECLARE @res NVARCHAR(20)
EXEC SendMsg @login = 'Anonymus', @hash = '4afDfTFc206Kws1rZLOKTw', @to = 'w', @text = 'Hello :))', @res = @res OUTPUT
SELECT @res

DROP PROC SendMsg
DROP PROC registration
DROP PROC GetCountMessages