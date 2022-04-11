CREATE OR ALTER PROCEDURE [dbo].[RC_LST_Products]
(
	@Offset BIGINT,
	@Count BIGINT
)
AS
BEGIN
	SELECT
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
	ORDER BY
		Products.ProductGuid
	OFFSET 
		@Offset ROWS 
	FETCH NEXT 
		@Count ROWS ONLY
END