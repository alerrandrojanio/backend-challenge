USE [challenge]
GO

CREATE OR ALTER PROCEDURE [CreateDeposit]
    @personId VARCHAR(255),
    @accountNumber VARCHAR(20),
    @value DECIMAL(18, 2),
    @createdAt DATETIME
AS
BEGIN
	DECLARE @GeneratedId TABLE (DepositId UNIQUEIDENTIFIER);

    INSERT INTO [Deposit] 
        ([PersonId],
        [AccountNumber],
        [Value],
        [CreatedAt])
	OUTPUT INSERTED.DepositId INTO @GeneratedId
    VALUES 
        (@personId,
        @accountNumber,
        @value,
        @createdAt)
    
    SELECT DepositId FROM @GeneratedId;
END
GO