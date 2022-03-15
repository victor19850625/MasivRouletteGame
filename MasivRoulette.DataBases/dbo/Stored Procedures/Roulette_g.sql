
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Roulette_g
-- =============================================

CREATE PROCEDURE [dbo].[Roulette_g]
	@IdRoulette [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT *
		FROM [Roulettes] WITH(NOLOCK)
		WHERE [IdRoulette] = @IdRoulette
END;
