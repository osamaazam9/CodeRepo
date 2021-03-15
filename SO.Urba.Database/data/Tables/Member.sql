CREATE TABLE [data].[Member] (
    [memberId]           INT          IDENTITY (1, 1) NOT NULL,
    [contactInfoId]      INT          NULL,
    [username]           VARCHAR (50) NULL,
    [password]           VARCHAR (64) NULL,
    [forcePasswordReset] BIT          NULL,
    [created]            DATETIME     CONSTRAINT [DF_Member_created] DEFAULT (getdate()) NOT NULL,
    [modified]           DATETIME     CONSTRAINT [DF_Member_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]          INT          NULL,
    [modifiedBy]         INT          NULL,
    [isActive]           BIT          CONSTRAINT [DF_Member_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([memberId] ASC),
    CONSTRAINT [FK_Users_ContactInfo] FOREIGN KEY ([contactInfoId]) REFERENCES [data].[ContactInfo] ([contactInfoId]) ON DELETE CASCADE ON UPDATE CASCADE
);







