USE [challenge]
GO

CREATE TABLE MerchantPerson (
    MerchantPersonId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PersonId UNIQUEIDENTIFIER NOT NULL,
    CNPJ VARCHAR(14) UNIQUE NOT NULL,   
    MerchantName VARCHAR(255) NOT NULL,          
    MerchantAddress VARCHAR(255),     
    MerchantContact VARCHAR(255),
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId) ON DELETE CASCADE
)
GO