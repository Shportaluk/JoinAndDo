USE JoinAndDo

GO
CREATE PROC Registration
  @login NVARCHAR(20),
  @pass NVARCHAR(150),
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



CREATE PROC GetCountMessages
  @login NVARCHAR(20),
  @hash NVARCHAR(100)
AS
IF (( SELECT COUNT(*) FROM Users WHERE Login = @login and Hash = @hash ) = 1 )
BEGIN
	SELECT count(*) FROM Messages WHERE toLogin = @login
END
GO

GO
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


-- GetDialog     Login, Hash, LoginInterlocutor
GO
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


GO
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



GO
CREATE PROC GetAccessions
	@login NVARCHAR(20),
	@hash NVARCHAR(100)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	SELECT Title, Text, People, AllPeople, IsComplete FROM My_accession WHERE Login = @login
END
GO



GO
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

GO
CREATE PROC GetUserByName
	@name NVARCHAR(20)
AS
	SELECT Id, FirstName, LastName, Hash FROM Users WHERE FirstName LIKE @name + '%'
	UNION ALL
	SELECT Id, FirstName, LastName, Hash FROM Users WHERE LastName LIKE @name + '%'
GO