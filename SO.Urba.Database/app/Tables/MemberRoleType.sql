CREATE TABLE [app].[MemberRoleType] (
    [memberRoleTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [name]             NVARCHAR (200) NOT NULL,
    [description]      NVARCHAR (MAX) NULL,
    [created]          DATETIME       CONSTRAINT [DF_MemberRoleType_created] DEFAULT (getdate()) NOT NULL,
    [modified]         DATETIME       CONSTRAINT [DF_MemberRoleType_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]        INT            NULL,
    [modifiedBy]       INT            NULL,
    [isActive]         BIT            CONSTRAINT [DF_MemberRoleType_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK__memberRoleTypeId] PRIMARY KEY CLUSTERED ([memberRoleTypeId] ASC)
);

