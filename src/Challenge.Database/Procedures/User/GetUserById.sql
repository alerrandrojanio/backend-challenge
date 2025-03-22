USE [challenge]
GO

CREATE OR ALTER PROCEDURE [GetUserById]
    @userId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        [UserId],
        [Name],
        [Email],
        [PasswordHash],
        [CreatedAt]
    FROM 
        [User]
    WHERE 
        [UserId] = @userId
    OPTION (MAXDOP 1)
END
GO