CREATE PROCEDURE [dbo].[AssignContractorToCategories] 
(
	@ContractorID int, 
	@ContractorCategoryIDList varchar(30)  -- pass one or more CategoryID's separated by commas
)
AS
BEGIN

	IF @ContractorID <= 0
	BEGIN
		PRINT 'Contractor ID is required.'
		RETURN
	END

	DELETE FROM dbo.ContractorCategoryLookup WHERE ContractorID = @ContractorID

	DECLARE @CategoryIDList varchar(30), @Pos int
	DECLARE @CategoryID int

	SET @CategoryIDList = REPLACE(@ContractorCategoryIDList, ' ', '') + ','
	SET @Pos = CHARINDEX(',', @CategoryIDList, 1)

	WHILE @Pos > 0
	BEGIN
		SET @CategoryID = CAST( LEFT(@CategoryIDList, @Pos - 1) AS int )
		IF @CategoryID > 0
		BEGIN
			INSERT INTO dbo.ContractorCategoryLookup
				(ContractorID, ContractorCategoryID)
				SELECT @ContractorID, @CategoryID
		END
		SET @CategoryIDList = SUBSTRING(@CategoryIDList, @Pos + 1, 30)
		SET @Pos = CHARINDEX(',', @CategoryIDList, 1)
	END  --while
END  --stored proc