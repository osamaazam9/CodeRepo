CREATE PROCEDURE [dbo].[AddNewOrUpdateMemberCategory]
	@ExistingMemberCategoryID int,
	@MemberCategoryName varchar(20),
	@MemberCategoryHasPaidFee varchar(1) = '',	
	@MemberCategoryFeeAmount smallmoney = 0,
	@MemberCategoryParentMemberID int 
	
	  
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

	BEGIN TRY

		IF @ExistingMemberCategoryID <= 0
		BEGIN
			IF NOT EXISTS (SELECT id FROM MemberCategories WHERE Name = @MemberCategoryName)
			BEGIN
					INSERT INTO MemberCategories([Name], ParentMemberID)
					VALUES( @MemberCategoryName, @MemberCategoryParentMemberID )

					SET @ExistingMemberCategoryID = SCOPE_IDENTITY()
			END

			ELSE
			BEGIN
				PRINT 'Member category "' + @MemberCategoryName + '" already exists'
				RETURN
			END
		END
		
		UPDATE MemberCategories
		SET
			Name = @MemberCategoryName,
			HasPaidFee = @MemberCategoryHasPaidFee,
			FeeAmount = @MemberCategoryFeeAmount,
			ParentMemberID = @MemberCategoryParentMemberID
		WHERE
			id = @ExistingMemberCategoryID
	
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		PRINT ERROR_MESSAGE();
	END CATCH


	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
	
END