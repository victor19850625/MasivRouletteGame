-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure BetsIdOpening_g
-- =============================================

CREATE PROCEDURE [dbo].[BetsIdOpening_g]
    @IdOpening [BIGINT]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT * FROM [Bets] WITH(NOLOCK)
		WHERE IdOpening = @IdOpening
END;
