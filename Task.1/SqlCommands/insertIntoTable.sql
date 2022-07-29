DECLARE @first AS INT = 1
DECLARE @last AS INT = 3

WHILE(@first <= @last)
BEGIN
INSERT INTO Markers(MarkerName, Latitude, Longitude) values(CONVERT(varchar, @first), RAND(10) *(@first * 10), RAND(10) * (@first * 10))
    SET @first += 1
END