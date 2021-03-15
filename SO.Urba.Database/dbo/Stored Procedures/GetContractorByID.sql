CREATE PROCEDURE [dbo].[GetContractorByID]
(
	@ContractorID int
)	  
AS
BEGIN
	SET NOCOUNT ON;

	-- the contractor info only
	SELECT
		c.id,
		c.CompanyName,
		ci.Firstname, 
		ci.Lastname, 
		ci.Address, 
		ci.City, 
		ci.State, 
		ci.Zip, 
		ci.WorkPhone, 
		ci.Fax,  
		ci.Email,
		c.License,
		c.Bonded,
		c.AgreementSigned

	FROM
		Contractors c
		INNER JOIN ContactInfo ci
			ON c.ContactInfoID = ci.id

	WHERE
		c.id = @ContractorID


	-- the contractor's category assignments, if any
	SELECT
		cc.id,
		cc.Name,
		(SELECT COUNT(*) FROM dbo.ContractorCategoryLookup ccl WHERE ccl.ContractorCategoryID = cc.id)
			AS 'Contractor Count'
	FROM
		Contractors c
		INNER JOIN dbo.ContractorCategoryLookup ccl
			ON c.id = ccl.ContractorID
		INNER JOIN dbo.ContractorCategories cc
			ON cc.id = ccl.ContractorCategoryID
	WHERE
		c.id = @ContractorID
END