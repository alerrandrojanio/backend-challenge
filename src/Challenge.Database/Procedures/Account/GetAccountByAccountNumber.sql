USE [challenge]
GO

CREATE OR ALTER PROCEDURE [GetAccountByAccountNumber]
    @accountNumber VARCHAR(20)
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
        [AccountNumber] = @AccountNumber
    OPTION (MAXDOP 1)
END
GO