USE [challenge]
GO

CREATE OR ALTER PROCEDURE [CreateTransfer]
    @payerId UNIQUEIDENTIFIER,
    @payeeId UNIQUEIDENTIFIER,
    @value DECIMAL(10,2),
    @createdAt DATETIME
AS
BEGIN
	DECLARE @GeneratedId TABLE (TransferId UNIQUEIDENTIFIER);

    INSERT INTO [Transfer] 
        ([PayerId],
        [PayeeId],
        [Value],
        [CreatedAt])
	OUTPUT INSERTED.TransferId INTO @GeneratedId
    VALUES 
        (@payerId,
        @payeeId,
        @value,
        @createdAt)
    
    SELECT TransferId FROM @GeneratedId;
END
GO