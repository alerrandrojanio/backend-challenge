USE [challenge]
GO

CREATE OR ALTER PROCEDURE [CreateToken]
    @token VARCHAR(1200),
    @expiration DATETIME,
    @userId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @GeneratedId TABLE (TokenId UNIQUEIDENTIFIER);

    INSERT INTO [Token] 
        ([Token],
        [Expiration],
        [UserId])
	OUTPUT INSERTED.TokenId INTO @GeneratedId
    VALUES 
        (@token,
        @expiration,
        @userId)
    
    SELECT TokenId FROM @GeneratedId;
END
GO