
ALTER TABLE Players 
ADD CONSTRAINT DF_Players_Id DEFAULT NEWID() FOR Id;

ALTER TABLE Games 
ADD CONSTRAINT DF_Games_Id DEFAULT NEWID() FOR Id;

ALTER TABLE Openings 
ADD CONSTRAINT DF_Openings_Id DEFAULT NEWID() FOR Id;

ALTER TABLE Tournaments 
ADD CONSTRAINT DF_Tournaments_Id DEFAULT NEWID() FOR Id;

-- Games -> Players
ALTER TABLE Games ADD FOREIGN KEY (WhitePlayerId) REFERENCES Players(Id);
ALTER TABLE Games ADD FOREIGN KEY (BlackPlayerId) REFERENCES Players(Id);

-- Games -> Openings
ALTER TABLE Games ADD FOREIGN KEY (OpeningId) REFERENCES Openings(Id);

-- Games -> Tournaments
ALTER TABLE Games ADD FOREIGN KEY (TournamentId) REFERENCES Tournaments(Id);

--TournamentPlayer -> Tournaments
ALTER TABLE TournamentPlayer ADD FOREIGN KEY (TournamentId) REFERENCES Tournaments(Id);

ALTER TABLE TournamentPlayer ADD FOREIGN KEY (PlayerId) REFERENCES Players(Id);
GO