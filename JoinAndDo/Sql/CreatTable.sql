CREATE DATABASE JoinAndDo
USE JoinAndDo

CREATE TABLE Users
(
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR( 20 ),
	Pass NVARCHAR( 20 ),
	FirstName NVARCHAR( 20 ),
	LastName NVARCHAR( 15 ),
	Hash NVARCHAR( 100 )
);

CREATE TABLE Joins
(
	Id INT IDENTITY( 1, 1 ),
	Title NVARCHAR( 20 ),
	Text NVARCHAR( 1000 ),
	People INT,
	AllPeople INT
);

CREATE TABLE My_accession
(
	Id INT IDENTITY( 1, 1 ),
	Title NVARCHAR( 20 ),
	Text NVARCHAR( 1000 ),
	People INT,
	AllPeople INT,
	IsComplete BIT 
);

CREATE TABLE Deals_accession
(
	Id INT IDENTITY( 1, 1 ),
	Title NVARCHAR( 20 ),
	Text NVARCHAR( 1000 ),
	[User] NVARCHAR(50),
	People INT,
	AllPeople INT
);

SELECT Login, Name, Hash FROM Users where Login = 'admin' and Pass = '123456'

INSERT INTO Users VALUES ( 'Andrew WAY', 'admin', '123456', null );


INSERT INTO Joins VALUES ( 'Test Title #1', 'Looking started he up perhaps against. Looking started he up perhaps against. Looking started he up perhaps against. Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 7, 10 )
INSERT INTO Joins VALUES ( 'Test Title #2', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 9, 10 )
INSERT INTO Joins VALUES ( 'Test Title #3', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 4, 10 )
INSERT INTO Joins VALUES ( 'Test Title #4', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 5, 10 )


INSERT INTO My_accession VALUES ( 'Test Title #1', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.Families blessing he in to no daughter.Families blessing he in to no daughter.', 4, 10 , 1 )
INSERT INTO My_accession VALUES ( 'Test Title #2', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 5, 10 , 0 )
INSERT INTO My_accession VALUES ( 'Test Title #3', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 5, 10 , 1 )
INSERT INTO My_accession VALUES ( 'Test Title #4', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 7, 10 , 0 )

INSERT INTO Deals_accession VALUES ( 'Test Title #1', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 'Nazar Dorin', 9, 10 )
INSERT INTO Deals_accession VALUES ( 'Test Title #1', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 'Bogdan Nezairs', 7, 10 )
INSERT INTO Deals_accession VALUES ( 'Test Title #1', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 'Nazar Sendder', 7, 7 )



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

DROP PROC registration

SELECT * FROM Users
SELECT * FROM Joins
SELECT * FROM My_accession
SELECT * FROM Deals_accession
UPDATE Users SET Hash = NULL where Login = '' and hash = 'hash'

DROP TABLE Users
DROP TABLE Joins
DROP TABLE My_accession
DROP TABLE Deals_accession