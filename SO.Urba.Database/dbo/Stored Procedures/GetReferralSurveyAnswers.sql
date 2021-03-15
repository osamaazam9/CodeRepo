CREATE PROCEDURE [dbo].[GetReferralSurveyAnswers]
(
	@ReferralID int
)	  
AS
BEGIN
	SET NOCOUNT ON;

	-- the contractor info only
	SELECT
		q.id AS 'Question ID',
		sa.Answer

	FROM
		Referrals r
		INNER JOIN SurveyAnswers sa
			ON sa.ReferralID = r.id
		INNER JOIN Questions q
			ON q.id = sa.QuestionID

	WHERE
		r.id = @ReferralID
END