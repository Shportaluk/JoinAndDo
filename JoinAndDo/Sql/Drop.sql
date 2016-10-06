USE JoinAndDo


--DROP TABLE Messages
DROP TABLE My_accession
DROP TABLE Deals_accession
DROP TABLE RoleOfUserInAccession
DROP TABLE MessagesInAccession
DROP TABLE RoleOfUsers
DROP TABLE ListSkillsOfUsers
ALTER TABLE dbo.[Accession] DROP CONSTRAINT fk_Accession

DROP TABLE RequestCompleteToAccession
DROP TABLE RequestJoinToAccession
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
DROP PROC ExitWithAccession
DROP PROC LoadProfileImg
DROP PROC GetUsersByIdOfAccession
DROP PROC RequestComplateAccession
DROP PROC AddSkillToUser