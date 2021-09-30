CREATE PROCEDURE sp_CreateNewUser
@id INT OUTPUT,
@firstname NVARCHAR(MAX),
@lastname NVARCHAR(MAX),
@Username NVARCHAR(20),
@Address NVARCHAR(MAX),
@age INT

AS
BEGIN

IF(@Username IS NULL OR @Username = '')
raiserror('Username is null', 16, 1)

INSERT INTO Users  
VALUES (@firstname,@lastname,@Username,@Address,@age,@id)

SET @id = SCOPE_IDENTITY()

END