USE [challenge]
GO

CREATE OR ALTER PROCEDURE [GetAccountByPersonId]
    @personId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        [AccountId],
        [PersonId],
        [AccountNumber],
        [Balance],
        [CreatedAt]
    FROM 
        [Account]
    WHERE 
        [PersonId] = @personId
    OPTION (MAXDOP 1)
END
GO