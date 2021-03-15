/*
1. 
	add IF-s for drops ?? do I need them ?

2.
MemberRoleLookup ... two entries (as from DataGenerate)

These data -> add at the end of the file, togeter.  for Admin only 
	 contact info
	 member ?
	 member role  ... 2 entries -> role admin and role user

*/

IF OBJECT_ID('[data].[MemberRoleLookup]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP [MemberRoleLookup] -- -- -- '
	DROP TABLE  [UrbaWeb].[data].[MemberRoleLookup]
END

IF OBJECT_ID('[app].[MemberRoleType]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP member role type -- -- -- '
	DROP TABLE [UrbaWeb].[app].[MemberRoleType]
END


IF OBJECT_ID('[data].[Member]', 'U') IS NOT NULL 
BEGIN
	print '-- -- -- DROP member -- -- -- '
	DROP TABLE [UrbaWeb].[data].[Member]
END

IF OBJECT_ID('[data].[SurveyAnswer]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP survey answer -- -- -- '
	DROP TABLE [UrbaWeb].[data].[SurveyAnswer] 
END

IF OBJECT_ID('[data].[Referral]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP referral -- -- -- '
	DROP TABLE [UrbaWeb].[data].[Referral]
END

IF OBJECT_ID('[data].[ClientOrganizationLookup]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP ClientOrganizationLookup -- -- -- '
	DROP TABLE [UrbaWeb].[data].[ClientOrganizationLookup]
END

IF OBJECT_ID('[data].[Organization]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP Organization -- -- -- '
	DROP TABLE  [UrbaWeb].[data].[Organization]
END

IF OBJECT_ID('[data].[Client]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP Client -- -- -- '
	DROP TABLE  [UrbaWeb].[data].[Client]
END

IF OBJECT_ID('[data].[CompanyCategoryLookup]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP CompanyCategoryLookup -- -- -- '
	DROP TABLE  [UrbaWeb].[data].[CompanyCategoryLookup]
END

IF OBJECT_ID('[app].[CompanyCategoryType]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP CompanyCategoryType -- -- -- '
	DROP TABLE  [UrbaWeb].[app].[CompanyCategoryType]
END

IF OBJECT_ID('[data].[Company]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP company -- -- -- '
	DROP TABLE  [UrbaWeb].[data].[Company]
END

IF OBJECT_ID('[app].[QuestionType]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP QuestionType -- -- -- '
	DROP TABLE  [UrbaWeb].[app].[QuestionType]
END

IF OBJECT_ID('[data].[ContactInfo]', 'U') IS NOT NULL
BEGIN
	print '-- -- -- DROP ContactInfo -- -- -- '
	DROP TABLE  [UrbaWeb].[data].[ContactInfo]
END


USE [UrbaWeb]
GO

print ' '
print '-- -- --  -- -- '
print ' '

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

print '------------------------------- Create MemberRoe------------------------------------'
/****** Object:  Table [app].[MemberRoleType]    Script Date: 2/27/2014 12:59:38 AM ******/
CREATE TABLE [app].[MemberRoleType](
	[memberRoleTypeId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[description] [nvarchar](max) NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK__memberRoleTypeId] PRIMARY KEY CLUSTERED 
	(
		[memberRoleTypeId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE [app].[MemberRoleType] ADD  CONSTRAINT [DF_MemberRoleType_created]  DEFAULT (getdate()) FOR [created]
	ALTER TABLE [app].[MemberRoleType] ADD  CONSTRAINT [DF_MemberRoleType_modified]  DEFAULT (getdate()) FOR [modified]
	ALTER TABLE [app].[MemberRoleType] ADD  CONSTRAINT [DF_MemberRoleType_isActive]  DEFAULT ((1)) FOR [isActive]
	GO


print '-- -- --  CompanyCategoryType -- -- -- '
SET ANSI_PADDING ON

CREATE TABLE [UrbaWeb].[app].[CompanyCategoryType](
	[companyCategoryTypeId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK_ContractorCategories] PRIMARY KEY CLUSTERED 
	(
		[companyCategoryTypeId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]


SET ANSI_PADDING OFF
ALTER TABLE [UrbaWeb].[app].[CompanyCategoryType] ADD  CONSTRAINT [DF_CompanyCategoryType_created]  DEFAULT (getdate()) FOR [created]
ALTER TABLE [UrbaWeb].[app].[CompanyCategoryType] ADD  CONSTRAINT [DF_CompanyCategoryType_modified]  DEFAULT (getdate()) FOR [modified]
ALTER TABLE [UrbaWeb].[app].[CompanyCategoryType] ADD  CONSTRAINT [DF_CompanyCategoryType_isActive]  DEFAULT ((1)) FOR [isActive]
GO

SET IDENTITY_INSERT [app].[CompanyCategoryType] ON
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (1, N'Dentists', GETDATE(), N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (2, N'Cleaners', GETDATE(), N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (3, N'Programmers', GETDATE(), N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (4, N'Dusters', GETDATE(), N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (5, N'Caterers', GETDATE(), N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (8, N'Plumbers', GETDATE(), N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (9, N'RealEstate', GETDATE(), N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (10, N'Broker', N'2014-02-26 00:07:38', N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (11, N'Printing', N'2014-02-26 00:07:38', N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (14, N'EconomicDevlopment', N'2014-02-26 00:07:38', N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (16, N'BailBonds', N'2014-02-26 00:07:38', N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (18, N'Engineer-Mechanical', N'2014-02-26 00:07:38', N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (19, N'Engineer-Structural', N'2014-02-26 00:07:38', N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (20, N'Engineer-Civil', N'2014-02-26 00:07:38', N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (21, N'TestCategory', N'2014-02-26 00:07:38', N'2014-02-26 00:07:38', NULL, NULL, 1)
INSERT INTO [UrbaWeb].[app].[CompanyCategoryType] ([companyCategoryTypeId], [name], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (22, N'Another Test Categor', N'2014-02-26 00:07:38', N'2014-02-26 00:07:38', NULL, NULL, 1)
SET IDENTITY_INSERT [app].[CompanyCategoryType] OFF
GO


print '--------------------- Create ContactInfo --------------------------- '
SET ANSI_PADDING ON
CREATE TABLE [data].[ContactInfo](
	[contactInfoId] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [varchar](50) NULL,
	[lastName] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[city] [varchar](50) NULL,
	[state] [varchar](3) NULL,
	[zip] [varchar](10) NULL,
	[homePhone] [varchar](20) NULL,
	[workPhone] [varchar](20) NULL,
	[fax] [varchar](20) NULL,
	[cell] [varchar](20) NULL,
	[email] [varchar](50) NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK_ContactInfo] PRIMARY KEY CLUSTERED 
	(
		[contactInfoId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET ANSI_PADDING OFF

	ALTER TABLE [data].[ContactInfo] ADD  CONSTRAINT [DF_ContactInfo_created]  DEFAULT (getdate()) FOR [created]
	ALTER TABLE [data].[ContactInfo] ADD  CONSTRAINT [DF_ContactInfo_modified]  DEFAULT (getdate()) FOR [modified]
	ALTER TABLE [data].[ContactInfo] ADD  CONSTRAINT [DF_ContactInfo_isActive]  DEFAULT ((1)) FOR [isActive]
	GO




print '------------------------------------ Create Organization ---------------------------'
/****** Object:  Table [data].[Organization]    Script Date: 2/26/2014 10:03:44 PM ******/
SET ANSI_PADDING ON
CREATE TABLE [UrbaWeb].[data].[Organization](
	[organizationId] [int] IDENTITY(1,1) NOT NULL,
	[contactInfoId] [int] NULL,
	[name] [varchar](50) NOT NULL,
	[feeAmount] [decimal](18, 2) NULL,
	[hasPaidFee] [bit] NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK_MemberCategories] PRIMARY KEY CLUSTERED 
	(
		[organizationId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET ANSI_PADDING OFF

	ALTER TABLE [data].[Organization] ADD  CONSTRAINT [DF_ContactCategoryMembership_created]  DEFAULT (getdate()) FOR [created]
	ALTER TABLE [data].[Organization] ADD  CONSTRAINT [DF_ContactCategoryMembership_modified]  DEFAULT (getdate()) FOR [modified]
	ALTER TABLE [data].[Organization] ADD  CONSTRAINT [DF_ContactCategoryMembership_isActive]  DEFAULT ((1)) FOR [isActive]

	ALTER TABLE [data].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_ContactInfo] FOREIGN KEY([contactInfoId])
	REFERENCES [data].[ContactInfo] ([contactInfoId])

	ALTER TABLE [data].[Organization] CHECK CONSTRAINT [FK_Organization_ContactInfo]
	GO




print '------------------------------ Create CLient  -------------------------------------- '

CREATE TABLE [data].[Client] (
    [clientId]      INT           IDENTITY (1, 1) NOT NULL,
    [contactInfoId] INT           NOT NULL,
    [hasPaidFee]    BIT           NULL,
    [feeAmount]     DECIMAL(18, 2)    NULL,
    [startingDate]  SMALLDATETIME NULL,
    [endDate]       SMALLDATETIME NULL,
    [created]       DATETIME      CONSTRAINT [DF_ContactMembership_created] DEFAULT (getdate()) NOT NULL,
    [modified]      DATETIME      CONSTRAINT [DF_ContactMembership_modified] DEFAULT (getdate()) NOT NULL,
    [createdBy]     INT           NULL,
    [modifiedBy]    INT           NULL,
    [isActive]      BIT           CONSTRAINT [DF_ContactMembership_isActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([clientId] ASC),
    CONSTRAINT [FK_Members_ContactInfo] FOREIGN KEY ([contactInfoId]) REFERENCES [data].[ContactInfo] ([contactInfoId]) ON DELETE CASCADE ON UPDATE CASCADE
);


print '------------------------ Create ClientOrganizationLookup ------------------------------------'
/****** Object:  Table [data].[ClientOrganizationLookup]    Script Date: 2/27/2014 1:14:12 AM ******/
CREATE TABLE [data].[ClientOrganizationLookup](
	[clientOrganizationLookupId] [uniqueidentifier] NOT NULL,
	[clientId] [int] NOT NULL,
	[organizationId] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK_ClientOrganizationLookup] PRIMARY KEY CLUSTERED 
	(
		[clientOrganizationLookupId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [data].[ClientOrganizationLookup] ADD  CONSTRAINT [DF_ClientOrganizationLookup_contactOrganizationLookupId]  DEFAULT (newid()) FOR [clientOrganizationLookupId]
	ALTER TABLE [data].[ClientOrganizationLookup] ADD  CONSTRAINT [DF_ClientOrganizationLookup_created]  DEFAULT (getdate()) FOR [created]
	ALTER TABLE [data].[ClientOrganizationLookup] ADD  CONSTRAINT [DF_ClientOrganizationLookup_modified]  DEFAULT (getdate()) FOR [modified]
	ALTER TABLE [data].[ClientOrganizationLookup] ADD  CONSTRAINT [DF_ClientOrganizationLookup_isActive]  DEFAULT ((1)) FOR [isActive]

	ALTER TABLE [data].[ClientOrganizationLookup]  WITH CHECK ADD  CONSTRAINT [FK_ClientOrganizationLookup_Client] FOREIGN KEY([clientId])
	REFERENCES [data].[Client] ([clientId])
	ON UPDATE CASCADE
	ON DELETE CASCADE

	ALTER TABLE [data].[ClientOrganizationLookup] CHECK CONSTRAINT [FK_ClientOrganizationLookup_Client]

	ALTER TABLE [data].[ClientOrganizationLookup]  WITH CHECK ADD  CONSTRAINT [FK_ClientOrganizationLookup_Organization] FOREIGN KEY([organizationId])
	REFERENCES [data].[Organization] ([organizationId])
	ON UPDATE CASCADE
	ON DELETE CASCADE

	ALTER TABLE [data].[ClientOrganizationLookup] CHECK CONSTRAINT [FK_ClientOrganizationLookup_Organization]
	GO



print '--------------------- Create QuestionType --------------------------- '
/****** Object:  Table [app].[QuestionType]    Script Date: 2/26/2014 9:04:54 PM ******/
CREATE TABLE [app].[QuestionType](
	[questionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[questionText] [text] NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
	(
		[questionTypeId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE [app].[QuestionType] ADD  CONSTRAINT [DF_QuestionType_created]  DEFAULT (getdate()) FOR [created]
	ALTER TABLE [app].[QuestionType] ADD  CONSTRAINT [DF_QuestionType_modified]  DEFAULT (getdate()) FOR [modified]
	ALTER TABLE [app].[QuestionType] ADD  CONSTRAINT [DF_QuestionType_isActive]  DEFAULT ((1)) FOR [isActive]
	GO

	SET IDENTITY_INSERT [app].[QuestionType] ON
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (1, N'Were you satisfied with the service?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (2, N'Were you satisfied with the price and value of the work?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (3, N'Was the work completed within the stated time?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (4, N'Was the provider usually on time?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (5, N'Was the estimate close to the final cost?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (6, N'Do you think the service provider had enough expertise?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (7, N'Did the provider respond to your questions timely?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (8, N'Did the provider provide license or insurance?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (9, N'What was your overall assessment of the work?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (10, N'Would you recommend this provider?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	INSERT INTO [app].[QuestionType] ([questionTypeId], [questionText], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (11, N'How was our call center service?', N'2014-02-26 00:07:39', N'2014-02-26 00:07:39', NULL, NULL, 1)
	SET IDENTITY_INSERT [app].[QuestionType] OFF
	GO
		
print '--------------------- Create Member --------------------------- '
/****** Object:  Table [data].[Member]    Script Date: 2/26/2014 9:05:27 PM ******/
SET ANSI_PADDING ON
CREATE TABLE [data].[Member](
	[memberId] [int] IDENTITY(1,1) NOT NULL,
	[contactInfoId] [int] NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](64) NULL,
	[forcePasswordReset] [bit] NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
	(
		[memberId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET ANSI_PADDING OFF

	ALTER TABLE [data].[Member] ADD  CONSTRAINT [DF_Member_created]  DEFAULT (getdate()) FOR [created]
	ALTER TABLE [data].[Member] ADD  CONSTRAINT [DF_Member_modified]  DEFAULT (getdate()) FOR [modified]
	ALTER TABLE [data].[Member] ADD  CONSTRAINT [DF_Member_isActive]  DEFAULT ((1)) FOR [isActive]

	ALTER TABLE [data].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Users_ContactInfo] FOREIGN KEY([contactInfoId])
	REFERENCES [data].[ContactInfo] ([contactInfoId])
	ON UPDATE CASCADE
	ON DELETE CASCADE

	ALTER TABLE [data].[Member] CHECK CONSTRAINT [FK_Users_ContactInfo]
	GO

print '------------------------Create MemberRoleLookup ---------------------'
/****** Object:  Table [data].[MemberRoleLookup]    Script Date: 2/27/2014 1:30:17 AM ******/
CREATE TABLE [data].[MemberRoleLookup](
	[memberRoleId] [uniqueidentifier] NOT NULL,
	[memberRoleTypeId] [int] NULL,
	[memberId] [int] NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[memberRoleId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [data].[MemberRoleLookup] ADD  DEFAULT (newid()) FOR [memberRoleId]
	ALTER TABLE [data].[MemberRoleLookup] ADD  DEFAULT (getdate()) FOR [created]
	ALTER TABLE [data].[MemberRoleLookup] ADD  DEFAULT (getdate()) FOR [modified]
	ALTER TABLE [data].[MemberRoleLookup] ADD  DEFAULT ((1)) FOR [isActive]

	ALTER TABLE [data].[MemberRoleLookup]  WITH CHECK ADD  CONSTRAINT [FK_MemberRole_Member] FOREIGN KEY([memberId])
	REFERENCES [data].[Member] ([memberId])
	ON UPDATE CASCADE
	ON DELETE CASCADE

	ALTER TABLE [data].[MemberRoleLookup] CHECK CONSTRAINT [FK_MemberRole_Member]

	ALTER TABLE [data].[MemberRoleLookup]  WITH CHECK ADD  CONSTRAINT [FK_MemberRole_MemberRoleType] FOREIGN KEY([memberRoleTypeId])
	REFERENCES [app].[MemberRoleType] ([memberRoleTypeId])
	ON UPDATE CASCADE
	ON DELETE CASCADE

	ALTER TABLE [data].[MemberRoleLookup] CHECK CONSTRAINT [FK_MemberRole_MemberRoleType]
	GO



print '--------------------- Create Company --------------------------- '
/****** Object:  Table [data].[Company]    Script Date: 2/27/2014 1:17:26 AM ******/
SET ANSI_PADDING ON
CREATE TABLE [data].[Company](
	[companyId] [int] IDENTITY(1,1) NOT NULL,
	[companyName] [varchar](50) NOT NULL,
	[contactInfoId] [int] NOT NULL,
	[license] [varchar](20) NULL,
	[bonded] [varchar](1) NULL,
	[agreementSigned] [smalldatetime] NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_Contractors] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF
ALTER TABLE [data].[Company] ADD  CONSTRAINT [DF_Company_created]  DEFAULT (getdate()) FOR [created]
ALTER TABLE [data].[Company] ADD  CONSTRAINT [DF_Company_modified]  DEFAULT (getdate()) FOR [modified]
ALTER TABLE [data].[Company] ADD  CONSTRAINT [DF_Company_isActive]  DEFAULT ((1)) FOR [isActive]

ALTER TABLE [data].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Contractors_ContactInfo] FOREIGN KEY([contactInfoId])
REFERENCES [data].[ContactInfo] ([contactInfoId])
ON UPDATE CASCADE
ON DELETE CASCADE

ALTER TABLE [data].[Company] CHECK CONSTRAINT [FK_Contractors_ContactInfo]
GO

print '------------------------------------- Create CompanyCategoryLookup -------------------------------'
/****** Object:  Table [data].[CompanyCategoryLookup]    Script Date: 2/27/2014 1:19:15 AM ******/
CREATE TABLE [data].[CompanyCategoryLookup](
	[companyCategoryId] [uniqueidentifier] NOT NULL,
	[companyId] [int] NOT NULL,
	[companyCategoryTypeId] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK_CompanyCategory] PRIMARY KEY CLUSTERED 
	(
		[companyCategoryId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [data].[CompanyCategoryLookup] ADD  CONSTRAINT [DF_CompanyCategory_companyCategoryId]  DEFAULT (newid()) FOR [companyCategoryId]
	ALTER TABLE [data].[CompanyCategoryLookup] ADD  CONSTRAINT [DF_CompanyCategoryLookup_created]  DEFAULT (getdate()) FOR [created]
	ALTER TABLE [data].[CompanyCategoryLookup] ADD  CONSTRAINT [DF_CompanyCategoryLookup_modified]  DEFAULT (getdate()) FOR [modified]
	ALTER TABLE [data].[CompanyCategoryLookup] ADD  CONSTRAINT [DF_CompanyCategoryLookup_isActive]  DEFAULT ((1)) FOR [isActive]

	ALTER TABLE [data].[CompanyCategoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_ContractorCategoryLookup_ContractorCategories] FOREIGN KEY([companyCategoryTypeId])
	REFERENCES [app].[CompanyCategoryType] ([companyCategoryTypeId])
	ON UPDATE CASCADE
	ON DELETE CASCADE

	ALTER TABLE [data].[CompanyCategoryLookup] CHECK CONSTRAINT [FK_ContractorCategoryLookup_ContractorCategories]

	ALTER TABLE [data].[CompanyCategoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_ContractorCategoryLookup_Contractors] FOREIGN KEY([companyId])
	REFERENCES [data].[Company] ([companyId])
	ON UPDATE CASCADE
	ON DELETE CASCADE

	ALTER TABLE [data].[CompanyCategoryLookup] CHECK CONSTRAINT [FK_ContractorCategoryLookup_Contractors]
	GO


print '------------------- Create [Referral] -- ----------------------------------- -- '
/****** Object:  Table [data].[Referral]    Script Date: 2/27/2014 1:33:18 AM ******/
SET ANSI_PADDING ON
CREATE TABLE [data].[Referral](
	[referralId] [int] IDENTITY(1,1) NOT NULL,
	[clientId] [int] NOT NULL,
	[companyId] [int] NOT NULL,
	[companyCategoryTypeId] [int] NOT NULL,
	[referralDate] [smalldatetime] NULL,
	[quote] [decimal](18, 2) NULL,
	[accepted] [varchar](1) NULL,
	[finalQuote] [decimal](18, 2) NULL,
	[finishDate] [smalldatetime] NULL,
	[description] [varchar](max) NULL,
	[surveyComment] [varchar](max) NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK_Referrals] PRIMARY KEY CLUSTERED 
	(
		[referralId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	SET ANSI_PADDING OFF
	ALTER TABLE [data].[Referral] ADD  CONSTRAINT [DF_Referral_created]  DEFAULT (getdate()) FOR [created]
	ALTER TABLE [data].[Referral] ADD  CONSTRAINT [DF_Referral_modified]  DEFAULT (getdate()) FOR [modified]
	ALTER TABLE [data].[Referral] ADD  CONSTRAINT [DF_Referral_isActive]  DEFAULT ((1)) FOR [isActive]

	ALTER TABLE [data].[Referral]  WITH CHECK ADD  CONSTRAINT [FK_Referrals_ContractorCategories] FOREIGN KEY([companyCategoryTypeId])
	REFERENCES [app].[CompanyCategoryType] ([companyCategoryTypeId])
	ON UPDATE CASCADE
	ON DELETE CASCADE

	ALTER TABLE [data].[Referral] CHECK CONSTRAINT [FK_Referrals_ContractorCategories]
	ALTER TABLE [data].[Referral]  WITH CHECK ADD  CONSTRAINT [FK_Referrals_Contractors] FOREIGN KEY([companyId])
	REFERENCES [data].[Company] ([companyId])

	ALTER TABLE [data].[Referral] CHECK CONSTRAINT [FK_Referrals_Contractors]

	ALTER TABLE [data].[Referral]  WITH CHECK ADD  CONSTRAINT [FK_Referrals_Members] FOREIGN KEY([clientId])
	REFERENCES [data].[Client] ([clientId])
	ON UPDATE CASCADE
	ON DELETE CASCADE

	ALTER TABLE [data].[Referral] CHECK CONSTRAINT [FK_Referrals_Members]
	GO

print '---------------------------------- Create SurveyAnswer --------------------------------------------'
CREATE TABLE [data].[SurveyAnswer](
	[surveyAnswerId] [uniqueidentifier] NOT NULL,
	[referralId] [int] NOT NULL,
	[questionTypeId] [int] NOT NULL,
	[answer] [int] NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NOT NULL,
	[createdBy] [int] NULL,
	[modifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	 CONSTRAINT [PK_SurveyAnswer] PRIMARY KEY CLUSTERED 
	(
		[surveyAnswerId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [data].[SurveyAnswer] ADD  CONSTRAINT [DF_SurveyAnswer_created]  DEFAULT (getdate()) FOR [created]
	ALTER TABLE [data].[SurveyAnswer] ADD  CONSTRAINT [DF_SurveyAnswer_modified]  DEFAULT (getdate()) FOR [modified]
	ALTER TABLE [data].[SurveyAnswer] ADD  CONSTRAINT [DF_SurveyAnswer_isActive]  DEFAULT ((1)) FOR [isActive]

	ALTER TABLE [data].[SurveyAnswer]  WITH CHECK ADD  CONSTRAINT [FK_SurveyAnswers_Questions] FOREIGN KEY([questionTypeId])
	REFERENCES [app].[QuestionType] ([questionTypeId])

	ALTER TABLE [data].[SurveyAnswer] CHECK CONSTRAINT [FK_SurveyAnswers_Questions]

	ALTER TABLE [data].[SurveyAnswer]  WITH CHECK ADD  CONSTRAINT [FK_SurveyAnswers_Referrals] FOREIGN KEY([referralId])
	REFERENCES [data].[Referral] ([referralId])
	ON UPDATE CASCADE
	ON DELETE CASCADE

	ALTER TABLE [data].[SurveyAnswer] CHECK CONSTRAINT [FK_SurveyAnswers_Referrals]
	GO

-- ----------------------------------------------------------------------------
-- Initialize data for default user: Admin

-- Create ContactInfo entry for admin to reference it's key in Member table
SET IDENTITY_INSERT [data].[ContactInfo] ON
-- empty strings INSERT INTO [data].[ContactInfo] ([contactInfoId], [firstName], [lastName], [address], [city], [state], [zip], [homePhone], [workPhone], [fax], [cell], [email], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (1, N'The', N'Admin', N'', N'', N'', N'', NULL, N'', NULL, NULL, N'', GETDATE(), GETDATE(), NULL, NULL, 1)
INSERT INTO [data].[ContactInfo] ([contactInfoId], [firstName], [lastName], [address], [city], [state], [zip], [homePhone], [workPhone], [fax], [cell], [email], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (1, N'The', N'Admin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETDATE(), GETDATE(), NULL, NULL, 1)
SET IDENTITY_INSERT [data].[ContactInfo] OFF

-- Create an entry in member table for the admin.
SET IDENTITY_INSERT [data].[Member] ON
--INSERT INTO [data].[Member] ([memberId], [contactInfoId], [username], [password], [forcePasswordReset], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (1, 1, N'admin', N'admin', NULL, GETDATE(), GETDATE(), NULL, NULL, 1)
-- with hashed password
INSERT INTO [data].[Member] ([memberId], [contactInfoId], [username], [password], [forcePasswordReset], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (1, 1, N'admin', N'D033E22AE348AEB5660FC2140AEC35850C4DA997', NULL, N'2014-02-27 13:50:44', N'2014-02-27 13:52:28', NULL, NULL, 1)
SET IDENTITY_INSERT [data].[Member] OFF

-- Admin and User roles
SET IDENTITY_INSERT [app].[MemberRoleType] ON
INSERT INTO [app].[MemberRoleType] ([memberRoleTypeId], [name], [description], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (1, N'Admin', NULL, GETDATE(), GETDATE(), NULL, NULL, 1)
INSERT INTO [app].[MemberRoleType] ([memberRoleTypeId], [name], [description], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (2, N'User', NULL, GETDATE(), GETDATE(), NULL, NULL, 1)
SET IDENTITY_INSERT [app].[MemberRoleType] OFF
	
-- Assign Admin roles of Admin and User. Admin will create the rest of the users and assign appropriate roles.
INSERT INTO [data].[MemberRoleLookup] ([memberRoleId], [memberRoleTypeId], [memberId], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (NEWID(), 1, 1, GETDATE(), GETDATE(), NULL, NULL, 1)
INSERT INTO [data].[MemberRoleLookup] ([memberRoleId], [memberRoleTypeId], [memberId], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (NEWID(), 2, 1, GETDATE(), GETDATE(), NULL, NULL, 1)

