
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Opening_g
-- =============================================

CREATE PROCEDURE [dbo].[Opening_g]
	@IdOpening [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT *
		FROM [Openings] WITH(NOLOCK)
		WHERE [IdOpening] = @IdOpening
END;
