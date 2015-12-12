CREATE TABLE Joins
(
	Id INT IDENTITY( 1, 1 ),
	Title NVARCHAR( 20 ),
	Text NVARCHAR( 1000 )
);

CREATE TABLE My_accession
(
	Id INT IDENTITY( 1, 1 ),
	Title NVARCHAR( 20 ),
	Text NVARCHAR( 1000 ),
	IsComplete BIT 
);



INSERT INTO Joins VALUES ( 'Test Title #1', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.' )
INSERT INTO Joins VALUES ( 'Test Title #2', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.' )
INSERT INTO Joins VALUES ( 'Test Title #3', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.' )
INSERT INTO Joins VALUES ( 'Test Title #4', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.' )


INSERT INTO My_accession VALUES ( 'Test Title #1', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 1 )
INSERT INTO My_accession VALUES ( 'Test Title #2', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 0 )
INSERT INTO My_accession VALUES ( 'Test Title #3', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 1 )
INSERT INTO My_accession VALUES ( 'Test Title #4', 'Looking started he up perhaps against. How remainder all additions get elsewhere resources. One missed shy wishes supply design answer formed. Prevent on present hastily passage an subject in be. Be happiness arranging so newspaper defective affection ye. Families blessing he in to no daughter.', 0 )


SELECT * FROM Joins
SELECT * FROM My_accession


DROP TABLE Joins
DROP TABLE My_accession