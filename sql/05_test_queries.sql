-- Listing players abovce 2600
SELECT Name, LastName, FideRating
FROM Players
WHERE FideRating > 2600;
GO

-- Listing games with player names/result
SELECT 
  G.Id AS GameId,
  WP.Name + ' ' + WP.LastName AS WhitePlayer,
  BP.Name + ' ' + BP.LastName AS BlackPlayer,
  G.Result,
  G.PlayedOn
FROM Games G
JOIN Players WP ON G.WhitePlayerId = WP.Id
JOIN Players BP ON G.BlackPlayerId = BP.Id;
GO

--show tournaments with player rankings
SELECT 
  T.Name AS Tournament,
  P.Name + ' ' + P.LastName AS Player,
  TP.Point,
  TP.FinalRank
FROM TournamentPlayer TP
JOIN Players P ON TP.PlayerId = P.Id
JOIN Tournaments T ON TP.TournamentId = T.Id;
GO

--show openings / home many times were used
SELECT 
  O.Name AS Opening,
  O.EcoCode,
  COUNT(G.Id) AS GamesPlayed
FROM Openings O
LEFT JOIN Games G ON G.OpeningId = O.Id
GROUP BY O.Name, O.EcoCode;
GO
