-- ECommerce Database Schema and Stored Procedures

-- Product Table
CREATE TABLE [Product] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [ProductName] NVARCHAR(100) NOT NULL,
    [UnitPrice] MONEY NOT NULL
);
CREATE INDEX IX_Product_ProductName ON [Product]([ProductName]);

-- Customer Table
CREATE TABLE [Customer] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [CustomerName] NVARCHAR(100) NOT NULL,
    [ContactName] NVARCHAR(100),
    [ContactTitle] NVARCHAR(50),
    [Address] NVARCHAR(200),
    [City] NVARCHAR(50),
    [Region] NVARCHAR(50),
    [PostalCode] NVARCHAR(20),
    [Country] NVARCHAR(50),
    [Phone] NVARCHAR(20),
    [Fax] NVARCHAR(20)
);
CREATE INDEX IX_Customer_CustomerName ON [Customer]([CustomerName]);

-- Order Table
CREATE TABLE [Order] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [CustomerId] INT NOT NULL,
    [OrderDate] DATETIME NOT NULL,
    [RequiredDate] DATETIME NOT NULL,
    FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id])
);
CREATE INDEX IX_Order_CustomerId ON [Order]([CustomerId]);

-- OrderDetail Table
CREATE TABLE [OrderDetail] (
    [OrderId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    PRIMARY KEY ([OrderId], [ProductId]),
    FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id]),
    FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
);
CREATE INDEX IX_OrderDetail_OrderId ON [OrderDetail]([OrderId]);
CREATE INDEX IX_OrderDetail_ProductId ON [OrderDetail]([ProductId]);

-- Supplier Table
CREATE TABLE [Supplier] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [SupplierName] NVARCHAR(100) NOT NULL
);
CREATE INDEX IX_Supplier_SupplierName ON [Supplier]([SupplierName]);

-- Category Table
CREATE TABLE [Category] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [CategoryName] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(200)
);
CREATE INDEX IX_Category_CategoryName ON [Category]([CategoryName]);

-- Stored Procedures
GO
CREATE PROCEDURE sp_GetAllProducts
AS
BEGIN
    SELECT * FROM [Product];
END
GO
CREATE PROCEDURE sp_AddProduct
    @ProductName NVARCHAR(100),
    @UnitPrice MONEY
AS
BEGIN
    INSERT INTO [Product] ([ProductName], [UnitPrice]) VALUES (@ProductName, @UnitPrice);
END
GO
CREATE PROCEDURE sp_UpdateProduct
    @Id INT,
    @ProductName NVARCHAR(100),
    @UnitPrice MONEY
AS
BEGIN
    UPDATE [Product] SET [ProductName] = @ProductName, [UnitPrice] = @UnitPrice WHERE [Id] = @Id;
END
GO
CREATE PROCEDURE sp_DeleteProduct
    @Id INT
AS
BEGIN
    DELETE FROM [Product] WHERE [Id] = @Id;
END
GO
-- Add similar stored procedures for Customer, Order, etc. as needed
