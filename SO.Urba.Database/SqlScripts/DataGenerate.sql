-- delete first, then insert

-- DON'T CHANGE THE ORDER OF DELETE STATEMENNTS!    also Don't change the order of Insets below
-- apparently, because of Foreign Keys relationship, the order of some tables deletions and insertions is important
print '-- -- -- delete CompanyCategoryType -- -- -- '
delete from [UrbaWeb].[app].[CompanyCategoryType]
print '-- -- -- delete member role type -- -- -- '
delete from [UrbaWeb].[app].[MemberRoleType]
print '-- -- -- delete [QuestionType] -- -- -- '
delete from [UrbaWeb].[app].[QuestionType]
print '-- -- -- delete Client -- -- -- '
DELETE FROM [UrbaWeb].[data].[Client]
print '-- -- -- delete ClientOrganizationLookup -- -- -- '
delete from [UrbaWeb].[data].[ClientOrganizationLookup]
print '-- -- -- delete company -- -- -- '
DELETE FROM [UrbaWeb].[data].[Company]
print '-- -- -- delete [CompanyCategoryLookup] -- -- -- '
DELETE FROM [UrbaWeb].[data].[CompanyCategoryLookup]
print '-- -- -- delete [Organization] -- -- -- '
DELETE FROM [UrbaWeb].[data].[Organization]
print '-- -- -- delete contact info -- -- -- '
DELETE FROM [UrbaWeb].[data].[ContactInfo]
print '-- -- -- delete member -- -- -- '
DELETE FROM [UrbaWeb].[data].[Member]
print '-- -- -- delete [MemberRoleLookup] -- -- -- '
delete from [UrbaWeb].[data].[MemberRoleLookup]
print '-- -- -- delete referral -- -- -- '
DELETE FROM [UrbaWeb].[data].[Referral]
print '-- -- -- delete survey answer -- -- -- '
DELETE FROM [UrbaWeb].[data].[SurveyAnswer] 
go


print ' '
print '-- -- -- start inserting -- -- --  -- -- start inserting -- --  -- -- start inserting -- -- '
print ' '


print '-- -- -- Insert CompanyCategoryType -- -- -- '
SET IDENTITY_INSERT [UrbaWeb].[app].[CompanyCategoryType] ON

INSERT INTO [UrbaWeb].[app].[CompanyCategoryType]
           ([companyCategoryTypeId]
		   ,[name]
           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive])
		SELECT [id]
			 ,[Name]
					,GETDATE()
					,GETDATE()
					,NULL
					,NULL
					,1
			FROM [Urba].[dbo].[ContractorCategories]

SET IDENTITY_INSERT [UrbaWeb].[app].[CompanyCategoryType] OFF
GO



print '-- -- -- Insert [QuestionType] -- -- -- '
SET IDENTITY_INSERT [UrbaWeb].[app].[QuestionType] ON

INSERT INTO [UrbaWeb].[app].[QuestionType]
           ([questionTypeId]
		   ,[questionText]
           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive])
Select	 [id]
		,[QuestionText]
					,GETDATE()
					,GETDATE()
					,NULL
					,NULL
					,1
					from [Urba].[dbo].[Questions]

SET IDENTITY_INSERT [UrbaWeb].[app].[QuestionType] OFF
GO





print '-- -- -- Insert [MemberRoleType] -- -- -- '
SET IDENTITY_INSERT [UrbaWeb].[app].[MemberRoleType] ON

INSERT INTO [UrbaWeb].[app].[MemberRoleType]
           ([memberRoleTypeId], [name], [description]
           ,[created]           ,[modified]           ,[createdBy]           ,[modifiedBy]           ,[isActive])
     VALUES   (1, 'Admin',
		    NULL	,GETDATE()	,GETDATE()	,NULL	,NULL	,1)

INSERT INTO [UrbaWeb].[app].[MemberRoleType]
           ([memberRoleTypeId], [name]    ,[description]
           ,[created]           ,[modified]           ,[createdBy]           ,[modifiedBy]           ,[isActive])
     VALUES   (2, 'User',
		    NULL	,GETDATE()	,GETDATE()	,NULL	,NULL	,1)

SET IDENTITY_INSERT [UrbaWeb].[app].[MemberRoleType] OFF
GO


/*///////////////////////// ContactInfo ///////////////////////////// */
print '--------------------- Insert ContactInfo --------------------------- '

