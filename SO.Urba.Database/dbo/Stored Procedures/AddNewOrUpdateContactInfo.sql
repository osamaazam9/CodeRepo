CREATE PROCEDURE [dbo].[AddNewOrUpdateContactInfo]
	@ExistingContactInfoID int,  -- pass 0 to create a new record
	@ContactFirstname varchar(50) = '',
	@ContactLastname varchar(50) = '',
	@ContactAddress varchar(50) = '',
	@ContactCity varchar(50) = '',
	@ContactState varchar(3) = '',
	@ContactZip varchar(10) = '',
	@ContactHomePhone varchar(20) = '',
	@ContactWorkPhone varchar(20) = '',
	@ContactFax varchar(20) = '',
	@ContactCell varchar(20) = '',
	@ContactEmail varchar(50) = ''

AS
BEGIN

	IF @ExistingContactInfoID <= 0
	BEGIN
		INSERT INTO dbo.ContactInfo(Firstname) 
		VALUES(NULL)  --must insert at least one NULL

		SET @ExistingContactInfoID = SCOPE_IDENTITY()
	END

	UPDATE ContactInfo
	SET
		Firstname = @ContactFirstname,
		Lastname = @ContactLastname,
		Address = @ContactAddress,
		City = @ContactCity,
		State = @ContactState,
		Zip = @ContactZip,
		HomePhone = @ContactHomePhone,
		WorkPhone = @ContactWorkPhone,
		Fax = @ContactFax,
		Cell = @ContactCell,
		Email = @ContactEmail

	WHERE
		id = @ExistingContactInfoID
	
END