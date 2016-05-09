CREATE DATABASE JoinAndDo
USE JoinAndDo

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


SELECT * FROM Joins
SELECT * FROM My_accession
SELECT * FROM Deals_accession


DROP TABLE Joins
DROP TABLE My_accession
DROP TABLE Deals_accession