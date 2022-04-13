CREATE OR ALTER PROCEDURE [dbo].[RC_GET_ProductByName]
(
	@Name VARCHAR(100)
)
AS
BEGIN
	SELECT TOP 1
		Products.ProductId,
		Products.ProductGuid,
		Products.Name,
		Products.Description,
		Products.Value,
		Products.Quantity,
		Products.IsActive,
		Products.CreatedAt,
		JSON_QUERY((
			SELECT 
				ProductImages.Path AS Path
			FROM
				ProductImages
			WHERE
				ProductImages.ProductId = Products.ProductId
			FOR JSON PATH)) AS Images
	FROM
		Products
	WHERE
		Products.Name = @Name
END