CREATE TABLE [dbo].[FODMAPIngredients]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[Name]		NVARCHAR(50)		NOT NULL,
	[Aliases]	NVARCHAR(50)		NULL,
	CONSTRAINT [PK_dbo.FODMAPIngredients] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[LabelledIngredients]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[Name]		NVARCHAR(50)		NOT NULL,
	[usdaId]	INT					NOT NULL,
	CONSTRAINT [PK_dbo.LabelledIngredients] PRIMARY KEY CLUSTERED ([ID] ASC)
);

-- #### IDENTITY TABLES BELOW #### --

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

--## MORE NON-ASPNET TABLES ##--

CREATE TABLE [dbo].[UserLists]
(
	[listID]	INT	IDENTITY (1,1)	NOT NULL,
	[userID]	NVARCHAR(128)		NOT NULL,
	[listName]	NVARCHAR(150)		NOT NULL,
	CONSTRAINT [PK_dbo.UserLists] PRIMARY KEY CLUSTERED ([listID] ASC),
	CONSTRAINT [PK_dbo.UserLists_dbo.userID] FOREIGN KEY ([userID])
		REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
);

CREATE TABLE [dbo].[SavedFoods]
(
	[usdaFoodID]	INT				NOT NULL,
	[listID]		INT				NOT NULL,
	[brand]			NVARCHAR(200)	NOT NULL,
	[upc]			NVARCHAR(32)	NOT NULL,
	[desc]			NVARCHAR(200)	NOT NULL,
	CONSTRAINT [PK_dbo.SavedFoods] PRIMARY KEY CLUSTERED ([usdaFoodID] ASC, [listID] ASC),
	CONSTRAINT [PK_dbo.SavedFoods_dbo.listID] FOREIGN KEY ([listID])
		REFERENCES [dbo].[UserLists] ([listID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[UserIngredients]
(
	[userID]				NVARCHAR(128)	NOT NULL,
	[Label]					NVARCHAR(10)	NOT NULL 
		CHECK (Label IN('High-Risk', 'Low-Risk', 'Blacklist')),
	[LabelledIngredientID]	INT				NOT NULL,
	[FODMAPIngredientID]	INT				NULL,
	CONSTRAINT [PK_dbo.UserIngredients] PRIMARY KEY CLUSTERED ([LabelledIngredientID] ASC, [userID] ASC),
	CONSTRAINT [PK_dbo.UserIngredients_dbo.userID] FOREIGN KEY ([userID])
		REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.UserIngredients_dbo.LabelledID] FOREIGN KEY ([LabelledIngredientID])
		REFERENCES [dbo].[LabelledIngredients] ([ID]),
	CONSTRAINT [PK_dbo.UserIngredients_dbo.FODMAPIngredientID] FOREIGN KEY ([FODMAPIngredientID])
		REFERENCES [dbo].[FODMAPIngredients] ([ID])
);

CREATE TABLE [dbo].[UserInformation]
(
	[userID]	NVARCHAR(128)	NOT NULL,
	[firstName]	NVARCHAR(30)	NOT NULL,
	[lastName]	NVARCHAR(30)	NOT NULL,
	[ethnicity]	NVARCHAR(50)	NOT NULL
		CHECK (ethnicity IN('Prefer not to say','Black', 'Native American', 'Asian', 'Native Hawaiian or Other Pacific Islander', 'Hispanic or Latino', 'White')),
	[birthdate]	DATETIME		NOT NULL,
	[country]	NVARCHAR(50)	NOT NULL,
	[gender]	NVARCHAR(25)	NOT NULL
		CHECK (gender IN('Male','Female','Nonbinary','Other/Prefer not to say')),
	CONSTRAINT [PK_dbo.UserInformation] PRIMARY KEY CLUSTERED ([userID] ASC),
	CONSTRAINT [PK_dbo.UserInformation_dbo.userID] FOREIGN KEY ([userID])
		REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[UserProfile]
(	
	[userID]		NVARCHAR(128)	NOT NULL,
	[is_public]		BIT				NOT NULL,
	[optIn]		BIT				NOT NULL,
	[showEthnicity]	BIT				NOT NULL,
	[showAge]		BIT				NOT NULL,
	[showCountry]	BIT				NOT NULL,
	[showGender]	BIT				NOT NULL,
	[showContact]	BIT				NOT NULL,
	[description]	NVARCHAR(2000)	NULL,
	[profileImgUrl]	NVARCHAR(15)	NULL,
	CONSTRAINT [PK_dbo.UserProfile] PRIMARY KEY CLUSTERED ([userID] ASC),
	CONSTRAINT [PK_dbo.UserProfile_dbo.userID] FOREIGN KEY ([userID])
		REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Quotes]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[Body]		NVARCHAR(250)		NOT NULL,
	[Author]	NVARCHAR(50)		NOT NULL,
	CONSTRAINT [PK_dbo.Quotes] PRIMARY KEY CLUSTERED ([ID] ASC)
);


--## SEED DATA ##--

INSERT INTO [dbo].[FODMAPIngredients] (Name) VALUES
	('Agave Syrup'),
	('Amaranth Flour'),
	('Barley'),
	('Buttermilk'),
	('Einkorn'),
	('Emmer'),
	('Fruit Concentrate'),
	('Garlic'),
	('Glucose-fructose syrup'),
	('Tinned Fruit in Natural Juice'),
	('Honey'),
	('Fructose'),
	('High Fructose Corn Syrup'),
	('Fruit Juice'),
	('Milk'),
	('Ice Cream'),
	('Yogurt'),
	('Ricotta'),
	('Cottage Cheese'),
	('Asparagus'),
	('Beetroot'),
	('Broccoli'),
	('Cabbage'),
	('Fennel'),
	('Garlic Powder'),
	('Garlic Salt'),
	('Okra'),
	('Rye'),
	('Baked Beans'),
	('Rambutan'),
	('Longan'),
	('Cauliflower'),
	('Onion Salt'),
	('Onion Powder'),
	('Dried Fruit'),
	('Fruit Juice'),
	('Gram Flour'),
	('Inulin'),
	('Isoglucose'),
	('Isolated Fructose'),
	('Lentil Flour'),
	('Lupin'),
	('Sour Cream'),
	('Spelt'),
	('Fructans'),
	('Lactose'),
	('Apple Juice'),
	('Custard'),
	('Sweetened condensed milk'),
	('Falafel'),
	('Sausage'),
	('Salami')
GO

INSERT INTO [dbo].[FODMAPIngredients] (Name, Aliases) VALUES
	('Mushroom', 'Mushrooms'),
	('Watermelon', 'Watermellon'),
	('Cashews', 'Cashew'),
	('Pistacios', 'Pistacio'),
	('Figs', 'Fig'),
	('Blackberries', 'Blackberry'),
	('Spring Onions', 'Spring Onion'),
	('Split Peas', 'Split Pea'),
	('Green Peas', 'Green Pea'),
	('Snow Pea', 'Snow Peas'),
	('Milk Curds', 'Milk Curd'),
	('Chickory Root', 'Chickory Extract'),
	('Custard Apple', 'Custard Apples'),
	('White Peaches', 'White Peach'),
	('Chickpeas', 'Chickpea'),
	('Lychee', 'Lychees'),
	('Nectarine', 'Nectarines'),
	('Peach', 'Peaches'),
	('Plum', 'Plums'),
	('Prune', 'Prunes'),
	('Avocado', 'Avocados'),
	('Persimmon', 'Persimmon'),
	('Apricot', 'Apricots'),
	('Cherries', 'Cherry'),
	('Lentils', 'Lentil'),
	('Red Kidney Beans', 'Red Kidney Bean'),
	('Nashi Pear', 'Nashi Pears'),
	('Leeks', 'Leek'),
	('Onions', 'Onion'),
	('Peas', 'Pea'),
	('Shallots', 'Shallot'),
	('Brussels sprout', 'Brussels Sprouts'),
	('Clingstone Peaches', 'Clingstone Peach'),
	('Mango', 'Mangoes'),
	('Artichokes', 'Artichoke'),
	('Sugar Snap Peas', 'Sugar Snap Pea'),
	('Pear', 'Pears'),
	('Apple', 'Apples'),
	('Khorasan Flour', 'Kamut'),
	('Chickpea Flour', 'Besan'),
	('Sorbitol','E420'),
	('Mannitol', 'E410'),
	('Xylitol','E967'),
	('Maltitol','E965'),
	('Isomalt','E953'),
	('Erythritol', 'E968'),
	('Fructooligosaccharides', 'FOS'),
	('Wheat', 'Gluten')
GO

INSERT INTO [dbo].[Quotes] (Body, Author) VALUES
    ('I swear I''ll branch. Promise.', 'Tanner'),
    ('All Scrum, no master.', 'Tanner'),
    ('That''s like a whole scrum effort point right there.', 'Jensen'),
    ('I can give you the "let''s normalize this database" look.', 'Jensen'),
    ('Make it more vogue.', 'Cassidy'),
    ('That''s the best flex I''ve heard all day.', 'Tanner'),
    ('"Cute and nonaggressive" that describes me pretty well.', 'Jensen'),
    ('We don''t even have a steak yet!', 'Tanner'),
    ('Is pathos just like being a dick?', 'Gabriel'),
    ('That is a powerful vibrate!', 'Jensen'),
    ('I''m combining some of the things, but not all of them.', 'Cassidy'),
    ('Ow I got something in my eye, wow this room is really dusty or something they should fix that cuz I might look like I''m crying but I''m totally not.', 'Jensen'),
	('If I''m drinking, I''m drinking. If I''m coding, I''m coding.', 'Tanner'),
	('Don''t clutch your rosary at me!', 'Tanner'),
	('I just timeboxed myself, bro.', 'Tanner'),
	('I live for cardinal sin.', 'Cassidy'),
	('Anything is a board if you''re brave enough.', 'Tanner'),
	('New year,  new pancreas.', 'Tanner'),
	('That''s a heckin'' Irish last name.', 'Jensen'),
	('Your peanut butter is pretty flexible.', 'Jensen'),
	('Notice the chili...', 'Tanner'),
	('But how do you keep your goldfish clean?', 'Jensen'),
	('Oh no, I changed Testy''s password.', 'Jensen'),
	('All manual testing shall be done with chili.', 'Jensen'),
	('This quantum robot hamster is sneckin''.', 'Jensen'),
	('The real associates degree is the friends you made along the way.', 'Tanner'),
	('It''s not going right now, but it goes.', 'Gabriel'),
	('Hmmmmmmgwahhh.', 'Jensen'),
	('I can have all the things that I want!', 'Cassidy'),
	('Ease of use equals ease of bowel movements.', 'Jensen'),
	('Yeah, you want to have a "fresh" object to play with.', 'Gabriel'),
	('Fear the might of Beansi!', 'Tanner'),
	('Did someone fix the... uh... Nope, its still anal', 'Gabriel'),
	('I thought you were talking in flatus.', 'Tanner'),
	('Class inheritance is like onions', 'Tanner')
GO