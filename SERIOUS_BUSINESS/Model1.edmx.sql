
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/25/2012 10:27:33
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

IF OBJECT_ID(N'[dbo].[FK_StoreItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemSet] DROP CONSTRAINT [FK_StoreItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ConsumerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_ConsumerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_EmployeeOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderService]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PositionSet] DROP CONSTRAINT [FK_OrderService];
GO
IF OBJECT_ID(N'[dbo].[FK_AppointmentEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet] DROP CONSTRAINT [FK_AppointmentEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_ServiceItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PositionSet] DROP CONSTRAINT [FK_ServiceItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemCategoryItemParameter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemParameterSet] DROP CONSTRAINT [FK_ItemCategoryItemParameter];
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
IF OBJECT_ID(N'[dbo].[PositionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PositionSet];
GO
IF OBJECT_ID(N'[dbo].[AppointmentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AppointmentSet];
GO
IF OBJECT_ID(N'[dbo].[ItemParameterSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemParameterSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [date] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [consID] int  NOT NULL,
    [emplID] int  NOT NULL
);
GO

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [login] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [aptID] int  NOT NULL
);
GO

-- Creating table 'ConsumerSet'
CREATE TABLE [dbo].[ConsumerSet] (
    [name] nvarchar(max)  NOT NULL,
    [phone] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [id] int  NOT NULL
);
GO

-- Creating table 'ItemCategorySet'
CREATE TABLE [dbo].[ItemCategorySet] (
    [name] nvarchar(max)  NOT NULL,
    [id] int  NOT NULL
);
GO

-- Creating table 'ItemSet'
CREATE TABLE [dbo].[ItemSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [designation] nvarchar(max)  NOT NULL,
    [purchasePrice] float  NOT NULL,
    [salePrice] float  NOT NULL,
    [storeResidue] int  NOT NULL,
    [demand] nvarchar(max)  NOT NULL,
    [catID] int  NOT NULL
);
GO

-- Creating table 'PositionSet'
CREATE TABLE [dbo].[PositionSet] (
    [orderID] int IDENTITY(1,1) NOT NULL,
    [id] int  NOT NULL,
    [count] int  NOT NULL,
    [itemID] int  NOT NULL
);
GO

-- Creating table 'AppointmentSet'
CREATE TABLE [dbo].[AppointmentSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [accessModifier] smallint  NOT NULL
);
GO

-- Creating table 'ItemParameterSet'
CREATE TABLE [dbo].[ItemParameterSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [valueDbl] float  NOT NULL,
    [valueBool] bit  NOT NULL,
    [valueTxt] nvarchar(max)  NOT NULL,
    [catID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ConsumerSet'
ALTER TABLE [dbo].[ConsumerSet]
ADD CONSTRAINT [PK_ConsumerSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ItemCategorySet'
ALTER TABLE [dbo].[ItemCategorySet]
ADD CONSTRAINT [PK_ItemCategorySet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [PK_ItemSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'PositionSet'
ALTER TABLE [dbo].[PositionSet]
ADD CONSTRAINT [PK_PositionSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'AppointmentSet'
ALTER TABLE [dbo].[AppointmentSet]
ADD CONSTRAINT [PK_AppointmentSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ItemParameterSet'
ALTER TABLE [dbo].[ItemParameterSet]
ADD CONSTRAINT [PK_ItemParameterSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [catID] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [FK_StoreItem]
    FOREIGN KEY ([catID])
    REFERENCES [dbo].[ItemCategorySet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreItem'
CREATE INDEX [IX_FK_StoreItem]
ON [dbo].[ItemSet]
    ([catID]);
GO

-- Creating foreign key on [consID] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_ConsumerOrder]
    FOREIGN KEY ([consID])
    REFERENCES [dbo].[ConsumerSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConsumerOrder'
CREATE INDEX [IX_FK_ConsumerOrder]
ON [dbo].[OrderSet]
    ([consID]);
GO

-- Creating foreign key on [emplID] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_EmployeeOrder]
    FOREIGN KEY ([emplID])
    REFERENCES [dbo].[EmployeeSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeOrder'
CREATE INDEX [IX_FK_EmployeeOrder]
ON [dbo].[OrderSet]
    ([emplID]);
GO

-- Creating foreign key on [orderID] in table 'PositionSet'
ALTER TABLE [dbo].[PositionSet]
ADD CONSTRAINT [FK_OrderService]
    FOREIGN KEY ([orderID])
    REFERENCES [dbo].[OrderSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderService'
CREATE INDEX [IX_FK_OrderService]
ON [dbo].[PositionSet]
    ([orderID]);
GO

-- Creating foreign key on [aptID] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [FK_AppointmentEmployee]
    FOREIGN KEY ([aptID])
    REFERENCES [dbo].[AppointmentSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AppointmentEmployee'
CREATE INDEX [IX_FK_AppointmentEmployee]
ON [dbo].[EmployeeSet]
    ([aptID]);
GO

-- Creating foreign key on [itemID] in table 'PositionSet'
ALTER TABLE [dbo].[PositionSet]
ADD CONSTRAINT [FK_ServiceItem]
    FOREIGN KEY ([itemID])
    REFERENCES [dbo].[ItemSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceItem'
CREATE INDEX [IX_FK_ServiceItem]
ON [dbo].[PositionSet]
    ([itemID]);
GO

-- Creating foreign key on [catID] in table 'ItemParameterSet'
ALTER TABLE [dbo].[ItemParameterSet]
ADD CONSTRAINT [FK_ItemCategoryItemParameter]
    FOREIGN KEY ([catID])
    REFERENCES [dbo].[ItemCategorySet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemCategoryItemParameter'
CREATE INDEX [IX_FK_ItemCategoryItemParameter]
ON [dbo].[ItemParameterSet]
    ([catID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------