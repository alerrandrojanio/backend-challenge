USE [challenge]
GO

CREATE OR ALTER PROCEDURE [UpdateAccountBalance]
    @accountId UNIQUEIDENTIFIER,
    @balance DECIMAL(10,2)
AS
BEGIN
    UPDATE
        [Account]
    SET
        [Balance] = @balance,
        [UpdatedAt] = GETDATE()
    WHERE
        [AccountId] = @accountId
END
GO