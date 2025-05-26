CREATE TABLE [Players] (
	[Id] UNIQUEIDENTIFIER PRIMARY KEY,
	[Name] NVARCHAR(255),
	[LastName] NVARCHAR(255),
	[Sex] NVARCHAR(255) NOT NULL CHECK (Sex IN ('Female', 'Male')),
	[Nationality] NVARCHAR(255) NOT NULL CHECK (
		Nationality IN ('Unknown', 'Georgia', 'Norway', 'USA', 'Russia', 'France', 'India', 'China', 'Netherlands', 'Azerbaijan', 'Poland', 'Vietnam', 'Egypt', 'Romania', 'Uzbekistan')
		),
	[FideRating] INT,
    [Title] NVARCHAR(255) NOT NULL CHECK (
		Title IN ('None', 'WCM', 'WFM', 'WIM', 'WGM', 'CM', 'FM', 'IM', 'GM')
	)
)
GO

CREATE TABLE [Games] (
	[Id] UNIQUEIDENTIFIER PRIMARY KEY,
	[WhitePlayerId] UNIQUEIDENTIFIER,
	[BlackPlayerId] UNIQUEIDENTIFIER,
	[Result] NVARCHAR(255) NOT NULL CHECK (
		Result IN ('NotDefined', 'WhiteWin', 'BlackWin', 'Draw')
	),
	[Moves] NVARCHAR(MAX),
	[PlayedOn] DATETIME,
	[OpeningId] UNIQUEIDENTIFIER,
	[TournamentId] UNIQUEIDENTIFIER
)
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