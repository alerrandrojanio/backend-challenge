USE [challenge]
GO

CREATE OR ALTER PROCEDURE [GetLatestValidTokenByUserId]
    @userId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1
        [UserTokenId],
        [Token],
        [Expiration],
        [UserId]
    FROM 
        [UserToken]
    WHERE 
        [UserId] = @userId
        AND Expiration > GETUTCDATE()
    ORDER BY CreatedAt DESC
END
GO
