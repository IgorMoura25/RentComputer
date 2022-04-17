CREATE OR ALTER PROCEDURE [dbo].[RC_GET_CustomerByNationalId]
(
	@NationalId VARCHAR(11)
)
AS
BEGIN
	SELECT TOP 1
		Customers.CustomerId 'Id',
		Customers.CustomerGuid 'UniversalId',
		Customers.Name,
		Customers.IsActive,
		Customers.CreatedAt,
		Customers.Email 'EmailAddress',
		Customers.NationalId 'Number'
	FROM
		Customers
	WHERE
		Customers.NationalId = @NationalId
END

exec RC_GET_CustomerByNationalId @NationalId = '43972511850'