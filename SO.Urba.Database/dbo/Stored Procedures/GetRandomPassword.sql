CREATE PROCEDURE [dbo].[GetRandomPassword] 	  
AS
BEGIN

	DECLARE @PasswordListCount AS INT
	SET @PasswordListCount = (SELECT COUNT(*) FROM PasswordList)

	SELECT 
		Word 
	FROM 
		PasswordList 
	WHERE 
		id = (
			SELECT CAST((RAND() * @PasswordListCount + 1 ) AS INT)
		)
END