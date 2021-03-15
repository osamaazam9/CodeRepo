CREATE PROCEDURE [dbo].[AuthenticateAndGetUser]
(
	@UserLogin varchar(50),
	@UserPassword varchar(50)
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		u.id,
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
		u.[Login], 
		u.Password,
		u.ForcePasswordReset,
		u.IsAdmin

	FROM
		Users u
		INNER JOIN ContactInfo ci
			ON u.ContactInfoID = ci.id

	WHERE
		u.[Login] = @UserLogin
		AND u.Password = @UserPassword
END