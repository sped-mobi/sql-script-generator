﻿/*******************************************************************************
--     This code was generated by a tool.
--     Runtime Version:4.0.30319.42000
--
--     Changes to this file may cause incorrect behavior and will be lost if
--     the code is regenerated.
*******************************************************************************/

PRINT ('========== Preparation ==========');

GO
USE [master];

GO
PRINT ('Dropping database [BePro_DB]');

GO
DROP DATABASE IF EXISTS [BePro_DB];

GO
PRINT ('Creating database [BePro_DB]');

GO
CREATE DATABASE [BePro_DB];

GO
USE [BePro_DB];

GO
PRINT ('========== Creating tables ==========');

GO
PRINT ('Creating table [dbo].[Student]');

GO
CREATE TABLE [BePro_DB].[dbo].[Student]
(
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]         VARCHAR (100) NOT NULL,
    [LastName]          VARCHAR (100) NOT NULL,
    [DistrictID]        INT           NOT NULL,
    [PrimaryDisability] VARCHAR (100) NULL,
    [PrimaryBehavior]   VARCHAR (100) NULL,
    [SecondaryBehavior] VARCHAR (100) NULL,
    [TertiaryBehavior]  VARCHAR (100) NULL
);

GO
PRINT ('Creating table [dbo].[Goal]');

GO
CREATE TABLE [BePro_DB].[dbo].[Goal]
(
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [Number]       INT           NULL,
    [TimeFrame]    VARCHAR (100) NULL,
    [Condition]    VARCHAR (100) NULL,
    [Behavior]     VARCHAR (100) NULL,
    [Criteria]     VARCHAR (100) NULL,
    [IsAcademic]   BIT           NULL,
    [IsFunctional] BIT           NULL
);

GO
PRINT ('Creating table [dbo].[Objective]');

GO
CREATE TABLE [BePro_DB].[dbo].[Objective]
(
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [GoalID]       INT           NULL,
    [Number]       INT           NULL,
    [TimeFrame]    VARCHAR (100) NULL,
    [Condition]    VARCHAR (100) NULL,
    [Behavior]     VARCHAR (100) NULL,
    [Criteria]     VARCHAR (100) NULL,
    [IsAcademic]   BIT           NULL,
    [IsFunctional] BIT           NULL
);

GO
PRINT ('========== Creating Primary Keys ==========');

GO
PRINT ('[1 - 3] Creating primary key constraint [PK_Student_ID] on table [Student]([ID])');

GO
ALTER TABLE [BePro_DB].[dbo].[Student]
    ADD CONSTRAINT [PK_Student_ID] PRIMARY KEY CLUSTERED ([ID] ASC);

GO
PRINT ('[2 - 3] Creating primary key constraint [PK_Goal_ID] on table [Goal]([ID])');

GO
ALTER TABLE [BePro_DB].[dbo].[Goal]
    ADD CONSTRAINT [PK_Goal_ID] PRIMARY KEY CLUSTERED ([ID] ASC);

GO
PRINT ('[3 - 3] Creating primary key constraint [PK_Objective_ID] on table [Objective]([ID])');

GO
ALTER TABLE [BePro_DB].[dbo].[Objective]
    ADD CONSTRAINT [PK_Objective_ID] PRIMARY KEY CLUSTERED ([ID] ASC);

GO
PRINT ('========== Creating Froreign Keys ==========');

GO
PRINT ('[1  - 1] Creating foriegn key constraint [FK_Objective_GoalID]');

GO
ALTER TABLE [BePro_DB].[dbo].[Objective]
    ADD CONSTRAINT [FK_Objective_GoalID] FOREIGN KEY ([GoalID]) REFERENCES [BePro_DB].[dbo].[Goal] ([ID]);

GO
