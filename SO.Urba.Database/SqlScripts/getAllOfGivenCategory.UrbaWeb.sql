USE [UrbaWeb]

		SELECT
			c.companyId,
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
			[data].Company c
			INNER JOIN [data].ContactInfo ci
				ON c.contactInfoId = ci.contactInfoId
			LEFT JOIN [data].CompanyCategoryLookup ccl
				ON ccl.companyId = c.companyId
			LEFT JOIN [data].Referral r
				ON r.companyId = c.companyId
			LEFT JOIN [data].SurveyAnswer sa
				ON sa.referralId = r.referralId

		WHERE
			ccl.companyCategoryTypeId = 1

		GROUP BY 
			c.companyId,
			c.CompanyName,
			ci.Firstname, 
			ci.Lastname,  
			ci.WorkPhone,
			c.License,
			c.Bonded,
			c.AgreementSigned
