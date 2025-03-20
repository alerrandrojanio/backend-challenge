USE [challenge]
GO

CREATE OR ALTER PROCEDURE [MerchantPerson]
    @personId UNIQUEIDENTIFIER,
    @cnpj VARCHAR(14),
	@merchantName VARCHAR(255),
    @merchantAddress VARCHAR(255),
    @merchantContact VARCHAR(255)
AS
BEGIN
    DECLARE @GeneratedId TABLE (MerchantPersonId UNIQUEIDENTIFIER);
    
    INSERT INTO [IndividualPerson] 
        ([PersonId],
        [CNPJ],
		[MerchantName],
        [MerchantAddress],
        [MerchantContact])
    OUTPUT INSERTED.MerchantPersonId INTO @GeneratedId
    VALUES 
        (@personId,
        @cnpj,
		@merchantName,
        @merchantAddress,
        @merchantContact)
    
    SELECT MerchantPersonId FROM @GeneratedId;
END
GO