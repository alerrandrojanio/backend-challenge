USE [challenge]
GO

CREATE TABLE [User] (
    UserId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), 
    Name VARCHAR(100) NOT NULL,           
    Email VARCHAR(100) NOT NULL,              
    PasswordHash VARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL
);