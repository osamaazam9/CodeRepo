CREATE TABLE [data].[CompanyCategoryLookup] (
    [companyCategoryId]     UNIQUEIDENTIFIER CONSTRAINT [DF_CompanyCategory_companyCategoryId] DEFAULT (newid()) NOT NULL,
    [companyId]             INT              NOT NULL,
    [companyCategoryTypeId] INT              NOT NULL,
    [created]               DATETIME         CONSTRAINT [DF_CompanyCategoryLookup_created] DEFAULT (getdate()) NOT NULL,
    [modified]              DATETIME         CONSTRAINT [DF_CompanyCategoryLookup_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]             INT              NULL,
    [modifiedBy]            INT              NULL,
    [isActive]              BIT              CONSTRAINT [DF_CompanyCategoryLookup_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CompanyCategory] PRIMARY KEY CLUSTERED ([companyCategoryId] ASC),
    CONSTRAINT [FK_ContractorCategoryLookup_ContractorCategories] FOREIGN KEY ([companyCategoryTypeId]) REFERENCES [app].[CompanyCategoryType] ([companyCategoryTypeId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ContractorCategoryLookup_Contractors] FOREIGN KEY ([companyId]) REFERENCES [data].[Company] ([companyId]) ON DELETE CASCADE ON UPDATE CASCADE
);

