CREATE TABLE [data].[Organization] (
    [organizationId] INT             IDENTITY (1, 1) NOT NULL,
    [contactInfoId]  INT             NULL,
    [name]           VARCHAR (50)    NOT NULL,
    [feeAmount]      DECIMAL (18, 2) NULL,
    [hasPaidFee]     BIT             NULL,
    [created]        DATETIME        CONSTRAINT [DF_ContactCategoryMembership_created] DEFAULT (getdate()) NOT NULL,
    [modified]       DATETIME        CONSTRAINT [DF_ContactCategoryMembership_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]      INT             NULL,
    [modifiedBy]     INT             NULL,
    [isActive]       BIT             CONSTRAINT [DF_ContactCategoryMembership_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_MemberCategories] PRIMARY KEY CLUSTERED ([organizationId] ASC),
    CONSTRAINT [FK_Organization_ContactInfo] FOREIGN KEY ([contactInfoId]) REFERENCES [data].[ContactInfo] ([contactInfoId])
);





