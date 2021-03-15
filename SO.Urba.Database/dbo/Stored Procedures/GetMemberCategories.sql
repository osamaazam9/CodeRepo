CREATE PROCEDURE [dbo].[GetMemberCategories]
	  
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		mc.id,
		mc.Name,
		mc.HasPaidFee,
		CAST(mc.FeeAmount AS NUMERIC(8,2)) AS [FeeAmount],
		(SELECT COUNT(*) FROM Members m WHERE m.CategoryID = mc.id)
			AS 'Member Count'
	FROM
		MemberCategories mc		
END