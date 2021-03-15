CREATE PROCEDURE [dbo].[AddNewOrUpdateReferral] 
(
	@ExistingReferralID int,  -- 0 or less denotes a new entry
	@MemberID int,
	@ContractorID int,
	@ContractorCategoryID int,
	@ReferralDate smalldatetime = NULL,
	@Quote smallmoney = NULL,
	@Accepted varchar(1) = NULL,
	@FinalQuote smallmoney = NULL,
	@FinishDate smalldatetime = NULL,
	@Description varchar(max) = NULL
)
AS
BEGIN


	BEGIN TRANSACTION

	BEGIN TRY

		-- if ReferralID is 0, then we must create a new Referral entry before we update
		IF @ExistingReferralID <= 0
		BEGIN
			INSERT INTO dbo.Referrals(MemberID, ContractorID, ContractorCategoryID)
			VALUES(@MemberID, @ContractorID, @ContractorCategoryID)

			SET @ExistingReferralID = SCOPE_IDENTITY()	
		END


		UPDATE dbo.Referrals
		SET
			ReferralDate = @ReferralDate,
			Quote = @Quote,
			Accepted = @Accepted,
			FinalQuote = @FinalQuote,
			FinishDate = @FinishDate,
			Description = @Description
		
		WHERE
			id = @ExistingReferralID

		SELECT @ExistingReferralID AS 'id'

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		PRINT ERROR_MESSAGE();
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;

END