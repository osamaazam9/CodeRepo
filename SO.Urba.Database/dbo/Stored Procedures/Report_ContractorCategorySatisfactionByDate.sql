CREATE PROCEDURE [dbo].[Report_ContractorCategorySatisfactionByDate]
(
	@FromDate smalldatetime,
	@ToDate smalldatetime
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SET ANSI_WARNINGS OFF;

	SELECT
		'PERIOD:',
		CONVERT(VARCHAR(10), @FromDate, 101)
		+ ' thru '
		+ CONVERT(VARCHAR(10), @ToDate, 101)

	SELECT
		cc.Name [Provider Categry],
		COUNT(DISTINCT r.id) AS [Referrals],
		SUM(CASE sa.Answer WHEN 5 THEN 1 ELSE 0 END) AS [Very Good],
		SUM(CASE sa.Answer WHEN 4 THEN 1 ELSE 0 END) AS [Good],
		SUM(CASE sa.Answer WHEN 3 THEN 1 ELSE 0 END) AS [Average],
		SUM(CASE sa.Answer WHEN 2 THEN 1 ELSE 0 END) AS [Bad],
		SUM(CASE sa.Answer WHEN 1 THEN 1 ELSE 0 END) AS [Very Bad]
			
	FROM	
		Referrals r
		INNER JOIN SurveyAnswers sa
			ON sa.ReferralID = r.id
		INNER JOIN ContractorCategories cc
			ON cc.id = r.ContractorCategoryID

	WHERE
		r.ReferralDate BETWEEN @FromDate AND @ToDate

	GROUP BY
		cc.id,	
		cc.Name

END