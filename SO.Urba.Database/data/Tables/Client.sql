CREATE TABLE [data].[Client] (
    [clientId]      INT             IDENTITY (1, 1) NOT NULL,
    [contactInfoId] INT             NOT NULL,
    [hasPaidFee]    BIT             NULL,
    [feeAmount]     DECIMAL (18, 2) NULL,
    [startingDate]  SMALLDATETIME   NULL,
    [endDate]       SMALLDATETIME   NULL,
    [created]       DATETIME        CONSTRAINT [DF_ContactMembership_created] DEFAULT (getdate()) NOT NULL,
    [modified]      DATETIME        CONSTRAINT [DF_ContactMembership_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]     INT             NULL,
    [modifiedBy]    INT             NULL,
    [isActive]      BIT             CONSTRAINT [DF_ContactMembership_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([clientId] ASC),
    CONSTRAINT [FK_Members_ContactInfo] FOREIGN KEY ([contactInfoId]) REFERENCES [data].[ContactInfo] ([contactInfoId]) ON DELETE CASCADE ON UPDATE CASCADE
);





