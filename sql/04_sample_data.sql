-- Insert Players
DECLARE @whiteId UNIQUEIDENTIFIER = NEWID();
DECLARE @blackId UNIQUEIDENTIFIER = NEWID();

INSERT INTO Players (Id, Name, LastName, Sex, Nationality, FideRating, Title)
VALUES 
  (@whiteId, 'Magnus', 'Carlsen', 'Male', 'Norway', 2850, 'GM'),
  (@blackId, 'Hou', 'Yifan', 'Female', 'China', 2650, 'GM');

-- Insert Tournament
DECLARE @tournamentId UNIQUEIDENTIFIER = NEWID();

INSERT INTO Tournaments (Id, Name, StartDate, EndDate, Location)
VALUES (
  @tournamentId, 
  'World Chess Championship 2025', 
  '2025-11-01', 
  '2025-11-30', 
  'Dubai'
);

-- Insert Opening
DECLARE @openingId UNIQUEIDENTIFIER = NEWID();

INSERT INTO Openings (Id, Name, EcoCode, Moves)
VALUES (
  @openingId,
  'Sicilian Defense',
  'B20',
  'e4 c5'
);

-- Insert Game 
DECLARE @gameId UNIQUEIDENTIFIER = NEWID();

INSERT INTO Games (
  Id, WhitePlayerId, BlackPlayerId, Result, Moves, PlayedOn, OpeningId, TournamentId
)
VALUES (
  @gameId,
  @whiteId,
  @blackId,
  'WhiteWin',
  'e4 c5 Nf3 d6 d4 cxd4 Nxd4 Nf6 Nc3 a6',
  GETDATE(),
  @openingId,
  @tournamentId
);

-- Insert TournamentPlayer
INSERT INTO TournamentPlayer (TournamentId, PlayerId, Point, FinalRank)
VALUES 
  (@tournamentId, @whiteId, 8.5, 1),
  (@tournamentId, @blackId, 7.0, 2);
GO