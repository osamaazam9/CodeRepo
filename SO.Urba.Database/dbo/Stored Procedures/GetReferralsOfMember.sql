CREATE PROCEDURE [dbo].[GetReferralsOfMember]
(
	@MemberID int
)	  
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		r.id,
		c.id AS 'ContractorID',
		c.CompanyName AS 'ContractorCompanyName',
		cc.id AS 'ContractorCategoryID',
		cc.[Name] AS 'ContractorCategoryName',
		CONVERT(varchar(8), r.ReferralDate, 1) AS 'ReferralDate',
		CONVERT(numeric(8,2), r.Quote) AS 'Quote',
		r.Accepted,
		CONVERT(numeric(8,2), r.FinalQuote) AS 'FinalQuote',
		CONVERT(varchar(8), r.FinishDate, 1) AS 'FinishDate',
		r.Description

	FROM
		Members m
		INNER JOIN Referrals r
			ON r.MemberID = m.id
		INNER JOIN Contractors c
			ON c.id = r.ContractorID
		INNER JOIN ContractorCategories cc
			ON cc.id = r.ContractorCategoryID
		
	WHERE
		m.id = @MemberID
END