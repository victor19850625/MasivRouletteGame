
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure OpeningOpen
-- =============================================

CREATE PROCEDURE [dbo].[OpeningOpen]
	@IdOpening [BigInt] OUTPUT,
	@DateStartOpening [DateTime] OUTPUT,
	@IdRoulette [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @Now DateTime = GETDATE()
	INSERT INTO [Openings]	(
		[IdRoulette],
		[DateStartOpening],
		[DateFinishOpening],
		[NumberOpening],
		[ColorOpening],
		[EnableOpening] )
		VALUES (
		@IdRoulette,
		@Now,
		NULL,
		NULL,
		NULL,
		1 );
		
	SELECT @IdOpening = SCOPE_IDENTITY();
	SELECT @DateStartOpening = @Now;
END;
