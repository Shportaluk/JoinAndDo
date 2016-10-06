USE JoinAndDo

GO
CREATE PROC Registration
  @login NVARCHAR(20),
  @pass NVARCHAR(150),
  @firstName NVARCHAR(20),
  @lastName NVARCHAR(20)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login ) = 0)
BEGIN
	INSERT INTO Users VALUES( @login, @pass, @firstName, @lastName, null, 'default.bmp', 0, 0, 0, 0 );
	SELECT 'OK'
END
ELSE
	SELECT 'User is already registered'
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
CREATE PROC SendMsgToAccession
  @login NVARCHAR(20),
  @hash NVARCHAR(100),
  @idAccession INT,
  @text NVARCHAR(1000)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF( (SELECT COUNT(*) FROM RoleOfUserInAccession WHERE Login = @login) >= 1 )
	BEGIN
		INSERT INTO MessagesInAccession VALUES( @login, @text, @idAccession, GETDATE() );
		SELECT 'Ok'
	END
	ELSE 
		SELECT 'You have not access'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
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
CREATE PROC  GetDialogOfAccession
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@idAccession INT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF( (SELECT COUNT(*) FROM RoleOfUserInAccession WHERE Login = @login) >= 1 )
	BEGIN
		SELECT Login, Text FROM MessagesInAccession WHERE IdAccession = @idAccession ORDER BY Date
		SELECT 'Ok'
	END
	ELSE 
		SELECT 'You have not access'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
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
	UPDATE Users SET AllAccessions = AllAccessions + 1 WHERE Login = @login
	UPDATE Users SET CurrentlyAccessions = CurrentlyAccessions + 1 WHERE Login = @login
END
GO 



GO
CREATE PROC GetUserByName
	@name NVARCHAR(20)
AS
	SELECT Id, FirstName, LastName, Hash, PathImg FROM Users WHERE FirstName LIKE @name + '%'
	UNION ALL
	SELECT Id, FirstName, LastName, Hash, PathImg FROM Users WHERE LastName LIKE @name + '%'
GO


GO
CREATE PROC GetMyAccessionsManagement
	@login NVARCHAR(20),
	@hash NVARCHAR(100)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	SELECT * FROM Accession WHERE Login = @login
END
GO 



