-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure BetsWinners_g
-- =============================================

CREATE PROCEDURE [dbo].[BetsWinners_g]
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
