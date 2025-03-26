USE [challenge]
GO

CREATE OR ALTER PROCEDURE [GetEmailTemplateByType]
    @emailType VARCHAR(20)
AS
BEGIN
    SELECT 
        [EmailTemplateId],
        [Subject],
        [Body],
        [Type],
        [CreatedAt]
    FROM 
        [EmailTemplate]
    WHERE 
        [Type] = @emailType
    OPTION (MAXDOP 1)
END
GO