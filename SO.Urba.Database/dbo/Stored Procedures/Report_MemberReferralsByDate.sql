CREATE PROCEDURE [dbo].[Report_MemberReferralsByDate]
(
	@MemberID int,
	@FromDate smalldatetime,
	@ToDate smalldatetime
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SET ANSI_WARNINGS OFF;

	DECLARE @MemberName AS VARCHAR(100)
	DECLARE @MemberHomeNumber AS VARCHAR(50)
	DECLARE @MemberAddress AS VARCHAR(50)
	DECLARE @MemberCellNumber AS VARCHAR(50)
	DECLARE @MemberCityStateZip AS VARCHAR(50)
	DECLARE @MemberEmail AS VARCHAR(50)
	DECLARE @MemberOrganization AS VARCHAR(50)
	DECLARE @MemberStartingDate AS VARCHAR(50)
	DECLARE @MemberEndDate AS VARCHAR(50)
	DECLARE @MemberDues AS VARCHAR(50)

	SELECT
		@MemberName = mci.Firstname + ' ' + mci.Lastname,
		@MemberHomeNumber = mci.HomePhone,
		@MemberAddress = mci.Address,
		@MemberCellNumber = mci.Cell,
		@MemberCityStateZip = mci.City + ', ' + mci.State + ' ' + mci.Zip,
		@MemberEmail = mci.Email,
		@MemberOrganization = mc.Name,
		@MemberStartingDate = m.StartingDate,
		@MemberEndDate = m.EndDate
	
	FROM
		Members m
		INNER JOIN ContactInfo mci
			ON mci.id = m.ContactInfoID
		INNER JOIN MemberCategories mc
			ON mc.id = m.CategoryID
		
	WHERE
		m.id = @MemberID

	SELECT @MemberName, 'Home:', @MemberHomeNumber
	UNION ALL
	SELECT @MemberAddress, 'Cell:', @MemberCellNumber
	UNION ALL
	SELECT @MemberCityStateZip, 'Email:', @MemberEmail

	SELECT 'Organization: ', @MemberOrganization
	UNION ALL
	SELECT 'Membership', @MemberStartingDate + ' thru' + @MemberEndDate
	UNION ALL
	SELECT 'Dues:', @MemberDues

	SELECT
		r.id AS [ID],
		c.CompanyName AS [Contractor],
		r.Description AS [Job],
		r.ReferralDate AS [Referral Date],
		CASE r.Accepted WHEN 'Y' THEN 'accepted' ELSE 'pending' END AS [Status],
		CAST(SUM(sa.answer)/(COUNT(sa.answer)*5.0) * 100 AS DECIMAL(5,2)) AS 'Score'

	FROM
		Members m
		INNER JOIN Referrals r
			ON r.MemberID = m.id
		INNER JOIN Contractors c
			ON c.id = r.ContractorID
		LEFT JOIN SurveyAnswers sa
			ON sa.ReferralID = r.id

	WHERE
		m.id = @MemberID
		AND r.ReferralDate BETWEEN @FromDate AND @ToDate

	GROUP BY 
		m.id,
		c.CompanyName,
		r.id,
		r.Description,
		r.ReferralDate,
		r.Accepted 
END