CREATE PROCEDURE [dbo].[GetContractorCategories]
	  
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		cc.id,
		cc.Name,
		COUNT(ccl.ContractorID) AS 'Contractor Count'
		
	FROM dbo.ContractorCategories cc
	LEFT JOIN dbo.ContractorCategoryLookup ccl
		ON ccl.ContractorCategoryID = cc.id

	GROUP BY 
		cc.id,
		cc.Name

	ORDER BY 
		cc.Name

END