USE JoinAndDo

CREATE TABLE Users
(
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR( 20 ),
	Pass NVARCHAR( 20 ),
	FirstName NVARCHAR( 20 ),
	LastName NVARCHAR( 15 ),
	Hash NVARCHAR( 100 ),
	
	FulfillmentAccession INT,
	AcceptedConnections INT,
	TimeWorking INT
);

INSERT INTO Users VALUES ( 'asd', 'asd', 'Andrew', 'Way', null, 0, 0, 0 );

SELECT * FROM Users

CREATE TABLE Messages (
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR ( 20 ),
	Text NVARCHAR ( 1000 ),
	ToLogin NVARCHAR ( 20 ),
	Date datetime
);

INSERT INTO Messages VALUES ( 'Anonymus', 'text3', 'asd', GETDATE() )
SELECT TOP 1 Login, Text  FROM Messages WHERE ToLogin = 'asd' ORDER BY Id DESC

CREATE TABLE Joins
(
	Id INT IDENTITY( 1, 1 ),
	Creator NVARCHAR( 20 ),
	Title NVARCHAR( 20 ),
	Text NVARCHAR( 1000 ),
	Category NVARCHAR( 100 ),
	People INT,
	AllPeople INT
);
CREATE TABLE My_accession
(
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR(20),
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



INSERT INTO Joins VALUES ( 'Test Title #1', 'Looking started he up perhaps against. Looking started he up perhaps against. Looking started he up perhaps against. Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 7, 10 )
INSERT INTO Joins VALUES ( 'Test Title #2', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 9, 10 )
INSERT INTO Joins VALUES ( 'Test Title #3', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 4, 10 )
INSERT INTO Joins VALUES ( 'Test Title #4', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 5, 10 )


INSERT INTO My_accession VALUES ( 'asd', 'Test Title #1', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.Families blessing he in to no daughter.Families blessing he in to no daughter.', 4, 10 , 1 )
INSERT INTO My_accession VALUES ( 'asd', 'Test Title #2', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 5, 10 , 0 )
INSERT INTO My_accession VALUES ( 'asd', 'Test Title #3', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 5, 10 , 1 )
INSERT INTO My_accession VALUES ( 'asd', 'Test Title #4', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 7, 10 , 0 )
INSERT INTO My_accession VALUES ( 'Anonymus', 'Test Title #4', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 7, 10 , 0 )
INSERT INTO My_accession VALUES ( 'Anonymus', 'Test Title #4', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 7, 10 , 0 )

INSERT INTO Deals_accession VALUES ( 'Test Title #1', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 'Nazar Dorin', 9, 10 )
INSERT INTO Deals_accession VALUES ( 'Test Title #1', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 'Bogdan Nezairs', 7, 10 )
INSERT INTO Deals_accession VALUES ( 'Test Title #1', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 'Nazar Sendder', 7, 7 )



SELECT * FROM Users
SELECT * FROM Joins
SELECT * FROM Messages
SELECT * FROM My_accession
SELECT * FROM Deals_accession
UPDATE Users SET Hash = NULL where Login = '' and hash = 'hash'

DROP TABLE Users
DROP TABLE Messages
DROP TABLE Joins
DROP TABLE My_accession
DROP TABLE Deals_accession