CREATE PROCEDURE [dbo].[GetMemberCategoryByID]
(
	@MemberCategoryID int
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		mc.id,
		mc.Name,
		mc.HasPaidFee, 
		mc.FeeAmount,
		(SELECT COUNT(*) FROM Members m WHERE m.CategoryID = mc.id)
			AS 'Member Count',
		mc.ParentMemberID

	FROM
		MemberCategories mc

	WHERE
		mc.id = @MemberCategoryID
END