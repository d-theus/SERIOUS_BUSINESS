
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/23/2012 14:10:49
-- Generated from EDMX file: F:\Users\Slowness\Programming\DB\SERIOUS_BUSINESS\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database1];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet] DROP CONSTRAINT [FK_OrderEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemSet] DROP CONSTRAINT [FK_StoreItem];
GO
IF OBJECT_ID(N'[dbo].[FK_CheckupOrRepairItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemSet] DROP CONSTRAINT [FK_CheckupOrRepairItem];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemSet] DROP CONSTRAINT [FK_OrderItem];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderCheckupOrRepair]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CheckupOrRepairSet] DROP CONSTRAINT [FK_OrderCheckupOrRepair];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderPrinting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PrintingSet] DROP CONSTRAINT [FK_OrderPrinting];
GO
IF OBJECT_ID(N'[dbo].[FK_ConsumerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_ConsumerOrder];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet];
GO
IF OBJECT_ID(N'[dbo].[ConsumerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsumerSet];
GO
IF OBJECT_ID(N'[dbo].[ItemCategorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemCategorySet];
GO
IF OBJECT_ID(N'[dbo].[ItemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemSet];
GO
IF OBJECT_ID(N'[dbo].[CheckupOrRepairSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CheckupOrRepairSet];
GO
IF OBJECT_ID(N'[dbo].[PrintingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PrintingSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [date] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [Consumer_name] nvarchar(max)  NOT NULL,
    [Consumer_phone] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [apptment] nvarchar(max)  NOT NULL,
    [login] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ConsumerSet'
CREATE TABLE [dbo].[ConsumerSet] (
    [name] nvarchar(max)  NOT NULL,
    [phone] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ItemCategorySet'
CREATE TABLE [dbo].[ItemCategorySet] (
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ItemSet'
CREATE TABLE [dbo].[ItemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [designation] nvarchar(max)  NOT NULL,
    [purchasePrice] float  NOT NULL,
    [salePrice] float  NOT NULL,
    [storeResidue] int  NOT NULL,
    [Store_Name] nvarchar(max)  NOT NULL,
    [CheckupOrRepair_orderID] int  NOT NULL,
    [Order_Id] int  NOT NULL
);
GO

-- Creating table 'CheckupOrRepairSet'
CREATE TABLE [dbo].[CheckupOrRepairSet] (
    [orderID] int  NOT NULL,
    [isCheckupNeeded] nvarchar(max)  NOT NULL,
    [isRepairNeeded] nvarchar(max)  NOT NULL,
    [servicePrice] nvarchar(max)  NOT NULL,
    [Order_Id] int  NOT NULL
);
GO

-- Creating table 'PrintingSet'
CREATE TABLE [dbo].[PrintingSet] (
    [orderID] int  NOT NULL,
    [count] int  NOT NULL,
    [format] nvarchar(max)  NOT NULL,
    [isEditNeeded] bit  NOT NULL,
    [Order_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [name], [phone] in table 'ConsumerSet'
ALTER TABLE [dbo].[ConsumerSet]
ADD CONSTRAINT [PK_ConsumerSet]
    PRIMARY KEY CLUSTERED ([name], [phone] ASC);
GO

-- Creating primary key on [Name] in table 'ItemCategorySet'
ALTER TABLE [dbo].[ItemCategorySet]
ADD CONSTRAINT [PK_ItemCategorySet]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- Creating primary key on [Id] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [PK_ItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [orderID] in table 'CheckupOrRepairSet'
ALTER TABLE [dbo].[CheckupOrRepairSet]
ADD CONSTRAINT [PK_CheckupOrRepairSet]
    PRIMARY KEY CLUSTERED ([orderID] ASC);
GO

-- Creating primary key on [orderID] in table 'PrintingSet'
ALTER TABLE [dbo].[PrintingSet]
ADD CONSTRAINT [PK_PrintingSet]
    PRIMARY KEY CLUSTERED ([orderID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [FK_OrderEmployee]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Store_Name] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [FK_StoreItem]
    FOREIGN KEY ([Store_Name])
    REFERENCES [dbo].[ItemCategorySet]
        ([Name])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreItem'
CREATE INDEX [IX_FK_StoreItem]
ON [dbo].[ItemSet]
    ([Store_Name]);
GO

-- Creating foreign key on [CheckupOrRepair_orderID] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [FK_CheckupOrRepairItem]
    FOREIGN KEY ([CheckupOrRepair_orderID])
    REFERENCES [dbo].[CheckupOrRepairSet]
        ([orderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckupOrRepairItem'
CREATE INDEX [IX_FK_CheckupOrRepairItem]
ON [dbo].[ItemSet]
    ([CheckupOrRepair_orderID]);
GO

-- Creating foreign key on [Order_Id] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [FK_OrderItem]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderItem'
CREATE INDEX [IX_FK_OrderItem]
ON [dbo].[ItemSet]
    ([Order_Id]);
GO

-- Creating foreign key on [Order_Id] in table 'CheckupOrRepairSet'
ALTER TABLE [dbo].[CheckupOrRepairSet]
ADD CONSTRAINT [FK_OrderCheckupOrRepair]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderCheckupOrRepair'
CREATE INDEX [IX_FK_OrderCheckupOrRepair]
ON [dbo].[CheckupOrRepairSet]
    ([Order_Id]);
GO

-- Creating foreign key on [Order_Id] in table 'PrintingSet'
ALTER TABLE [dbo].[PrintingSet]
ADD CONSTRAINT [FK_OrderPrinting]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderPrinting'
CREATE INDEX [IX_FK_OrderPrinting]
ON [dbo].[PrintingSet]
    ([Order_Id]);
GO

-- Creating foreign key on [Consumer_name], [Consumer_phone] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_ConsumerOrder]
    FOREIGN KEY ([Consumer_name], [Consumer_phone])
    REFERENCES [dbo].[ConsumerSet]
        ([name], [phone])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConsumerOrder'
CREATE INDEX [IX_FK_ConsumerOrder]
ON [dbo].[OrderSet]
    ([Consumer_name], [Consumer_phone]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------