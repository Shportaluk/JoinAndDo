USE JoinAndDo

CREATE TABLE Users
(
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR( 20 ),
	Pass NVARCHAR( 150 ),
	FirstName NVARCHAR( 20 ),
	LastName NVARCHAR( 15 ),
	Hash NVARCHAR( 100 ),
	PathImg NVARCHAR( 100 ),
	
	CompletedAccessions INT,
	AbandonedAccessions INT,
	CurrentlyAccessions INT,
	AllAccessions INT,
	
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

CREATE TABLE Accession
(
	Id INT IDENTITY( 1, 1 ),
	Title NVARCHAR( 100 ),
	Text NVARCHAR( 1500 ),
	Category NVARCHAR(100),
	Login NVARCHAR( 20 ),
	People INT,
	AllPeople INT,
	
	PRIMARY KEY(Id),
	CONSTRAINT fk_Accession FOREIGN KEY ( Login ) REFERENCES Users( Login )
);

CREATE TABLE MessagesInAccession (
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR ( 20 ),
	Text NVARCHAR ( 1000 ),
	IdAccession INT,
	Date datetime,
	
	PRIMARY KEY ( Id ),
	CONSTRAINT fk_messages_in_accession_login FOREIGN KEY ( Login ) REFERENCES Users( Login ),
	CONSTRAINT fk_messages_in_accession_IdAccession FOREIGN KEY ( IdAccession ) REFERENCES Accession( Id )
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


CREATE TABLE RequestJoinToAccession (
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR ( 20 ),
	Text NVARCHAR ( 1000 ),
	Category NVARCHAR ( 100 ),
	ToIdAccession INT,
	Status NVARCHAR (10),
	
	PRIMARY KEY ( Id ),
	CONSTRAINT fk_request_join_to_accession_login FOREIGN KEY ( Login ) REFERENCES Users( Login ),
	CONSTRAINT fk_request_join_to_accession_idAccession FOREIGN KEY ( ToIdAccession ) REFERENCES Accession( Id )
);

CREATE TABLE RoleOfUserInAccession
(
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR( 20 ) NULL,
	RoleName NVARCHAR( 20 ),
	IdAccession INT,
	
	
	CONSTRAINT fk_RoleOfUserInAccession_Login FOREIGN KEY ( Login ) REFERENCES Users( Login ),
	CONSTRAINT fk_RoleOfUserInAccession_IdAccession FOREIGN KEY ( IdAccession ) REFERENCES Accession( Id )
);

CREATE TABLE RoleOfUsers
(
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR( 20 ),
	RoleName NVARCHAR( 20 ),
	
	
	CONSTRAINT fk_RoleOfUsers_Login FOREIGN KEY ( Login ) REFERENCES Users( Login )
);

CREATE TABLE RequestCompleteToAccession (
	Id INT IDENTITY( 1, 1 ),
	Login NVARCHAR ( 20 ),
	IdAccession INT,
	
	
	PRIMARY KEY ( Id ),
	CONSTRAINT fk_request_complete_accession_login FOREIGN KEY ( Login ) REFERENCES Users( Login ),
	CONSTRAINT fk_request_complete_accession_idAccession FOREIGN KEY ( IdAccession ) REFERENCES Accession( Id )
);


CREATE TABLE ListSkillsOfUsers (
	Id INT IDENTITY( 1, 1 ),
	PathImg NVARCHAR(100),
	Name NVARCHAR ( 10 ),
	Login NVARCHAR (20),
	
	
	PRIMARY KEY ( Id ),
	CONSTRAINT fk_Login_ListSkillsOfUsers FOREIGN KEY ( Login ) REFERENCES Users( Login )
);