CREATE OR ALTER PROCEDURE [dbo].[RC_ADD_Customer]
(
	@UniversalId UNIQUEIDENTIFIER,
	@Name VARCHAR(100),
	@Email VARCHAR(100),
	@NationalId VARCHAR(11),
	@IsActive BIT,
	@CreatedAt DATETIME
)
AS
BEGIN
	INSERT INTO Customers
	(
		CustomerGuid,
		Name,
		Email,
		NationalId,
		IsActive,
		CreatedAt
	)
	VALUES
	(
		@UniversalId,
		@Name,
		@Email,
		@NationalId,
		@IsActive,
		@CreatedAt
	)

	SELECT 
		CONVERT(BIGINT, SCOPE_IDENTITY()) 'Id',
		@UniversalId 'UniversalId',
		@Name 'Name',
		@IsActive 'IsActive',
		@CreatedAt 'CreatedAt',
		@Email 'EmailAddress',
		@NationalId 'Number'
	
END