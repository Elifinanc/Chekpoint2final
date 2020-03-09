DROP DATABASE IF EXISTS Checkpoint2
GO
CREATE DATABASE Checkpoint2
GO

USE Checkpoint2
GO

DROP TABLE IF EXISTS [Event]
GO

CREATE TABLE [Event](event_id INT PRIMARY KEY IDENTITY(1,1),
				     event_name VARCHAR(60) NOT NULL,
				     start_time DATETIME NOT NULL,
					 end_time DATETIME NOT NULL,
					 event_description VARCHAR(200) NOT NULL
)
GO 

DROP TABLE IF EXISTS Agenda
GO

CREATE TABLE Agenda(agenda_id INT PRIMARY KEY IDENTITY(1,1),
			 agenda_name VARCHAR(60) NOT NULL,
)
GO

CREATE TABLE EventAgenda(eventAgenda_id INT PRIMARY KEY IDENTITY(1,1),
						FK_event_id INT NOT NULL,
						FOREIGN KEY (FK_event_id) REFERENCES [Event](event_id),
						FK_agenda_id INT NOT NULL,
						FOREIGN KEY (FK_agenda_id) REFERENCES Agenda(agenda_id)
)
GO

DROP TABLE IF EXISTS Campus
GO

CREATE TABLE Campus(campus_id INT PRIMARY KEY IDENTITY(1,1),
                    campus_location VARCHAR(60) NOT NULL
)
GO

DROP TABLE IF EXISTS Cursus
GO

CREATE TABLE Cursus(cursus_id INT PRIMARY KEY IDENTITY(1,1),
				   cursus_name VARCHAR(60) NOT NULL,
				   cursus_startdate DATETIME NOT NULL,
				   cursus_enddate DATETIME NOT NULL,
				   FK_campus_id INT NOT NULL,
				   FOREIGN KEY (FK_campus_id) REFERENCES Campus(campus_id),
			       FK_agenda_id INT NOT NULL,
				   FOREIGN KEY (FK_agenda_id) REFERENCES Agenda(agenda_id),
)
GO

DROP TABLE IF EXISTS Expedition
GO

CREATE TABLE Expedition(expedition_id INT PRIMARY KEY IDENTITY(1,1),
						expedition_startdate DATETIME NOT NULL,
						expedition_enddate DATETIME NOT NULL,
						FK_cursus_id INT NOT NULL,
				        FOREIGN KEY (FK_cursus_id) REFERENCES Cursus(cursus_id),
)
GO

DROP TABLE IF EXISTS Quest
GO

CREATE TABLE Quest(quest_id INT PRIMARY KEY IDENTITY(1,1),
				   quest_author VARCHAR(60) NOT NULL,
				   quest_text VARCHAR(500) NOT NULL,
				   quest_title VARCHAR(60) NOT NULL
)
GO

CREATE TABLE ExpeditionQuest(expeditionQuest_id INT PRIMARY KEY IDENTITY(1,1),
							FK_quest_id INT NOT NULL,
							FOREIGN KEY (FK_quest_id) REFERENCES Quest(quest_id),
							FK_expedition_id INT NOT NULL,
				            FOREIGN KEY (FK_expedition_id) REFERENCES Expedition(expedition_id)
)
GO


DROP TABLE IF EXISTS Person
GO

CREATE TABLE Person(person_id INT PRIMARY KEY IDENTITY(1,1),
                    person_name VARCHAR(60) NOT NULL,
					person_type VARCHAR(60) NOT NULL,
					FK_agenda_id INT NOT NULL,
					FOREIGN KEY (FK_agenda_id) REFERENCES Agenda(agenda_id),
					FK_cursus_id INT ,
					FOREIGN KEY (FK_cursus_id) REFERENCES Cursus(cursus_id),
					FK_lead_id INT ,
					FOREIGN KEY (FK_lead_id) REFERENCES Person(person_id)
)
GO

DECLARE @Counter INT = 1
WHILE(@Counter <10)
BEGIN
	INSERT INTO [Event](event_name, start_time, end_time, event_description)
	VALUES(CONCAT('Event', @Counter), CONCAT('0',@Counter,'-0',@Counter,'-2020'), CONCAT('0',@Counter+1, '-0',@Counter+1,'-2020'),'')
	SET @Counter = @Counter +1
END
GO

DECLARE @Counter INT = 1
WHILE(@Counter <= 4)
BEGIN
	INSERT INTO Agenda(agenda_name)
	VALUES (CONCAT('Agenda', @Counter))
	SET @Counter = @Counter +1
END
GO

DECLARE @Counter INT = 1
DECLARE @CounterAgendaId INT = 1
WHILE (@Counter <= 12)
BEGIN
	INSERT INTO EventAgenda(FK_event_id, FK_agenda_id)
	VALUES (@Counter, @CounterAgendaId)
	SET @CounterAgendaId= @CounterAgendaId + 1
	IF (@CounterAgendaId =5)
		BEGIN
			SET @CounterAgendaId = 1
		END
	SET @Counter= @Counter + 1
END
GO

