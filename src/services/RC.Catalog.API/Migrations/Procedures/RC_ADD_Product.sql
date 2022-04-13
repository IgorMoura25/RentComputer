CREATE OR ALTER PROCEDURE [dbo].[RC_ADD_Product]
(
	@UniversalId UNIQUEIDENTIFIER,
	@Name VARCHAR(100),
	@Description VARCHAR(500) = NULL,
	@Value DECIMAL(12,2),
	@Quantity INT,
	@IsActive BIT,
	@CreatedAt DATETIME
)
AS
BEGIN
	INSERT INTO Products
	(
		ProductGuid,
		Name,
		Description,
		Value,
		Quantity,
		IsActive,
		CreatedAt
	)
	VALUES
	(
		@UniversalId,
		@Name,
		@Description,
		@Value,
		@Quantity,
		@IsActive,
		@CreatedAt
	)

	SELECT 
		CONVERT(BIGINT, SCOPE_IDENTITY()) 'Id',
		@UniversalId 'UniversalId',
		@Name 'Name',
		@Description 'Description',
		@Value 'Value',
		@Quantity 'Quantity',
		@IsActive 'IsActive',
		@CreatedAt 'CreatedAt'
	
END