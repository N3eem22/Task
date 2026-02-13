-- Sales Invoice ERP - SQL Server (MSSQL) Schema
-- Run this script in SQL Server Management Studio or sqlcmd against your SQL Server instance.
-- Create the database first if it doesn't exist, then run the rest in that database.

-- Optional: create database (run in 'master' or default context)
-- CREATE DATABASE ErpDb;
-- GO
-- USE ErpDb;
-- GO

-- Customers
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customers')
BEGIN
    CREATE TABLE Customers (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(200) NOT NULL,
        Address NVARCHAR(500) NULL,
        Phone NVARCHAR(50) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

-- Products
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE Products (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(200) NOT NULL,
        UnitPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
        Description NVARCHAR(500) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

-- Invoices
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Invoices')
BEGIN
    CREATE TABLE Invoices (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        InvoiceNumber NVARCHAR(50) NOT NULL UNIQUE,
        CustomerId INT NOT NULL,
        InvoiceDate DATE NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
        SubTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
        TaxPercent DECIMAL(5,2) NOT NULL DEFAULT 0,
        TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
        Total DECIMAL(18,2) NOT NULL DEFAULT 0,
        Notes NVARCHAR(1000) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_Invoices_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
    );
    CREATE INDEX IX_Invoices_CustomerId ON Invoices(CustomerId);
    CREATE INDEX IX_Invoices_InvoiceNumber ON Invoices(InvoiceNumber);
END
GO

-- Invoice Lines
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'InvoiceLines')
BEGIN
    CREATE TABLE InvoiceLines (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        InvoiceId INT NOT NULL,
        ProductId INT NOT NULL,
        Quantity DECIMAL(18,2) NOT NULL DEFAULT 1,
        UnitPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
        LineTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
        CONSTRAINT FK_InvoiceLines_Invoices FOREIGN KEY (InvoiceId) REFERENCES Invoices(Id) ON DELETE CASCADE,
        CONSTRAINT FK_InvoiceLines_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
    );
    CREATE INDEX IX_InvoiceLines_InvoiceId ON InvoiceLines(InvoiceId);
END
GO

-- Seed sample data (optional; run only if tables are empty)
IF (SELECT COUNT(*) FROM Customers) = 0
BEGIN
    SET IDENTITY_INSERT Customers ON;
    INSERT INTO Customers (Id, Name, Address, Phone) VALUES
    (1, N'Acme Corp', N'123 Main St', N'555-0100'),
    (2, N'Globex Inc', N'456 Oak Ave', N'555-0200'),
    (3, N'Initech', N'789 Tech Park', N'555-0300');
    SET IDENTITY_INSERT Customers OFF;
END
GO

IF (SELECT COUNT(*) FROM Products) = 0
BEGIN
    SET IDENTITY_INSERT Products ON;
    INSERT INTO Products (Id, Name, UnitPrice, Description) VALUES
    (1, N'Widget A', 29.99, N'Standard widget'),
    (2, N'Widget B', 49.99, N'Premium widget'),
    (3, N'Gadget X', 99.99, N'Advanced gadget'),
    (4, N'Service Pack', 19.99, N'Support package');
    SET IDENTITY_INSERT Products OFF;
END
GO
