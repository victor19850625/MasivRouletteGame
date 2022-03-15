
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[BetsWinners_g]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BetsWinners_g] AS' 
END
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure BetsWinners_g
-- =============================================

ALTER PROCEDURE [dbo].[BetsWinners_g]
    @IdOpening [BIGINT],
	@NumberBet [SMALLINT] = NULL,	
	@ColorBet [VARCHAR](1) = NULL
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT * FROM [Bets] WITH(NOLOCK)
		WHERE IdOpening = @IdOpening
		AND (NumberBet = @NumberBet
		OR ColorBet = ColorBet)
	


END;
GO
