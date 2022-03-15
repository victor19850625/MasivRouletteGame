
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Roulettes_g_All]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Roulettes_g_All] AS' 
END
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 11-March-22 
-- Description:	Stored Procedure Roulettes_g_All
-- =============================================

ALTER PROCEDURE [dbo].[Roulettes_g_All]
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
GO