SET IDENTITY_INSERT [UrbaWeb].[data].[ContactInfo] ON

INSERT INTO [UrbaWeb].[data].[ContactInfo]
           ([contactInfoId]
			,[Firstname]
			,[Lastname]
			,[Address]
			,[City]
			,[State]
			,[Zip]
			,[HomePhone]
			,[WorkPhone]
			,[Fax]
			,[Cell]
			,[Email]
			,[created]
			,[modified]
			,[createdBy]
			,[modifiedBy]
			,[isActive]
		)
			SELECT [id]
				  ,[Firstname]
				  ,[Lastname]
				  ,[Address]
				  ,[City]
				  ,[State]
				  ,[Zip]
				  ,[HomePhone]
				  ,[WorkPhone]
				  ,[Fax]
				  ,[Cell]
				  ,[Email]
					,GETDATE()
					,GETDATE()
					,NULL
					,NULL
					,1
			  FROM [Urba].[dbo].[ContactInfo]
			  

SET IDENTITY_INSERT [UrbaWeb].[data].[ContactInfo] OFF
GO

/*///////////////////////// company ///////////////////////////// */
print '--------------------- insert Company --------------------------- '

SET IDENTITY_INSERT [UrbaWeb].[data].[Company] ON


INSERT INTO [UrbaWeb].[data].[Company]
           ([companyId]
			  ,[companyName]
			  ,[contactInfoId]
			  ,[license]
			  ,[bonded]
			  ,[agreementSigned]
           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive]
		)
		SELECT [id]
			  ,[CompanyName]
			  ,[ContactInfoID]
			  ,[License]
			  ,[Bonded]
			  ,[AgreementSigned]
	  				,GETDATE()
					,GETDATE()
					,NULL
					,NULL
					,1
		  FROM  [Urba].[dbo].[Contractors]


SET IDENTITY_INSERT [UrbaWeb].[data].[Company] OFF
GO

/*///////////////////////// Member  (former Users) ///////////////////////////// */
print '--------------------- insert Member --------------------------- '

SET IDENTITY_INSERT [UrbaWeb].[data].[Member] ON

INSERT INTO [UrbaWeb].[data].[Member]
           ([memberId]
		   ,[contactInfoId]
           ,[username]
           ,[password]
           ,[forcePasswordReset]
           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive])
		SELECT [id]
			  ,[ContactInfoID]
			  ,[Login]
			  ,[Password]
			  ,[ForcePasswordReset]
			--  ,[IsAdmin]
  	  			,GETDATE()
				,GETDATE()
				,NULL
				,NULL
				,1
		  FROM [Urba].[dbo].[Users]

SET IDENTITY_INSERT [UrbaWeb].[data].[Member] OFF
GO

print '-------------------- Insert CLient  ------------------------------- '

SET IDENTITY_INSERT [UrbaWeb].[data].[Client] OFF

SET IDENTITY_INSERT [UrbaWeb].[data].[Client] ON


INSERT INTO [UrbaWeb].[data].[Client]
           ([clientId]
		   ,[contactInfoId]
           ,[hasPaidFee]
		   ,[feeAmount]
           ,[startingDate]
           ,[endDate]
           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive])
     
		SELECT [id]
			  ,[ContactInfoID]
			  ,case when ([HasPaidFee]='Y') then 1 else 0 end as [HasPaidFee]
			  ,CAST([FeeAmount] AS decimal(18, 2)) AS 'FeeAmount'
			  ,[StartingDate]
			  ,[EndDate]
				   ,GETDATE()
				   ,GETDATE()
				   ,NULL
				   ,NULL
				   ,1
			  FROM  [Urba].[dbo].[Members]

SET IDENTITY_INSERT [UrbaWeb].[data].[Client] OFF
GO

/*///////////////////////// referral ///////////////////////////// */
print '------------------- insert [Referral] -- ----------------------------------- -- '

SET IDENTITY_INSERT [UrbaWeb].[data].[Referral] ON

