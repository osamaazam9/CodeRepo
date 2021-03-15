CREATE PROCEDURE [dbo].[AddNewOrUpdateUser] 
(
	@ExistingUserID int,
	@UserLogin varchar(50),
	@UserPassword varchar(64),
	@UserFirstname varchar(50) = '',
	@UserLastname varchar(50) = '',
	@UserEmail varchar(50) = '',
	@UserIsAdmin bit = 0
)
AS
BEGIN

	BEGIN TRANSACTION

	BEGIN TRY

		-- because of foreign-key contstraints, we must make sure a ContactInfo entry already exists
		-- firstly, insert or update the ContactInfo entry
		DECLARE @ContactInfoID AS int
		SET @ContactInfoID = 0

		-- if UserID passed is not 0, then we already have its ContactInfo entry
		IF @ExistingUserID > 0
		BEGIN
			SET @ContactInfoID = (SELECT ContactInfoID FROM dbo.Users WHERE id = @ExistingUserID)
		END

		EXECUTE [dbo].[AddNewOrUpdateContactInfo]
			@ContactInfoID,
			@ContactFirstname = @UserFirstname,
			@ContactLastname = @UserLastname,
			@ContactEmail = @UserEmail

		IF @ContactInfoID <=0
		BEGIN
			SET  @ContactInfoID = @@IDENTITY
		END
		
		DECLARE @ActualExistingUserID AS int
		SET @ActualExistingUserID = @ExistingUserID

		-- if UserID is 0, then we must create a new Contractor entry before we update
		IF @ActualExistingUserID <= 0
		BEGIN
			INSERT INTO dbo.Users(ContactInfoID)
			VALUES(@ContactInfoID)

			SET @ActualExistingUserID = SCOPE_IDENTITY()	
		END

		UPDATE dbo.Users
		SET
			[Login] = @UserLogin,
			Password = 
				(
					CASE @ExistingUserID 
						WHEN 0 THEN @UserPassword  --update the password if new entry only
						ELSE Password
						END
				),
			ForcePasswordReset =
				(
					CASE @ExistingUserID 
						WHEN 0 THEN 1  --force password reset if new entry only
						ELSE 0
						END
				),
			IsAdmin = @UserIsAdmin
		WHERE
			id = @ActualExistingUserID

		SELECT @ExistingUserID AS 'id'

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		PRINT ERROR_MESSAGE();
	END CATCH


	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
	

END