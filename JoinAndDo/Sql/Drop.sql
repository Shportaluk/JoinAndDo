USE JoinAndDo

--DROP TABLE Users
--DROP TABLE Accession


DROP TABLE Messages
DROP TABLE Joins
DROP TABLE My_accession
DROP TABLE Deals_accession
DROP TABLE Role
DROP TABLE RequestJoinToAccession
DROP TABLE RolesOfHumanInAccession
DROP TABLE MessagesInAccession

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
DROP PROC GetMyAccessionsManagement
DROP PROC GetMyAccessions
DROP PROC SendRequestToAccession
DROP PROC AddUserToAccession
DROP PROC GetMyInvitation
DROP PROC DeleteJoin
DROP PROC AcceptRequestOfUserToAccession
DROP PROC RejectRequestOfUserToAccession
DROP PROC EditDescriptionOfAccession
DROP PROC EditTitleOfAccession
DROP PROC SendMsgToAccession
DROP PROC GetDialogOfAccession

