USE [challenge]
GO

CREATE OR ALTER PROCEDURE [CreateUser]
    @name VARCHAR(100),
    @email VARCHAR(100),
    @passwordHash VARCHAR(255)
AS
BEGIN
	DECLARE @GeneratedId TABLE (UserId UNIQUEIDENTIFIER);

    INSERT INTO [User] 
        ([Name],
        [Email],
        [PasswordHash])
	OUTPUT INSERTED.UserId INTO @GeneratedId
    VALUES 
        (@name,
        @email,
        @passwordHash)
    
    SELECT UserId FROM @GeneratedId;
END
GO