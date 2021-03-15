CREATE TABLE [data].[ContactInfo] (
    [contactInfoId] INT          IDENTITY (1, 1) NOT NULL,
    [firstName]     VARCHAR (50) NULL,
    [lastName]      VARCHAR (50) NULL,
    [address]       VARCHAR (50) NULL,
    [city]          VARCHAR (50) NULL,
    [state]         VARCHAR (3)  NULL,
    [zip]           VARCHAR (10) NULL,
    [homePhone]     VARCHAR (20) NULL,
    [workPhone]     VARCHAR (20) NULL,
    [fax]           VARCHAR (20) NULL,
    [cell]          VARCHAR (20) NULL,
    [email]         VARCHAR (50) NULL,
	[preferredContactMethod] INT CONSTRAINT [DF_ContactInfo_preferredContactMethod] DEFAULT 0 NOT NULL,
    [created]       DATETIME     CONSTRAINT [DF_ContactInfo_created] DEFAULT (getdate()) NOT NULL,
    [modified]      DATETIME     CONSTRAINT [DF_ContactInfo_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]     INT          NULL,
    [modifiedBy]    INT          NULL,
    [isActive]      BIT          CONSTRAINT [DF_ContactInfo_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ContactInfo] PRIMARY KEY CLUSTERED ([contactInfoId] ASC)
);