INSERT INTO [UrbaWeb].[data].[Referral]
           ([referralId]
  		   ,[clientId]
           ,[companyId]
           ,[companyCategoryTypeId]
           ,[referralDate]
           ,[quote]
           ,[accepted]
           ,[finalQuote]
           ,[finishDate]
           ,[description]
           ,[surveyComment]
           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive]
		)
	SELECT [id]
		  ,[MemberID]
		  ,[ContractorID]  
		  ,[ContractorCategoryID]
		  ,[ReferralDate]
		  ,CAST([Quote] AS decimal(18, 2)) AS 'Quote'
		  ,[Accepted]
		  ,CAST([FinalQuote] AS decimal(18, 2)) AS 'FinalQuote'
		  ,[FinishDate]
		  ,[Description]
		  ,[SurveyComment]
			,GETDATE()
			,GETDATE()
			,NULL
			,NULL
			,1
			  FROM [Urba].[dbo].[Referrals]
 

SET IDENTITY_INSERT [UrbaWeb].[data].[Referral] OFF
GO

/*/////////////////////////// survey answers /////////////////////////////////// */

print '--------------------- survey answers --------------------------- '

INSERT INTO [UrbaWeb].[data].[SurveyAnswer]
	
           ([surveyAnswerId]
		   ,[referralId]
           ,[questionTypeId]
           ,[answer]
           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive]
		)
	SELECT NEWID()
			,[ReferralID]
          ,[QuestionID]
          ,[Answer]
			,GETDATE()
			,GETDATE()
			,NULL
			,NULL
			,1
			  FROM [Urba].[dbo].[SurveyAnswers]
 
GO


print '--------------------- survey CompanyCategoryLookup --------------------------- '


INSERT INTO [UrbaWeb].[data].[CompanyCategoryLookup]
           ([companyCategoryId]
           ,[companyId]
           ,[companyCategoryTypeId]
           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive])
	SELECT NEWID()
		    ,[ContractorID]
		    ,[ContractorCategoryID]
			,GETDATE()
			,GETDATE()
			,NULL
			,NULL
			,1
	FROM [Urba].[dbo].[ContractorCategoryLookup]
GO




print '---------------------  [Organization] --------------------------- '

SET IDENTITY_INSERT [UrbaWeb].[data].[Organization] ON

INSERT INTO [UrbaWeb].[data].[Organization]
           ([organizationId]
		   ,[contactInfoId]
           ,[name]
           ,[feeAmount]
           ,[hasPaidFee]

           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive])
SELECT [id]
		-- note: in [Urba].[dbo].[MemberCategories] there is non existent FK value (39) which is an error and won't let this INSERT to succeed. Fixed by using 'CASE'
	  ,case when ([ParentMemberID]=39) then 33 else [ParentMemberID] end as [ParentMemberID]

      ,[Name]
      ,CAST([FeeAmount] AS decimal(18, 2)) AS 'FeeAmount'
	  ,case when ([HasPaidFee]='Y') then 1 else 0 end as [HasPaidFee]

      		,GETDATE()
			,GETDATE()
			,NULL
			,NULL
			,1
 FROM [Urba].[dbo].[MemberCategories]
 
 SET IDENTITY_INSERT [UrbaWeb].[data].[Organization] OFF

 
print '--------------------- survey [MemberRoleLookup] --------------------------- '

-- Tie user "admin" with role "admin"
INSERT INTO [UrbaWeb].[data].[MemberRoleLookup]
           ([memberRoleId]
           ,[memberRoleTypeId]
           ,[memberId]
           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive])
     VALUES
           (NEWID()
           ,1 -- memberRoleTypeId -- 1 - role:admin
           ,1 -- memberId
			,GETDATE()
			,GETDATE()
			,NULL
			,NULL
			,1)

-- Tie user "admin" with role "user"
INSERT INTO [UrbaWeb].[data].[MemberRoleLookup]
           ([memberRoleId]
           ,[memberRoleTypeId]
           ,[memberId]
           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive])
     VALUES
           (NEWID()
           ,2 -- memberRoleTypeId -- 2 - role:user
           ,1 -- memberId
			,GETDATE()
			,GETDATE()
			,NULL
			,NULL
			,1)
GO




/* New Table. Empty. Doesn't seem to be used since Organization already has ClientInfoId Foreign Key  in itself.

print '-- -- -- Insert ClientOrganizationLookup -- -- -- '
INSERT INTO [UrbaWeb].[data].[ClientOrganizationLookup]
           ([clientOrganizationLookupId]
           ,[clientId]
           ,[organizationId]

           ,[created]
           ,[modified]
           ,[createdBy]
           ,[modifiedBy]
           ,[isActive])
		VALUES( [id]
				  ,?
				  ,?
				  ,?
		
					,GETDATE()
					,GETDATE()
					,NULL
					,NULL
					,1)
GO
*/