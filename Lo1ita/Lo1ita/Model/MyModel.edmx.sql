
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/27/2017 22:05:46
-- Generated from EDMX file: E:\project\GitHub\MySite\Lo1ita\Lo1ita\Model\MyModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyWeb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Article]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article];
GO
IF OBJECT_ID(N'[dbo].[Attachment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attachment];
GO
IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Articles'
CREATE TABLE [dbo].[Articles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NULL,
    [Details] varchar(max)  NULL,
    [UpdateDate] datetime  NOT NULL,
    [CreatDate] datetime  NULL,
    [Display] int  NULL,
    [Author] nvarchar(50)  NULL,
    [CreaterGuid] nvarchar(50)  NULL,
    [Hits] int  NULL,
    [Excerpt] nvarchar(250)  NULL,
    [Type] int  NULL,
    [isDraft] int  NULL,
    [Textlength] int  NULL
);
GO

-- Creating table 'Attachments'
CREATE TABLE [dbo].[Attachments] (
    [ID] int  NOT NULL,
    [RefTableID] int  NULL,
    [RefTableName] nvarchar(100)  NULL,
    [Name] nvarchar(256)  NULL,
    [Extension] nvarchar(50)  NULL,
    [DisplayName] nchar(256)  NULL,
    [RelativePath] nvarchar(256)  NULL,
    [ContentType] nvarchar(100)  NULL,
    [CreatDate] datetime  NULL,
    [IsDeleted] int  NULL
);
GO

-- Creating table 'UserInfoes'
CREATE TABLE [dbo].[UserInfoes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserGuid] nvarchar(50)  NULL,
    [UserName] nvarchar(50)  NULL,
    [PassWord] nvarchar(50)  NULL,
    [Gender] int  NULL,
    [Birthdate] datetime  NULL,
    [CreatDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [PK_Articles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Attachments'
ALTER TABLE [dbo].[Attachments]
ADD CONSTRAINT [PK_Attachments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UserInfoes'
ALTER TABLE [dbo].[UserInfoes]
ADD CONSTRAINT [PK_UserInfoes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------