CREATE TABLE [app].[CompanyCategoryType] (
    [companyCategoryTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [name]                  VARCHAR (50) NULL,
    [created]               DATETIME     CONSTRAINT [DF_CompanyCategoryType_created] DEFAULT (getdate()) NOT NULL,
    [modified]              DATETIME     CONSTRAINT [DF_CompanyCategoryType_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]             INT          NULL,
    [modifiedBy]            INT          NULL,
    [isActive]              BIT          CONSTRAINT [DF_CompanyCategoryType_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ContractorCategories] PRIMARY KEY CLUSTERED ([companyCategoryTypeId] ASC)
);

