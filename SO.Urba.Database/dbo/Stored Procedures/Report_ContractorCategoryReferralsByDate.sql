CREATE PROCEDURE [dbo].[Report_ContractorCategoryReferralsByDate]
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
		COUNT(r.id) AS [Referrals],
		SUM(CASE WHEN Quote IS NOT NULL THEN 1 ELSE 0 END) AS [Quotes],
		SUM(CASE Accepted WHEN 'Y' THEN 1 ELSE 0 END) AS [Accepts],
		SUM(ISNULL(Quote, 0)) AS [Quotes Total],
		SUM(ISNULL(FinalQuote, 0)) AS [Final Quotes Total]
			
	FROM	
		ContractorCategories cc
		INNER JOIN Referrals r
			ON r.ContractorCategoryID = cc.id

	WHERE
		r.ReferralDate BETWEEN @FromDate AND @ToDate

	GROUP BY
		cc.id,	
		cc.Name
END