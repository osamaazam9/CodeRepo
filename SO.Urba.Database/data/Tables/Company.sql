CREATE TABLE [data].[Company] (
    [companyId]       INT           IDENTITY (1, 1) NOT NULL,
    [companyName]     VARCHAR (50)  NOT NULL,
    [contactInfoId]   INT           NOT NULL,
    [license]         VARCHAR (20)  NULL,
    [bonded]          VARCHAR (1)   NULL,
    [agreementSigned] SMALLDATETIME NULL,
    [created]         DATETIME      CONSTRAINT [DF_Company_created] DEFAULT (getdate()) NOT NULL,
    [modified]        DATETIME      CONSTRAINT [DF_Company_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]       INT           NULL,
    [modifiedBy]      INT           NULL,
    [isActive]        BIT           CONSTRAINT [DF_Company_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Contractors] PRIMARY KEY CLUSTERED ([companyId] ASC),
    CONSTRAINT [FK_Contractors_ContactInfo] FOREIGN KEY ([contactInfoId]) REFERENCES [data].[ContactInfo] ([contactInfoId]) ON DELETE CASCADE ON UPDATE CASCADE
);



