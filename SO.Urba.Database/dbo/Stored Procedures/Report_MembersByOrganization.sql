CREATE PROCEDURE [dbo].[Report_MembersByOrganization]
(
	@MemberCategoryID int
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SET ANSI_WARNINGS OFF;

	SELECT
		'ID:',
		CAST(@MemberCategoryID AS VARCHAR(10))
	UNION ALL
	SELECT
		'Organization: ',
		(SELECT [Name] FROM MemberCategories WHERE id = @MemberCategoryID)


	SELECT
		mci.Firstname AS [First Name],
		mci.Lastname AS [Last Name],
		mci.HomePhone AS [Phone],
		mci.Address AS [Address],
		mci.City AS [City],
		mci.State AS [State],
		m.HasPaidFee AS [Member Fee]
			
	FROM	
		MemberCategories mc
		INNER JOIN Members m
			ON m.CategoryID = mc.id
		INNER JOIN ContactInfo mci
			ON mci.id = m.ContactInfoID

	WHERE
		mc.id = @MemberCategoryID
END