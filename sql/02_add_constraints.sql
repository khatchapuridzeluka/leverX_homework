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