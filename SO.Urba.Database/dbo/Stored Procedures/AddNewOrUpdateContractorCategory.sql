CREATE PROCEDURE [dbo].[AddNewOrUpdateContractorCategory]
	@ExistingContractorCategoryID int,
	@ContractorCategoryName varchar(20)
	  
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

	BEGIN TRY


		IF @ExistingContractorCategoryID <= 0
		BEGIN
			IF NOT EXISTS (SELECT id FROM ContractorCategories WHERE Name = @ContractorCategoryName)
			BEGIN
					INSERT INTO ContractorCategories([Name])
					VALUES( @ContractorCategoryName )
			END

			ELSE
			BEGIN
				PRINT 'Contractor category "' + @ContractorCategoryName + '" already exists'
				RETURN
			END
		END
		
		UPDATE ContractorCategories
		SET
			Name = @ContractorCategoryName
		WHERE
			id = @ExistingContractorCategoryID
	
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		PRINT ERROR_MESSAGE();
	END CATCH


	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
	
END