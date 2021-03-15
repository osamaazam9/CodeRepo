CREATE PROCEDURE [dbo].[DeleteMemberCategory] 
	@MemberCategoryID int
	  
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM MemberCategories WHERE id = @MemberCategoryID
END