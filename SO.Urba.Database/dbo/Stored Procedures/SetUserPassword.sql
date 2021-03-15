CREATE PROCEDURE [dbo].[SetUserPassword] 
	@UserID int,
	@NewPassword varchar(64),
	@ForcePasswordReset bit 
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Users
	SET	
		Password = @NewPassword,
		ForcePasswordReset = @ForcePasswordReset
	WHERE id = @UserID
END