USE [challenge]
GO

CREATE OR ALTER PROCEDURE [GetMerchantPersonByPersonId]
    @personId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        [MerchantPersonId],
        [PersonId],
        [CNPJ],
		[MerchantName],
        [MerchantAddress],
        [MerchantContact]
    FROM 
        [MerchantPerson]
    WHERE 
        [PersonId] = @personId
    OPTION (MAXDOP 1)
END
GO