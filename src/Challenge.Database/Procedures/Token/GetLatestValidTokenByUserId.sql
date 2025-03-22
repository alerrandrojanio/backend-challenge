USE [challenge]
GO

CREATE PROCEDURE [GetLatestValidTokenByUserId]
    @userId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP 1
        [TokenId],
        [Token],
        [Expiration],
        [UserId]
    FROM 
        [Token]
    WHERE 
        [UserId] = @userId
        AND Expiration > GETUTCDATE()
    ORDER BY CreatedAt DESC;
END
GO
