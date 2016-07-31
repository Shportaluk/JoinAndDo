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
CREATE PROC GetDealsAccessions
	@login NVARCHAR(20),
	@hash NVARCHAR(100)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	SELECT * FROM Deals_accession WHERE Login = @login
END
GO



GO
CREATE PROC FindAccessions
	@text NVARCHAR(255)
AS
	SELECT * FROM Accession WHERE Text LIKE '%' + @text + '%'
GO


GO
CREATE PROC NewJoin
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@title NVARCHAR(20),
	@text NVARCHAR(1500),
	@category NVARCHAR(20),
	@needPeople INT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	INSERT INTO Accession OUTPUT Inserted.ID VALUES ( @title, @text, @category, @login, 0, @needPeople )
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


GO
CREATE PROC GetMyAccession
	@login NVARCHAR(20),
	@hash NVARCHAR(100)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	SELECT * FROM Accession WHERE Login = @login
END
GO 


GO
CREATE PROC SendRequestToAccession
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@text NVARCHAR(1000),
	@category NVARCHAR(100),
	@idAccession INT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF ( (SELECT COUNT(*) FROM RequestJoinToAccession WHERE Login = @login and ToIdAccession = @idAccession ) = 0 )
	BEGIN
		INSERT INTO RequestJoinToAccession VALUES ( @login, @text, @category, @idAccession, 'Waiting' );
		SELECT 'Ok'
	END
	ELSE
		SELECT 'You have sent a request'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO 


GO
CREATE PROC AddUserToAccession
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@loginUserAdded NVARCHAR(20),
	@role NVARCHAR(100),
	@idAccession INT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF ( (SELECT RoleName FROM Role WHERE Login = @login and IdAccession = @idAccession ) = 'Creator' or ( SELECT Login FROM Accession WHERE Id = @idAccession ) = @login )
	BEGIN
		INSERT INTO Role VALUES ( @login, @role, @idAccession )
		SELECT 'Ok'
	END
	ELSE
		SELECT 'you have not access'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO 



GO
CREATE PROC GetMyInvitation
	@login NVARCHAR(20),
	@hash NVARCHAR(100)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	SELECT Text, Category, ToIdAccession, Status FROM RequestJoinToAccession WHERE Login = @login
END
GO 