GO
CREATE PROC GetMyAccessions
	@login NVARCHAR(20),
	@hash NVARCHAR(100)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	Select IdAccession Into #TempGetMyAccessions From RoleOfUserInAccession  WHERE Login = 'qwe' and RoleName != 'Creator'

	WHILE (SELECT COUNT(*) FROM #TempGetMyAccessions) > 0
	BEGIN
		Declare @IdAccession int
		SET @IdAccession = ( SELECT TOP 1 IdAccession FROM #TempGetMyAccessions )
		SELECT * FROM Accession WHERE Id = @IdAccession
		Delete #TempGetMyAccessions Where IdAccession = @IdAccession
	END

	DROP TABLE #TempGetMyAccessions
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
	@idAccession INT,
	@res NVARCHAR(255) OUTPUT
AS
IF (( SELECT COUNT(*) FROM Users WHERE Login = @login and Hash = @hash ) = 1)
BEGIN
	IF (( SELECT Login FROM Accession WHERE Id = @idAccession) = @login )
	BEGIN
		IF(( SELECT COUNT(*) FROM RoleOfUserInAccession WHERE IdAccession = @idAccession and RoleName = @role ) >= 1 )
		BEGIN
			IF(( SELECT People FROM Accession WHERE Id = @idAccession ) < ( SELECT AllPeople FROM Accession WHERE Id = @idAccession ))
			BEGIN
				UPDATE RoleOfUserInAccession SET Login = @loginUserAdded WHERE IdAccession = @idAccession and RoleName = @role
				UPDATE Accession SET People = People + 1 WHERE Id = @idAccession
				SELECT @res = 'Ok'
			END
			ELSE
				SELECT @res = 'No free places'
		END
		ELSE
			SELECT @res = 'This RoleOfUserInAccession does not exist'
	END
	ELSE
		SELECT @res = 'You have not access'
END
ELSE
	SELECT @res = 'You are not registered or do not have entrance to the site'
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


GO 
CREATE PROC DeleteJoin
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@idAccession INT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF((SELECT COUNT(*) FROM Accession WHERE Id = @idAccession and Login = @login) = 1 )
	BEGIN
		SELECT Login INTO #TempLoginsDeteteJoin FROM RoleOfUserInAccession WHERE IdAccession = @idAccession and Login IS NOT NULL
		WHILE (SELECT COUNT(*) FROM #TempLoginsDeteteJoin) > 0
			BEGIN
				Declare @Login_ NVARCHAR(20)
				SET @Login_ = ( SELECT TOP 1 Login FROM #TempLoginsDeteteJoin )
				UPDATE Users SET CurrentlyAccessions = CurrentlyAccessions - 1 WHERE Login = @Login_
				Delete #TempLoginsDeteteJoin WHERE Login = @Login_
			END
		DROP TABLE #TempLoginsDeteteJoin
			
		
		DELETE FROM RequestJoinToAccession WHERE ToIdAccession = @idAccession
		DELETE FROM RoleOfUserInAccession WHERE IdAccession = @idAccession
		DELETE FROM MessagesInAccession WHERE IdAccession = @idAccession
		DELETE FROM RequestCompleteToAccession WHERE IdAccession = @idAccession
		DELETE FROM Accession WHERE Id = @idAccession
		SELECT 'Ok'
	END
	ELSE
		SELECT 'You have not access'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO



GO
CREATE PROC AcceptRequestOfUserToAccession
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@user NVARCHAR(20),
	@role NVARCHAR(100),
	@idAccession INT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF ((SELECT COUNT(*) FROM Accession WHERE Login = @login and Id = @idAccession ) >= 1)
	BEGIN
		DECLARE @res NVARCHAR(255)
		EXEC AddUserToAccession @login = @login, @hash = @hash, @loginUserAdded = @user, @role = @role, @idAccession = @idAccession, @res = @res OUTPUT
		IF(@res = 'Ok')
		BEGIN
			UPDATE RequestJoinToAccession SET Status = 'Accepted' WHERE ToIdAccession = @idAccession and Login = @user
			UPDATE Users SET AllAccessions = AllAccessions + 1 WHERE Login = @user
			UPDATE Users SET CurrentlyAccessions = CurrentlyAccessions + 1 WHERE Login = @user
			SELECT 'Ok'
		END
		ELSE
			SELECT 'Error'
	END
	ELSE
		SELECT 'You have not access'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO


GO
CREATE PROC RejectRequestOfUserToAccession
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@user NVARCHAR(20),
	@idAccession INT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF ((SELECT COUNT(*) FROM Accession WHERE Login = @login and Id = @idAccession ) >= 1)
	BEGIN
		UPDATE RequestJoinToAccession SET Status = 'Rejected' WHERE ToIdAccession = @idAccession and Login = @user
		SELECT 'Ok'
	END
	ELSE
		SELECT 'You have not access'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO

EXEC RejectRequestOfUserToAccession @login = 'qweqwe', @hash = '6cpe33dkzUq0DAWABVf7TQ', @user = '123123', @idAccession = 7


GO
CREATE PROC EditDescriptionOfAccession
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@idAccession INT,
	@description NVARCHAR(1500)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF( (SELECT COUNT(*) FROM Accession WHERE Id = @idAccession and Login = @login) = 1 )
	BEGIN
		UPDATE Accession SET Text = @description WHERE Id = @idAccession and Login = @login
		SELECT 'Ok'
	END
	ELSE
		SELECT 'You have not access'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO


GO
CREATE PROC EditTitleOfAccession
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@idAccession INT,
	@title NVARCHAR(100)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF( (SELECT COUNT(*) FROM Accession WHERE Id = @idAccession and Login = @login) = 1 )
	BEGIN
		UPDATE Accession SET Title = @title WHERE Id = @idAccession and Login = @login
		SELECT 'Ok'
	END
	ELSE
		SELECT 'You have not access'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO


GO
CREATE PROC ExitWithAccession
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@idAccession INT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF((SELECT COUNT(*) FROM RoleOfUserInAccession WHERE IdAccession = @idAccession and Login = @login and RoleName != 'Creator') = 1 )
	BEGIN
		UPDATE RoleOfUserInAccession SET Login = NULL WHERE IdAccession = @idAccession and Login = @login
		DELETE RequestJoinToAccession WHERE ToIdAccession = @idAccession and Login = @login
		UPDATE Accession SET People = People - 1 WHERE Id = @idAccession
		UPDATE Users SET AbandonedAccessions = AbandonedAccessions + 1 WHERE Login = @login
		UPDATE Users SET CurrentlyAccessions = CurrentlyAccessions - 1 WHERE Login = @login
		SELECT 'Ok'
	END
	ELSE
		SELECT 'You have not access'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO


GO
CREATE PROC LoadProfileImg
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@pathImg NVARCHAR(350)
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	UPDATE Users SET PathImg = @pathImg WHERE Login = @login
	SELECT 'Ok'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO


GO
CREATE PROC GetUsersByIdOfAccession
	@idAccession INT
AS
	SELECT Login, RoleName Into #Temp From RoleOfUserInAccession  WHERE IdAccession = @idAccession and Login IS NOT NULL
	CREATE TABLE #t
	(
		Id INT IDENTITY( 1, 1 ),
		Login NVARCHAR( 20 ),
		Hash NVARCHAR( 100 ),
		PathImg NVARCHAR( 100 ),
		FirstName NVARCHAR( 20 ),
		LastName NVARCHAR( 15 ),
		RoleName NVARCHAR( 20 )
	)
	WHILE (SELECT COUNT(*) FROM #Temp) > 0
	BEGIN
		Declare @login NVARCHAR( 20 )
		SET @login = ( SELECT TOP 1 Login FROM #Temp )
		INSERT INTO #t SELECT  Users.Login, Users.Hash, Users.PathImg, Users.FirstName, Users.LastName, #Temp.RoleName FROM Users, #Temp WHERE Users.Login = @login and #Temp.Login = @login
		DELETE #Temp Where Login = @login
	END
	SELECT * FROM #t
	DROP TABLE #t
GO


GO
CREATE PROC RequestComplateAccession
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@idAccession INT
AS
IF ((SELECT COUNT(*) FROM Users where Login = @login and Hash = @hash ) = 1)
BEGIN
	IF ( (SELECT COUNT(*) FROM RequestCompleteToAccession WHERE Login = @login and IdAccession = @idAccession) = 0 )
	BEGIN
		INSERT INTO RequestCompleteToAccession VALUES( @login, @idAccession )
		IF( ((SELECT COUNT(*) FROM RequestCompleteToAccession WHERE IdAccession = @idAccession)/((SELECT COUNT(*) FROM RoleOfUserInAccession WHERE idAccession = @idAccession)-1)) >= 0.7 )
		BEGIN
			
			SELECT Login INTO #TempLoginsRequestComplateAccession FROM RoleOfUserInAccession WHERE IdAccession = @idAccession and Login IS NOT NULL
			WHILE (SELECT COUNT(*) FROM #TempLoginsRequestComplateAccession) > 0
			BEGIN
				Declare @Login_ NVARCHAR(20)
				SET @Login_ = ( SELECT TOP 1 Login FROM #TempLoginsRequestComplateAccession )
				
				UPDATE Users SET CompletedAccessions = CompletedAccessions + 1 WHERE Login = @Login_
				
				Delete #TempLoginsRequestComplateAccession WHERE Login = @Login_
			END
			DROP TABLE #TempLoginsRequestComplateAccession
			
			
			DECLARE @Creator NVARCHAR(20)
			DECLARE @HashOfCreator NVARCHAR(100)
			SET @Creator = (SELECT Login FROM RoleOfUserInAccession WHERE RoleName = 'Creator' and IdAccession = @idAccession)
			SET @HashOfCreator = ( SELECT Hash FROM Users WHERE Login = @Creator )
			EXEC DeleteJoin @login = @Creator, @hash = @HashOfCreator, @idAccession = @idAccession
			
			SELECT 'Complated'
		END
		ELSE
			SELECT 'Ok'
	END
	ELSE
		SELECT 'You have sent a request'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO


GO
CREATE PROC AddSkillToUser
	@login NVARCHAR(20),
	@hash NVARCHAR(100),
	@pathImg NVARCHAR(100),
	@name NVARCHAR(10)
AS
IF ((SELECT COUNT(*) FROM Users WHERE Login = @login and Hash = @hash ) = 1)
BEGIN
	IF((SELECT COUNT(*) FROM ListSkillsOfUsers WHERE Login = @login and Name = @name) = 0)
	BEGIN
		INSERT INTO ListSkillsOfUsers VALUES( @pathImg, @name, @login )
		SELECT 'Ok'
	END
	ELSE
		SELECT 'This skill you already have'
END
ELSE
	SELECT 'You are not registered or do not have entrance to the site'
GO

EXEC GetUserByName @name = 'qweqwe'