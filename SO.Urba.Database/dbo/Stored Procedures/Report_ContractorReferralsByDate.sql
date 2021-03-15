CREATE PROCEDURE [dbo].[Report_ContractorReferralsByDate]
(
	@ContractorID int,
	@FromDate smalldatetime,
	@ToDate smalldatetime
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SET ANSI_WARNINGS OFF;

	DECLARE @ContractorCompanyName AS VARCHAR(50)
	DECLARE @ContractorContact AS VARCHAR(50)
	DECLARE @ContractorAddress AS VARCHAR(50)
	DECLARE @ContractorWorkNumber AS VARCHAR(50)
	DECLARE @ContractorCellNumber AS VARCHAR(50)
	DECLARE @ContractorFaxNumber AS VARCHAR(50)
	DECLARE @ContractorEmail AS VARCHAR(50)
	DECLARE @ContractorAgreementSigned AS VARCHAR(50)
	DECLARE @ContractorLicenseNumber AS VARCHAR(50)

	SELECT
		@ContractorCompanyName = c.CompanyName,
		@ContractorContact = mci.Firstname + ' ' + mci.Lastname,
		@ContractorAddress = mci.Address,
		@ContractorWorkNumber = mci.WorkPhone,
		@ContractorCellNumber = mci.Cell,
		@ContractorFaxNumber = mci.Fax,
		@ContractorEmail = mci.Email,
		@ContractorAgreementSigned = c.AgreementSigned,
		@ContractorLicenseNumber = c.License
	
	FROM
		Contractors c
		INNER JOIN ContactInfo mci
			ON mci.id = c.ContactInfoID
		
	WHERE
		c.id = @ContractorID

	SELECT @ContractorCompanyName, 'Work:', @ContractorWorkNumber
	UNION ALL
	SELECT @ContractorContact, 'Cell:', @ContractorCellNumber
	UNION ALL
	SELECT @ContractorAddress, 'Fax:', @ContractorFaxNumber
	UNION ALL
	SELECT 'ID: ' + CAST(@ContractorID AS VARCHAR(50)), 'E-mail:', @ContractorEmail
	UNION ALL
	SELECT 'Contractor agreement signed', @ContractorAgreementSigned, NULL
	UNION ALL
	SELECT 'License Number:', @ContractorLicenseNumber, NULL

	SELECT
		r.id AS [ID],
		mci.Firstname + ' ' + mci.Lastname AS [Homeowner],
		r.Description AS [Job],
		r.ReferralDate AS [Referral Date],
		CASE r.Accepted WHEN 'Y' THEN 'accepted' ELSE 'pending' END AS [Status],
		CAST(SUM(sa.answer)/(COUNT(sa.answer)*5.0) * 100 AS DECIMAL(5,2)) AS 'Score'

	FROM
		Contractors c
		INNER JOIN Referrals r
			ON r.ContractorID = c.id
		INNER JOIN Members m
			ON m.id = r.MemberID
		INNER JOIN ContactInfo mci
			ON mci.id = m.ContactInfoID
		LEFT JOIN SurveyAnswers sa
			ON sa.ReferralID = r.id

	WHERE
		c.id = @ContractorID
		AND ReferralDate BETWEEN @FromDate AND @ToDate

	GROUP BY 
		c.id,
		r.id,
		mci.Firstname,
		mci.Lastname,
		r.Description,
		r.ReferralDate,
		r.Accepted 
END