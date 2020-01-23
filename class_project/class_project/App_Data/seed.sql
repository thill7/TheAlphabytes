INSERT INTO [dbo].[Person] (FirstName, LastName) VALUES
	('Bob','Dole'),
	('Sarah','Smith'),
	('Jim','Jackson'),
	('Frank', 'Goodsworth'),
	('Billie','Jean')
GO

INSERT INTO [dbo].[Gender] (Name) VALUES
	('Male'),
	('Female')
GO

INSERT INTO [dbo].[Athlete] (Age, PersonID, GenderID) VALUES
	(15,1,1),
	(17,2,2),
	(14,3,1)
GO

INSERT INTO [dbo].[Coach] (PersonID) VALUES
	(4),
	(5)
GO

INSERT INTO [dbo].[Team] (Name, CoachID) VALUES
	('Dallas High School', 1),
	('The Independents', 2),
	('Central High School', 2)
GO

INSERT INTO [dbo].[TeamMember] (TeamID, AthleteID) VALUES
	(1,1),
	(1,2),
	(2,1),
	(3,2)
GO

INSERT INTO [dbo].[Event] (Name, GenderID) VALUES
	('50 meter backstroke',1),
	('50 meter backstroke',2),
	('100 meter freestyle',1),
	('100 meter freestyle',2)
GO

INSERT INTO [dbo].[Meet] (Date, Location) VALUES
	('2019-12-22','Dallas Aquatic Center'),
	('2019-12-30','Western Oregon University'),
	('2020-01-07','The Pacific Ocean')
GO

INSERT INTO [dbo].[Record] (Time, AthleteID, EventID, MeetID) VALUES
	(156,1,1,1),
	(187,1,1,2),
	(143,1,1,3),
	(375,1,3,1),
	(347,1,3,2),
	(336,1,3,3),
	(258,2,2,1),
	(269,2,2,2),
	(278,2,2,3),
	(584,2,4,1),
	(500,2,4,2),
	(512,2,4,3),
	(175,3,1,1),
	(222,3,1,2),
	(198,3,1,3),
	(200,3,3,1),
	(205,3,3,2),
	(199,3,3,3)
GO