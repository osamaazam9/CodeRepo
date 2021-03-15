CREATE PROCEDURE [dbo].[DeleteContractorCategory] 
	@ContractorCategoryID int
	  
AS
BEGIN
	SET NOCOUNT ON;

	--we do not delete from the lookup; there shouldn't be anything there anyway
	--unassigned contractors are not allowed
	--DELETE FROM dbo.ContractorCategoryLookup WHERE ContractorCategoryID = @ContractorCategoryID
 	DELETE FROM dbo.ContractorCategories WHERE id = @ContractorCategoryID
END