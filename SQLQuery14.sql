CREATE TABLE Note (Id INT, Text NVARCHAR(100), Color NVARCHAR(10))

CREATE TABLE Tag (Name NVARCHAR(50), Text NVARCHAR(100), Color NVARCHAR(10))

INSERT INTO Note
VALUES ('78564','This is some quote.','yellow')

INSERT INTO Tag
VALUES ('Ampersand','Listen to this','green')

SELECT *
FROM Note
WHERE Id = '78564'

SELECT *
FROM Tag
WHERE Name = '%A'

SELECT *
FROM Note
ORDER BY Id ASC

SELECT *
FROM Tag
ORDER BY Color DESC

SELECT *
FROM Tag
WHERE Text = 's%'

SELECT *
FROM Tag
WHERE LEN(Name) = 9

SELECT *
FROM Note
INNER JOIN Tag t on Note.Text != t.Text AND LEN(Note.Text) < LEN(t.Text) 

SELECT *
FROM Note
INNER JOIN Tag t on Note.Color != t.Color AND LEN(Note.Color) < LEN(t.Color)