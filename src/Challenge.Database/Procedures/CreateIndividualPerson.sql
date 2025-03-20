USE [challenge]
GO

CREATE OR ALTER PROCEDURE [CreateIndividualPerson]
    @personId UNIQUEIDENTIFIER,
    @cpf VARCHAR(11),
	@birthDate DATETIME
AS
BEGIN
    DECLARE @GeneratedId TABLE (IndividualPersonId UNIQUEIDENTIFIER);
    
    INSERT INTO [IndividualPerson] 
        ([PersonId],
        [CPF],
		[BirthDate])
    OUTPUT INSERTED.IndividualPersonId INTO @GeneratedId
    VALUES 
        (@personId,
        @cpf,
		@birthDate)
    
    SELECT IndividualPersonId FROM @GeneratedId;
END
GO