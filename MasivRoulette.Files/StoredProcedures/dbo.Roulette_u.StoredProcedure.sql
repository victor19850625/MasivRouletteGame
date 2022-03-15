
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roulette_u]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Roulette_u] AS' 
END
GO

-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Roulette_u
-- =============================================

ALTER PROCEDURE [dbo].[Roulette_u]
	@IdRoulette [BigInt],
	@TitleRoulette [varchar](100) = NULL,
	@OpenRoulette [bit] = NULL,
	@StateRoulette [bit] = NULL
AS
BEGIN	
	SET NOCOUNT ON;
	UPDATE [Roulettes] 
		SET	
			TitleRoulette  = CASE
                              WHEN @TitleRoulette IS NULL
                              THEN [TitleRoulette]
                              ELSE @TitleRoulette
                          END,
			StateRoulette  = CASE
                              WHEN @StateRoulette IS NULL
                              THEN [StateRoulette]
                              ELSE @StateRoulette
                          END,
			LastModify = GETDATE()
			
		FROM [Roulettes] WITH(NOLOCK)
		WHERE [IdRoulette] = @IdRoulette
		
	SELECT * FROM [Roulettes] WHERE [IdRoulette] = @IdRoulette
END;
GO