CREATE OR ALTER PROCEDURE [dbo].[RC_LST_Customers]
(
	@Offset BIGINT,
	@Count BIGINT
)
AS
BEGIN
	SELECT
		Customers.CustomerId,
		Customers.CustomerGuid,
		Customers.Name,
		Customers.IsActive,
		Customers.CreatedAt,
		Customers.Email 'EmailAddress',
		Customers.NationalId 'Number'
	FROM
		Customers
	ORDER BY
		Customers.CustomerGuid
	OFFSET 
		@Offset ROWS 
	FETCH NEXT 
		@Count ROWS ONLY
END