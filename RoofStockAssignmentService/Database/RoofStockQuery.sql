Create Database RoofStockDB
Go

USE RoofStockDB
Go

CREATE TABLE Addresses
(
		Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        Address1 NVARCHAR(MAX),
		Address2 NVARCHAR(MAX),
        City VARCHAR(100),
        Country VARCHAR(100),
        County VARCHAR(100),
        District VARCHAR(100),
        [State] VARCHAR(100),
        Zip VARCHAR(20),
        ZipPlus4 VARCHAR(20)
)
GO

CREATE TABLE Properties
(
		Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
        JsonId INT NOT NULL UNIQUE,
        AddressId INT,
        YearBuilt INT,
        ListPrice FLOAT,
        MonthlyRent FLOAT,
        GrossYield FLOAT
)
GO

CREATE PROC USP_GetProperties
AS
BEGIN
SELECT * FROM Properties
END
GO

CREATE PROC USP_SaveProperty(
        @JsonId INT,
        @YearBuilt INT,
        @ListPrice FLOAT,
        @MonthlyRent FLOAT,
        @GrossYield FLOAT,

		@Address1 NVARCHAR(MAX) NULL,
		@Address2 NVARCHAR(MAX) NULL,
        @City VARCHAR(100) NULL,
        @Country VARCHAR(100) NULL,
        @County VARCHAR(100) NULL,
        @District VARCHAR(100) NULL,
        @State VARCHAR(100) NULL,
        @Zip VARCHAR(20) NULL,
        @ZipPlus4 VARCHAR(20) NULL
)
AS
BEGIN TRY
BEGIN TRAN T1

DECLARE @address_id int
IF NOT EXISTS(SELECT Id FROM Properties WHERE JsonId = @JsonId)
BEGIN
INSERT INTO Addresses VALUES (@Address1, @Address2, @City, @Country, @County, @District, @State, @Zip, @ZipPlus4);
SET @address_id = SCOPE_IDENTITY()
INSERT INTO Properties VALUES (@JsonId, @address_id, @YearBuilt, @ListPrice, @MonthlyRent, @GrossYield);
END

COMMIT TRAN T1
END TRY
BEGIN CATCH

ROLLBACK TRAN T1;
throw;


END CATCH
GO