USE JoinAndDo

CREATE TABLE Users
(
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR( 20 ),
	Pass NVARCHAR( 150 ),
	FirstName NVARCHAR( 20 ),
	LastName NVARCHAR( 15 ),
	Hash NVARCHAR( 100 ),
	
	FulfillmentAccession INT,
	AcceptedConnections INT,
	TimeWorking INT,
	
	PRIMARY KEY ( Login )
);


CREATE TABLE Messages (
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR ( 20 ),
	Text NVARCHAR ( 1000 ),
	ToLogin NVARCHAR ( 20 ),
	Date datetime,
	
	PRIMARY KEY ( Id ),
	CONSTRAINT fk_messages FOREIGN KEY ( Login ) REFERENCES Users( Login )
);


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
	Login NVARCHAR( 20 ),
	Title NVARCHAR( 20 ),
	Text NVARCHAR( 1000 ),
	People INT,
	AllPeople INT,
	IsComplete BIT,
	
	CONSTRAINT fk_myAccession FOREIGN KEY ( Login ) REFERENCES Users( Login )
);

CREATE TABLE Deals_accession
(
	Id INT IDENTITY( 1, 1 ),
	Title NVARCHAR( 20 ),
	Text NVARCHAR( 1000 ),
	Login NVARCHAR( 20 ),
	People INT,
	AllPeople INT,
	
	
	CONSTRAINT fk_deals_Accession FOREIGN KEY ( Login ) REFERENCES Users( Login )
);