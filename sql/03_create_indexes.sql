CREATE INDEX idx_player_rating ON Players(FideRating);

CREATE INDEX idx_games_players ON Games(WhitePlayerId, BlackPlayerId);
GO