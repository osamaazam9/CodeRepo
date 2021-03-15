CREATE PROCEDURE [dbo].[RenameMemberCategory] 
	@MemberCategoryID int,
	@NewMemberCategoryName varchar(20)
	  
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT id FROM MemberCategories WHERE Name = @NewMemberCategoryName)
	BEGIN
		UPDATE MemberCategories 
		SET Name = @NewMemberCategoryName
		WHERE id = @MemberCategoryID
	END

	ELSE
	BEGIN
		PRINT 'Member category "' + @NewMemberCategoryName + '" already exists'
	END
END