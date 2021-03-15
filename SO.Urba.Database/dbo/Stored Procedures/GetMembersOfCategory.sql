CREATE PROCEDURE [dbo].[GetMembersOfCategory]
(
	@MemberCategoryID int
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		m.id,
		ci.Firstname, 
		ci.Lastname, 
		-- ci.Address, 
		ci.City, 
		-- ci.State, 
		ci.Zip
		/*
		ci.HomePhone, 
		ci.WorkPhone, 
		ci.Fax, 
		ci.Cell, 
		ci.Email,
		m.HasPaidFee, 
		m.FeeAmount, 
		m.StartingDate,	
		m.EndDate		
		*/

	FROM
		Members m
		INNER JOIN ContactInfo ci
			ON m.ContactInfoID = ci.id

	WHERE
		m.CategoryID = @MemberCategoryID
END