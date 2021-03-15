CREATE PROCEDURE [dbo].[AnswerReferralSurvey]	  
	@ReferralID int,
	@QuestionAnswerPairs varchar(100),
	@SurveyComment varchar(max)

AS
BEGIN
	SET NOCOUNT ON;

/*
	Question-answer pairs must be passed as a list.
	For example '1=0,12=3,33=5,47=1,856=1,'
	This means these questions --> answers:
		1   --> 0 (no answer)
		12  --> 3 (average)
		33  --> 5 (very good)
		47  --> 1 (very bad)
		856 --> 1 (very bad)

	These are the available answers:
	0 = no answer
	1 = very bad
	2 = bad
	3 = average
	4 = good
	5 - very good
*/

	UPDATE Referrals SET SurveyComment = @SurveyComment
	WHERE id = @ReferralID

	DECLARE @ThisPair varchar(10)
	DECLARE @PairPos int, @SplitPos int
	DECLARE @QuestionID int, @Answer int


	SET @QuestionAnswerPairs = REPLACE(@QuestionAnswerPairs, ' ', '') + ','
	SET @PairPos = CHARINDEX(',', @QuestionAnswerPairs, 1)

	WHILE @PairPos > 0
	BEGIN
		SET @ThisPair = LEFT(@QuestionAnswerPairs, @PairPos - 1)
		SET @SplitPos = CHARINDEX('=', @ThisPair, 1)
		IF @SplitPos > 0
		BEGIN
			SET @QuestionID = CAST( LEFT(@ThisPair, @SplitPos - 1) AS int )
			SET @Answer = CAST( SUBSTRING(@ThisPair, @SplitPos + 1, 1) AS int )

			IF @Answer = 0 SET @Answer = NULL

			IF EXISTS(SELECT * FROM SurveyAnswers WHERE ReferralID = @ReferralID AND QuestionID = @QuestionID)
			BEGIN
				UPDATE SurveyAnswers
					SET Answer = @Answer
				WHERE ReferralID = @ReferralID AND QuestionID = @QuestionID
			END
			ELSE
			BEGIN
				INSERT INTO SurveyAnswers(ReferralID, QuestionID, Answer)
				SELECT @ReferralID, @QuestionID, @Answer
			END
		END
		SET @QuestionAnswerPairs = SUBSTRING(@QuestionAnswerPairs, @PairPos + 1, 100)
		SET @PairPos = CHARINDEX(',', @QuestionAnswerPairs, 1)
	END  --while

END