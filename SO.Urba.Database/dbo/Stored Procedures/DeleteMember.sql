CREATE PROCEDURE [dbo].[DeleteMember] 
	@MemberID int
	  
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM ContactInfo WHERE id = (SELECT ContactInfoID FROM Members WHERE id = @MemberID )
	DELETE FROM Members WHERE id = @MemberID
END