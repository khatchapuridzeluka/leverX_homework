CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL
);


CREATE TABLE [Players] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [Name] NVARCHAR(255),
    [LastName] NVARCHAR(255),
    [Sex] INT NOT NULL CHECK (Sex IN (0, 1)), -- 0 = Male, 1 = Female
    [Nationality] INT NOT NULL CHECK (Nationality IN (
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14
    )),
    [FideRating] INT,
    [Title] INT NOT NULL CHECK (Title IN (0, 1, 2, 3, 4, 5, 6, 7, 8))
);

GO

CREATE TABLE [Games] (
	[Id] UNIQUEIDENTIFIER PRIMARY KEY,
	[WhitePlayerId] UNIQUEIDENTIFIER,
	[BlackPlayerId] UNIQUEIDENTIFIER,
	[Result] INT NOT NULL CHECK (Result IN (0, 1, 2, 3)),
	[Moves] NVARCHAR(MAX),
	[PlayedOn] DATETIME,
	[OpeningId] UNIQUEIDENTIFIER,
	[TournamentId] UNIQUEIDENTIFIER
);
GO
CREATE TABLE [Openings] (
	[Id] UNIQUEIDENTIFIER PRIMARY KEY,
	[Name] NVARCHAR(255),
	[EcoCode] NVARCHAR(255),
	[Moves] NVARCHAR(MAX)
)
GO

CREATE TABLE [Tournaments] (
	[Id] UNIQUEIDENTIFIER PRIMARY KEY,
	[Name] NVARCHAR(255),
	[StartDate] DATETIME,
	[EndDate] DATETIME,
	[Location] NVARCHAR(255)
)
GO

CREATE TABLE [TournamentPlayer] (
	[TournamentId] UNIQUEIDENTIFIER,
	[PlayerId] UNIQUEIDENTIFIER,
	[Point] FLOAT,
	[FinalRank] INT,
	PRIMARY KEY ([TournamentId], [PlayerId])
)
GO