USE [challenge]
GO

CREATE OR ALTER PROCEDURE [CreateAccount]
    @personId VARCHAR(255),
    @accountNumber VARCHAR(20),
    @balance DECIMAL(18, 2)
AS
BEGIN
	DECLARE @GeneratedId TABLE (AccountId UNIQUEIDENTIFIER);

    INSERT INTO [Account] 
        ([PersonId],
        [AccountNumber],
        [Balance])
	OUTPUT INSERTED.AccountId INTO @GeneratedId
    VALUES 
        (@personId,
        @accountNumber,
        @balance)
    
    SELECT AccountId FROM @GeneratedId;
END
GO