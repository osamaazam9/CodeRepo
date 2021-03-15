CREATE PROCEDURE [dbo].[AddNewOrUpdateMember] 
(
	@ExistingMemberID int,
	@MemberCategoryID int,
	@MemberFirstname varchar(50) = '',
	@MemberLastname varchar(50) = '',
	@MemberAddress varchar(50) = '',
	@MemberCity varchar(50) = '',
	@MemberState varchar(3) = '',
	@MemberZip varchar(10) = '',
	@MemberHomePhone varchar(20) = '',
	@MemberWorkPhone varchar(20) = '',
	@MemberFax varchar(20) = '',
	@MemberCell varchar(20) = '',
	@MemberEmail varchar(50) = '',
	@MemberHasPaidFee varchar(1) = '',	
	@MemberFeeAmount smallmoney = 0,
	@MemberStartingDate smalldatetime = NULL,
	@MemberEndDate smalldatetime = NULL,
	@MemberNewCategoryName varchar(50) = NULL
)
AS
BEGIN

	BEGIN TRANSACTION

	BEGIN TRY

		-- because of foreign-key contstraints, we must make sure a ContactInfo entry already exists
		-- firstly, insert or update the ContactInfo entry
		DECLARE @ContactInfoID AS int
		SET @ContactInfoID = 0

		-- if MemberID passed is not 0, then we already have its ContactInfo entry
		IF @ExistingMemberID > 0
		BEGIN
			SET @ContactInfoID = (SELECT ContactInfoID FROM dbo.Members WHERE id = @ExistingMemberID)
		END

		EXECUTE [dbo].[AddNewOrUpdateContactInfo]
			@ContactInfoID,
			@MemberFirstname,
			@MemberLastname,
			@MemberAddress,
			@MemberCity,
			@MemberState,
			@MemberZip,
			@MemberHomePhone,
			@MemberWorkPhone,
			@MemberFax,
			@MemberCell,
			@MemberEmail

		IF @ContactInfoID <=0
		BEGIN
			SET  @ContactInfoID = @@IDENTITY
		END
		

		-- if MemberID is 0, then we must create a new Contractor entry before we update
		IF @ExistingMemberID <= 0
		BEGIN
			INSERT INTO dbo.Members(ContactInfoID, CategoryID)
			VALUES(@ContactInfoID, @MemberCategoryID)

			SET @ExistingMemberID = SCOPE_IDENTITY()	
		END

		-- create a new category if a name for it was supplied
		IF @MemberNewCategoryName IS NOT NULL
		BEGIN
			INSERT INTO dbo.MemberCategories([Name], ParentMemberID)
			VALUES(@MemberNewCategoryName, @ExistingMemberID)

			SET @MemberCategoryID = SCOPE_IDENTITY()
		END

		UPDATE dbo.Members
		SET
			CategoryID = @MemberCategoryID,
			ContactInfoID = @ContactInfoID,
			HasPaidFee = @MemberHasPaidFee,
			FeeAmount = @MemberFeeAmount,
			StartingDate = @MemberStartingDate,
			EndDate = @MemberEndDate
		
		WHERE
			id = @ExistingMemberID

		SELECT @ExistingMemberID AS 'id'

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		PRINT ERROR_MESSAGE();
	END CATCH


	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
	

END