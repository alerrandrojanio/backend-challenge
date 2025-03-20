USE [challenge]
GO

CREATE OR ALTER PROCEDURE [CreatePerson]
    @name VARCHAR(255),
    @email VARCHAR(255),
    @phone VARCHAR(20)
AS
BEGIN
	DECLARE @GeneratedId TABLE (PersonId UNIQUEIDENTIFIER);

    INSERT INTO [Person] 
        ([Name],
        [Email],
        [Phone])
	OUTPUT INSERTED.PersonId INTO @GeneratedId
    VALUES 
        (@name,
        @email,
        @phone)
    
    SELECT PersonId FROM @GeneratedId;
END
GO