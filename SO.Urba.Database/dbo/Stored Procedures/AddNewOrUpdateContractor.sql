CREATE PROCEDURE [dbo].[AddNewOrUpdateContractor] 
(
	@ExistingContractorID int,  -- 0 or less denotes a new entry
	@ContractorCategoryIDList varchar(30),
	@ContractorCompanyName varchar(50) = '',
	@ContractorFirstname varchar(50) = '',
	@ContractorLastname varchar(50) = '',
	@ContractorAddress varchar(50) = '',
	@ContractorCity varchar(50) = '',
	@ContractorState varchar(3) = '',
	@ContractorZip varchar(10) = '',
	@ContractorWorkPhone varchar(20) = '',
	@ContractorFax varchar(20) = '',
	@ContractorEmail varchar(50) = '',
	@ContractorLicense varchar(20) = '',
	@ContractorBonded varchar(1) = '',
	@ContractorAgreementSigned smalldatetime = NULL
)
AS
BEGIN


	BEGIN TRANSACTION

	BEGIN TRY

		-- because of foreign-key contstraints, we must make sure a ContactInfo entry already exists
		-- firstly, insert or update the ContactInfo entry
		DECLARE @ContactInfoID AS int
		SET @ContactInfoID = 0

		-- if ContractorID passed is not 0, then we already have its ContactInfo entry
		IF @ExistingContractorID > 0
		BEGIN
			SET @ContactInfoID = (SELECT ContactInfoID FROM dbo.Contractors WHERE id = @ExistingContractorID)
		END

		EXECUTE [dbo].[AddNewOrUpdateContactInfo]
			@ContactInfoID,
			@ContractorFirstname,
			@ContractorLastname,
			@ContractorAddress,
			@ContractorCity,
			@ContractorState,
			@ContractorZip,
			NULL,  -- HomePhone
			@ContractorWorkPhone,
			@ContractorFax,
			NULL,  -- Cell
			@ContractorEmail

		IF @ContactInfoID <=0
		BEGIN
			SET  @ContactInfoID = @@IDENTITY
		END

		
		-- if ContractorID is 0, then we must create a new Contractor entry before we update
		IF @ExistingContractorID <= 0
		BEGIN
			INSERT INTO dbo.Contractors(ContactInfoID, CompanyName)
			VALUES(@ContactInfoID, '')  

			SET @ExistingContractorID = SCOPE_IDENTITY()	
		END


		UPDATE dbo.Contractors
		SET
			CompanyName = @ContractorCompanyName,
			ContactInfoID = @ContactInfoID,
			License = @ContractorLicense,
			Bonded = @ContractorBonded,
			AgreementSigned = @ContractorAgreementSigned
		
		WHERE
			id = @ExistingContractorID

		EXEC [dbo].[AssignContractorToCategories]
			@ExistingContractorID,
			@ContractorCategoryIDList

		SELECT @ExistingContractorID AS 'id'

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		PRINT ERROR_MESSAGE();
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;

END