DECLARE @Counter INT = 1
WHILE(@Counter <= 30)
BEGIN
	
	INSERT INTO Quest(quest_author, quest_text, quest_title)
	VALUES (CONCAT('QuestAuthor', @Counter), 'Lorem ipsum dolor sit amet, consectetur adipiscing elit,
	sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis
	nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat', CONCAT('QuestTitle',@Counter))
	SET @Counter = @Counter +1
END
GO

INSERT INTO Campus(campus_location)
VALUES ('Paris'), ('Strasbourg'), ('Lyon'), ('Marseille')
GO

DROP PROCEDURE IF EXISTS sp_AssociatedEachCampusAndEachAgendaToEachCursus
GO

CREATE PROCEDURE sp_AssociatedEachCampusAndEachAgendaToEachCursus
AS
BEGIN 
	DECLARE Campus_Cursor CURSOR SCROLL FOR
		SELECT campus_id FROM Campus
	DECLARE @CampusId INT
	DECLARE Agenda_Cursor CURSOR SCROLL FOR
		SELECT agenda_id FROM Agenda
	DECLARE @AgendaId INT
	OPEN Campus_Cursor	
	OPEN Agenda_Cursor
	FETCH FIRST FROM Campus_Cursor INTO @CampusId
	WHILE @@FETCH_STATUS =0
		BEGIN
			FETCH FIRST FROM Agenda_Cursor INTO @AgendaId
			WHILE @@FETCH_STATUS = 0 
				BEGIN
					INSERT INTO Cursus(cursus_name, cursus_startdate, cursus_enddate, FK_campus_id, FK_agenda_id) 
					VALUES ('Cursus','01-01-2020', '11-11-2020', @CampusId, @AgendaId)
					FETCH NEXT FROM Agenda_Cursor INTO @AgendaId
				END
			FETCH NEXT FROM Campus_Cursor INTO @CampusId
		END
	CLOSE Campus_Cursor
	CLOSE Agenda_Cursor
	DEALLOCATE Campus_Cursor
	DEALLOCATE Agenda_Cursor
END
GO

EXECUTE sp_AssociatedEachCampusAndEachAgendaToEachCursus
GO


DECLARE @Counter INT = 1
WHILE(@Counter < 10)
BEGIN
	INSERT INTO Expedition (expedition_startdate, expedition_enddate, FK_cursus_id)
	VALUES (CONCAT('0',@Counter, '-0',@Counter,'-2020'), CONCAT('0',@Counter +1, '-0',@Counter +1,'-2020'), @Counter)
	SET @Counter = @Counter +1
END
GO

DECLARE @Counter INT = 1
DECLARE @CounterExpeditionId INT = 1
WHILE (@Counter <= 30)
BEGIN
	INSERT INTO ExpeditionQuest (FK_quest_id, FK_expedition_id)
	VALUES (@Counter, @CounterExpeditionId)
	SET @CounterExpeditionId= @CounterExpeditionId + 1
	IF (@CounterExpeditionId =11)
		BEGIN
			SET @CounterExpeditionId = 1
		END
	SET @Counter= @Counter + 1
END
GO

--insertion des données dans la table Person

INSERT INTO Person(person_name, person_type, FK_agenda_id, FK_cursus_id, FK_lead_id)
VALUES ('Lead1', 'LeadFormer', 1 , NULL, 1)
GO

DROP PROCEDURE IF EXISTS sp_AssociatedEachFormerToLeadFormer
GO 

CREATE PROCEDURE sp_AssociatedEachFormerToLeadFormer
AS
BEGIN 
	DECLARE @FormerId INT = 1
	WHILE (@FormerId <= 4)
	BEGIN
		INSERT INTO Person(person_name, person_type, FK_agenda_id, FK_cursus_id, FK_lead_id)
		VALUES (CONCAT('Former', @FormerId), 'Former', 2 , NULL, 1)
		SET @FormerId = @FormerId  + 1
	END
END
GO

EXECUTE sp_AssociatedEachFormerToLeadFormer
GO

SELECT * FROM Person

DROP PROCEDURE IF EXISTS sp_AssociatedEachStudentToAFormer
GO 

CREATE PROCEDURE sp_AssociatedEachStudentToAFormer
AS
BEGIN 
	DECLARE @StudentIdCounter INT = 1, @NbStudent INT = 1
	WHILE (@StudentIdCounter <=120 AND @NbStudent>0)
	BEGIN
		DECLARE @FormerId INT
		SELECT @FormerId = person_id FROM Person WHERE person_name IN ('Former1', 'Former2', 'Former3', 'Former4') ORDER BY NEWID()
		INSERT INTO Person(person_name, person_type, FK_agenda_id, FK_cursus_id, FK_lead_id)
		VALUES (CONCAT('Student', @StudentIdCounter), 'Student', FLOOR(RAND()*(4-3+1))+3 , FLOOR(RAND()*(10-1+1))+1, @FormerId)
		SET @StudentIdCounter= @StudentIdCounter + 1

		SELECT COUNT(person_id)
		FROM Person
		GROUP BY FK_lead_id
		HAVING COUNT(person_id)<25
		SELECT @NbStudent = @@ROWCOUNT
	END
END
GO

EXECUTE sp_AssociatedEachStudentToAFormer
GO


DROP PROCEDURE IF EXISTS sp_GetAllEventBeetweenTwoDatesForAPerson
GO

CREATE PROCEDURE sp_GetAllEventBeetweenTwoDatesForAPerson (
@start_date AS DATETIME,
@end_date AS DATETIME,
@person_name AS VARCHAR(50)
)
AS
BEGIN 
	SELECT p.person_id, p.person_name, e.event_id, e.event_name, e.start_time, e.end_time
	FROM [Event] AS e
	INNER JOIN EventAgenda AS ea ON ea.FK_event_id = e.event_id
	INNER JOIN Agenda AS a ON a.agenda_id = ea.FK_agenda_id
	INNER JOIN Person AS p ON p.FK_agenda_id = a.agenda_id
	WHERE e.start_time >= @start_date AND e.end_time <= @end_date AND p.person_name = @person_name
	ORDER BY p.person_id
END
GO

EXECUTE sp_GetAllEventBeetweenTwoDatesForAPerson
    @start_date ='03-03-2020', 
    @end_date ='10-10-2020',
    @person_name ='Student74';
GO
