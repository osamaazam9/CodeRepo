CREATE TABLE [data].[Referral] (
    [referralId]            INT             IDENTITY (1, 1) NOT NULL,
    [clientId]              INT             NOT NULL,
    [companyId]             INT             NOT NULL,
    [companyCategoryTypeId] INT             NOT NULL,
    [referralDate]          SMALLDATETIME   NULL,
    [quote]                 DECIMAL (18, 2) NULL,
    [notified]              BIT             NULL,
    [accepted]              BIT             NULL,
    [finalQuote]            DECIMAL (18, 2) NULL,
    [finishDate]            SMALLDATETIME   NULL,
    [description]           VARCHAR (MAX)   NULL,
    [surveyComment]         VARCHAR (MAX)   NULL,
    [created]               DATETIME        CONSTRAINT [DF_Referral_created] DEFAULT (getdate()) NOT NULL,
    [modified]              DATETIME        CONSTRAINT [DF_Referral_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]             INT             NULL,
    [modifiedBy]            INT             NULL,
    [isActive]              BIT             CONSTRAINT [DF_Referral_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Referrals] PRIMARY KEY CLUSTERED ([referralId] ASC),
    CONSTRAINT [FK_Referrals_ContractorCategories] FOREIGN KEY ([companyCategoryTypeId]) REFERENCES [app].[CompanyCategoryType] ([companyCategoryTypeId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Referrals_Contractors] FOREIGN KEY ([companyId]) REFERENCES [data].[Company] ([companyId]),
    CONSTRAINT [FK_Referrals_Members] FOREIGN KEY ([clientId]) REFERENCES [data].[Client] ([clientId]) ON DELETE CASCADE ON UPDATE CASCADE
);









