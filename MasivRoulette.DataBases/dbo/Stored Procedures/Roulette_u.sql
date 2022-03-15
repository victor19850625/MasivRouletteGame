
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Roulette_u
-- =============================================

CREATE PROCEDURE [dbo].[Roulette_u]
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
