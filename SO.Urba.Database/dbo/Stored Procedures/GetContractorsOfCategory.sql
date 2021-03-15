CREATE PROCEDURE [dbo].[GetContractorsOfCategory]
(
	@ContractorCategoryID int
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SET ANSI_WARNINGS OFF;

	IF @ContractorCategoryID = -1
	BEGIN
		SELECT
			c.id,
			c.CompanyName,
			ci.Firstname, 
			ci.Lastname,  
			ci.WorkPhone,
			c.License,
			c.Bonded,
			c.AgreementSigned,
			CAST(SUM(sa.answer)/(COUNT(sa.answer)*5.0) * 100 AS DECIMAL(5,2)) AS 'Score',
			COUNT(DISTINCT sa.ReferralID) AS [Score Count]
			
		FROM Contractors c
		INNER JOIN ContactInfo ci
			ON c.ContactInfoID = ci.id
			LEFT JOIN Referrals r
				ON r.ContractorID = c.id
			LEFT JOIN SurveyAnswers sa
				ON sa.ReferralID = r.id

		WHERE
			c.id NOT IN (SELECT ContractorID FROM ContractorCategoryLookup)

		GROUP BY 
			c.id,
			c.CompanyName,
			ci.Firstname, 
			ci.Lastname,  
			ci.WorkPhone,
			c.License,
			c.Bonded,
			c.AgreementSigned

	END
	ELSE
		BEGIN
		SELECT
			c.id,
			c.CompanyName,
			ci.Firstname, 
			ci.Lastname,  
			ci.WorkPhone,
			c.License,
			c.Bonded,
			c.AgreementSigned,
			CAST(SUM(sa.answer)/(COUNT(sa.answer)*5.0) * 100 AS DECIMAL(5,2)) AS 'Score',
			COUNT(DISTINCT sa.ReferralID) AS [Score Count]

		FROM
			Contractors c
			INNER JOIN ContactInfo ci
				ON c.ContactInfoID = ci.id
			LEFT JOIN ContractorCategoryLookup ccl
				ON ccl.ContractorID = c.id
			LEFT JOIN Referrals r
				ON r.ContractorID = c.id
			LEFT JOIN SurveyAnswers sa
				ON sa.ReferralID = r.id

		WHERE
			ccl.ContractorCategoryID = @ContractorCategoryID

		GROUP BY 
			c.id,
			c.CompanyName,
			ci.Firstname, 
			ci.Lastname,  
			ci.WorkPhone,
			c.License,
			c.Bonded,
			c.AgreementSigned

	END

END