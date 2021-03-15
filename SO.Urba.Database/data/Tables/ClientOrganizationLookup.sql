CREATE TABLE [data].[ClientOrganizationLookup] (
    [clientOrganizationLookupId] UNIQUEIDENTIFIER CONSTRAINT [DF_ClientOrganizationLookup_contactOrganizationLookupId] DEFAULT (newid()) NOT NULL,
    [clientId]                   INT              NOT NULL,
    [organizationId]             INT              NOT NULL,
    [created]                    DATETIME         CONSTRAINT [DF_ClientOrganizationLookup_created] DEFAULT (getdate()) NOT NULL,
    [modified]                   DATETIME         CONSTRAINT [DF_ClientOrganizationLookup_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]                  INT              NULL,
    [modifiedBy]                 INT              NULL,
    [isActive]                   BIT              CONSTRAINT [DF_ClientOrganizationLookup_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ClientOrganizationLookup] PRIMARY KEY CLUSTERED ([clientOrganizationLookupId] ASC),
    CONSTRAINT [FK_ClientOrganizationLookup_Client] FOREIGN KEY ([clientId]) REFERENCES [data].[Client] ([clientId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ClientOrganizationLookup_Organization] FOREIGN KEY ([organizationId]) REFERENCES [data].[Organization] ([organizationId]) ON DELETE CASCADE ON UPDATE CASCADE
);



