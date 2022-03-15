-- =============================================
-- Author:		Victor Bravo
-- Create date: 11-March-22 
-- Description:	Stored Procedure Roulettes_g_All
-- =============================================

CREATE PROCEDURE [dbo].[Roulettes_g_All]
	@RowsCount [INT] OUTPUT
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT * INTO #TempResult
		FROM [Roulettes] WITH(NOLOCK)
		
	SET @RowsCount = (SELECT COUNT(IdRoulette) FROM #TempResult)
	
	SELECT * FROM #TempResult

	DROP TABLE #TempResult

END;
