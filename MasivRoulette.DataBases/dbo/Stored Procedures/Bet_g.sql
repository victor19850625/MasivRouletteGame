
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Bet_g
-- =============================================

CREATE PROCEDURE [dbo].[Bet_g]
	@IdBet [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT *
		FROM [Bets] WITH(NOLOCK)
		WHERE [IdBet] = @IdBet
END;
