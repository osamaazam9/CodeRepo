USE [UrbaWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Initialize data for default user: Admin

-- Create ContactInfo entry for admin to reference it's key in Member table
	-- empty strings INSERT INTO [data].[ContactInfo] ([contactInfoId], [firstName], [lastName], [address], [city], [state], [zip], [homePhone], [workPhone], [fax], [cell], [email], [created], [modified], [createdBy], [modifiedBy], [isActive]) VALUES (1, N'The', N'Admin', N'', N'', N'', N'', NULL, N'', NULL, NULL, N'', GETDATE(), GETDATE(), NULL, NULL, 1)
	INSERT INTO [data].[ContactInfo] ([firstName], [lastName], [address], [city], [state], [zip], [homePhone], [workPhone], [fax], [cell], [email], [created], [modified], [createdBy], [modifiedBy], [isActive]) 
			VALUES (N'The', N'Admin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETDATE(), GETDATE(), NULL, NULL, 1)

	DECLARE @newly_added_contact_info_id INT
	SET @newly_added_contact_info_id = @@IDENTITY

-- SET IDENTITY_INSERT [data].[ContactInfo] OFF


-- Create an entry in member table for the admin.
-- with hashed password
INSERT INTO [data].[Member] ([contactInfoId], [username], [password], [forcePasswordReset], [created], [modified], [createdBy], [modifiedBy], [isActive]) 
	VALUES (@newly_added_contact_info_id, N'admin', N'D033E22AE348AEB5660FC2140AEC35850C4DA997', NULL, GetDate(), GetDate(), NULL, NULL, 1)
	
	DECLARE @newly_added_member_id integer
	SET @newly_added_member_id = @@IDENTITY

-- Admin and User roles
Declare @admin_role_id INT
set @admin_role_id = NULL
SELECT  @admin_role_id = memberRoleTypeId FROM [app].[MemberRoleType] WHERE name = N'Admin'

Declare @user_role_id INT
set @user_role_id = NULL
SELECT  @user_role_id = memberRoleTypeId FROM [app].[MemberRoleType] WHERE name = N'User'

	
-- Assign Admin roles of Admin and User. Admin will create the rest of the users and assign appropriate roles.
INSERT INTO [data].[MemberRoleLookup] ([memberRoleId], [memberRoleTypeId], [memberId], [created], [modified], [createdBy], [modifiedBy], [isActive]) 
	VALUES (NEWID(), @admin_role_id, @newly_added_member_id, GETDATE(), GETDATE(), NULL, NULL, 1)
INSERT INTO [data].[MemberRoleLookup] ([memberRoleId], [memberRoleTypeId], [memberId], [created], [modified], [createdBy], [modifiedBy], [isActive]) 
	VALUES (NEWID(), @user_role_id, @newly_added_member_id, GETDATE(), GETDATE(), NULL, NULL, 1)

