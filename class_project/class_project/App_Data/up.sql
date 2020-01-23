CREATE TABLE [dbo].[Person]
(
	[ID]	INT IDENTITY (1,1)	NOT NULL,
	[FirstName]	NVARCHAR(50)	NOT NULL,
	[LastName]	NVARCHAR(50)	NOT NULL,
	CONSTRAINT [PK_dbo.Person] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Gender]
(
	[ID]	INT IDENTITY (1,1)	NOT NULL,
	[Name]	NVARCHAR(50)		NOT NULL,
	CONSTRAINT [PK_dbo.Gender] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Athlete]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[Age]		INT					NOT NULL,
	[PersonID]	INT					NOT NULL,
	[GenderID]	INT					NOT NULL,
	CONSTRAINT [PK_dbo.Athlete] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [PK_dbo.Athlete_dbo.PersonID] FOREIGN KEY ([PersonID])
		REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Athlete_dbo.GenderID] FOREIGN KEY ([GenderID])
		REFERENCES [dbo].[Gender] ([ID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Coach]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[PersonID]	INT					NOT NULL,
	CONSTRAINT [PK_dbo.Coach] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [PK_dbo.Coach_dbo.PersonID] FOREIGN KEY ([PersonID])
		REFERENCES [dbo].[Person] ([ID]) ON DELETE CASCADE,
);

CREATE TABLE [dbo].[Team]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[Name]		NVARCHAR(50)		NOT NULL,
	[CoachID]	INT					NOT NULL,
	CONSTRAINT [PK_dbo.Team] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [PK_dbo.Team_dbo.CoachID] FOREIGN KEY ([CoachID])
		REFERENCES [dbo].[Coach] ([ID]) ON DELETE CASCADE,
);

CREATE TABLE [dbo].[TeamMember]
(
	[TeamID]	INT		NOT NULL,
	[AthleteID]	INT		NOT NULL,
	CONSTRAINT [PK_dbo.TeamMeet] PRIMARY KEY CLUSTERED ([TeamID], [AthleteID]),
	CONSTRAINT [PK_dbo.TeamMember_dbo.TeamID] FOREIGN KEY ([TeamID])
		REFERENCES [dbo].[Team] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Team_dbo.AthleteID] FOREIGN KEY ([AthleteID])
		REFERENCES [dbo].[Athlete] ([ID]) ON DELETE NO ACTION,
);

CREATE TABLE [dbo].[Event]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[Name]		NVARCHAR(50)		NOT NULL,
	[GenderID]	INT					NOT NULL,
	CONSTRAINT [PK_dbo.Event] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [PK_dbo.Event_dbo.GenderID] FOREIGN KEY ([GenderID])
		REFERENCES [dbo].[Gender] ([ID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Meet]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[Date]		DATETIME			NOT NULL,
	[Location]	NVARCHAR(50)		NOT NULL,
	CONSTRAINT [PK_dbo.Meet] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Record]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[Time]		FLOAT				NOT NULL,
	[AthleteID]	INT					NOT NULL,
	[EventID]	INT					NOT NULL,
	[MeetID]	INT					NOT NULL,
	CONSTRAINT [PK_dbo.Record] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [PK_dbo.Record_dbo.AthleteID] FOREIGN KEY ([AthleteID])
		REFERENCES [dbo].[Athlete] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Record_dbo.EventID] FOREIGN KEY ([EventID])
		REFERENCES [dbo].[Event] ([ID]) ON DELETE NO ACTION,
	CONSTRAINT [PK_dbo.Record_dbo.MeetID] FOREIGN KEY ([MeetID])
		REFERENCES [dbo].[Meet] ([ID]) ON DELETE CASCADE
);

-- ##### IDENTITY TABLES BELOW ##### ATHLETE TABLES ABOVE #####

CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
USE [master]
GO
ALTER DATABASE [aspnet-class_project-20200121043524] SET  READ_WRITE 
GO
