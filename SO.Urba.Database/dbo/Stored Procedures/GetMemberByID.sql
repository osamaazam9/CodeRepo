CREATE PROCEDURE [dbo].[GetMemberByID]
(
	@MemberID int
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		m.id,
		m.CategoryID,
		ci.Firstname, 
		ci.Lastname, 
		ci.Address, 
		ci.City, 
		ci.State, 
		ci.Zip,
		ci.HomePhone, 
		ci.WorkPhone, 
		ci.Fax, 
		ci.Cell, 
		ci.Email,
		m.HasPaidFee, 
		m.FeeAmount, 
		m.StartingDate,	
		m.EndDate

	FROM
		Members m
		INNER JOIN ContactInfo ci
			ON m.ContactInfoID = ci.id

	WHERE
		m.id = @MemberID
END