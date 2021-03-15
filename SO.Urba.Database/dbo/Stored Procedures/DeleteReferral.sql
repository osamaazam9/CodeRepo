CREATE PROCEDURE [dbo].[DeleteReferral] 
	@ReferralID int
	  
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM SurveyAnswers WHERE ReferralID = @ReferralID
	DELETE FROM Referrals WHERE id = @ReferralID
END