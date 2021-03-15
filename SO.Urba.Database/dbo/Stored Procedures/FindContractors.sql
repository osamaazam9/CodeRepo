CREATE PROCEDURE [dbo].[FindContractors]
(
	@ContractorName varchar(50)
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SET ANSI_WARNINGS OFF;

	SELECT
		c.id,
		c.CompanyName,
		ci.Firstname, 
		ci.Lastname, 
		ci.Address, 
		ci.City, 
		ci.State, 
		ci.Zip, 
		ci.WorkPhone, 
		ci.Fax,  
		ci.Email,
		c.License,
		c.Bonded,
		c.AgreementSigned,
		CAST(SUM(sa.answer)/(COUNT(sa.answer)*5.0) * 100 AS DECIMAL(5,2)) AS [Score],
		COUNT(DISTINCT sa.ReferralID) AS [Score Count]
		
	FROM
		Contractors c
		INNER JOIN ContactInfo ci
			ON c.ContactInfoID = ci.id
		LEFT JOIN Referrals r
			ON r.ContractorID = c.id
		LEFT JOIN SurveyAnswers sa
			ON sa.ReferralID = r.id

	WHERE
		c.CompanyName LIKE '%' + @ContractorName + '%'
		OR ci.Firstname LIKE '%' + @ContractorName + '%'
		OR ci.Lastname LIKE '%' + @ContractorName + '%'

	GROUP BY 
		c.id,
		c.CompanyName,
		ci.Firstname, 
		ci.Lastname, 
		ci.Address, 
		ci.City, 
		ci.State, 
		ci.Zip, 
		ci.WorkPhone, 
		ci.Fax,  
		ci.Email,
		c.License,
		c.Bonded,
		c.AgreementSigned
END