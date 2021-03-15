CREATE PROCEDURE [dbo].[GetQuestionsList]	  
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		q.id,
		q.QuestionText

	FROM
		Questions q

END