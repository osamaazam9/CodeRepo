CREATE PROCEDURE [dbo].[AddTripleReferral] 
(
	@MemberID int,
	@ContractorCategoryID int,
	@ReferralDate smalldatetime = NULL,
	@Quote smallmoney = NULL,
	@Accepted varchar(1) = NULL,
	@FinalQuote smallmoney = NULL,
	@FinishDate smalldatetime = NULL,
	@Description text = NULL
)
AS
BEGIN

	SET NOCOUNT ON;
	SET ANSI_WARNINGS OFF  
	--we turn warnings off because we receive Null warnings for the LEFT JOIN Referrals
	--this happens when a contractor has no referrals

	DECLARE ContractorsCursor CURSOR FOR
	SELECT TOP 3
		c.id,
		COUNT(r.id)
	FROM dbo.ContractorCategoryLookup ccl
	INNER JOIN Contractors c
		ON c.id = ccl.ContractorID
	LEFT JOIN Referrals r
		ON r.ContractorID = c.id
	WHERE 
		ccl.ContractorCategoryID = @ContractorCategoryID		
	GROUP BY
		c.id
	ORDER BY
		COUNT(r.id)

	OPEN ContractorsCursor


	BEGIN TRANSACTION
	BEGIN TRY
		-- get 3 contractors with the least referrals
		DECLARE @aContractorID int
		DECLARE @aContractorReferralCount int

		-- loop through those lucky contractors to add a referral for each of them
		FETCH NEXT FROM ContractorsCursor
		INTO @aContractorID, @aContractorReferralCount
		WHILE @@FETCH_STATUS = 0
		BEGIN

			EXEC dbo.AddNewOrUpdateReferral
				0,  -- ReferralID is 0 because we are creating new ones
				@MemberID,
				@aContractorID,
				@ContractorCategoryID,
				@ReferralDate,
				@Quote,
				@Accepted,
				@FinalQuote,
				@FinishDate,
				@Description
				
				
			FETCH NEXT FROM ContractorsCursor
			INTO @aContractorID, @aContractorReferralCount
		END

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		PRINT ERROR_MESSAGE();
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;

	CLOSE ContractorsCursor
	DEALLOCATE ContractorsCursor
END