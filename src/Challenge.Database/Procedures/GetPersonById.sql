USE [challenge]
GO

CREATE OR ALTER PROCEDURE [GetPersonById]
    @personId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        [PersonId],
        [Name],
        [Email],
        [Phone],
        [CreatedAt]
    FROM 
        [Person]
    WHERE 
        [PersonId] = @personId
    OPTION (MAXDOP 1)
END
GO