
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure OpeningClose
-- =============================================

CREATE PROCEDURE [dbo].[OpeningClose]
	@IdOpening [BigInt],
	@NumberOpening [SmallInt],
	@ColorOpening [Varchar](1)
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @Now DateTime = GETDATE()
	UPDATE [Openings] 
		SET	
			DateFinishOpening  = @Now,
			NumberOpening  = @NumberOpening,
			ColorOpening  = @ColorOpening,
			EnableOpening  = 0
		FROM [Openings] WITH(NOLOCK)
		WHERE [IdOpening] = @IdOpening
		
	SELECT * FROM [Openings] WHERE [IdOpening] = @IdOpening
END;
