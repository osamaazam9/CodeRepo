CREATE PROCEDURE [dbo].[FindMembers]
(
	@MemberName varchar(50)
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT
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
		ci.Firstname LIKE '%' + @MemberName + '%'
		OR ci.Lastname LIKE '%' + @MemberName + '%'
END