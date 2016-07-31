USE JoinAndDo

--DROP TABLE Users
--DROP TABLE Accession


DROP TABLE Messages
DROP TABLE Joins
DROP TABLE My_accession
DROP TABLE Deals_accession
DROP TABLE Role
DROP TABLE RequestJoinToAccession

ALTER TABLE dbo.[Accession] DROP CONSTRAINT fk_Accession

DROP TABLE Accession
DROP TABLE Users




DROP PROC Registration
DROP PROC GetCountMessages
DROP PROC SendMsg
DROP PROC GetDialog
DROP PROC GetInterlocutor
DROP PROC GetAccessions
DROP PROC NewJoin
DROP PROC GetUserByName
DROP PROC GetDealsAccessions
DROP PROC FindAccessions
DROP PROC GetMyAccession
DROP PROC SendRequestToAccession
DROP PROC AddUserToAccession
DROP PROC GetMyInvitation
