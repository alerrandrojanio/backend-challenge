USE [challenge]
GO

CREATE OR ALTER PROCEDURE [CreateUserToken]
    @token VARCHAR(1200),
    @expiration DATETIME,
    @userId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @GeneratedId TABLE (UserTokenId UNIQUEIDENTIFIER);

    INSERT INTO [UserToken] 
        ([Token],
        [Expiration],
        [UserId])
	OUTPUT INSERTED.UserTokenId INTO @GeneratedId
    VALUES 
        (@token,
        @expiration,
        @userId)
    
    SELECT UserTokenId FROM @GeneratedId;
END
GO