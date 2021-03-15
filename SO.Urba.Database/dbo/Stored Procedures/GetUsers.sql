CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		u.id,
		u.[Login],
		u.Password,
		ci.Firstname, 
		ci.Lastname, 
		ci.Email,
		u.IsAdmin

	FROM
		Users u
		INNER JOIN ContactInfo ci
			ON u.ContactInfoID = ci.id

END