
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Bet_i
-- =============================================
CREATE PROCEDURE [dbo].[Bet_i]
	@IdBet [BigInt] OUTPUT,
	@RegisterBet [DATETIME] OUTPUT,
	@IdOpening [BigInt],
	@IdUser [BigInt],
	@NumberBet [SmallInt] = NULL,
	@ColorBet [varchar](1) = NULL,
	@ValueBet [DECIMAL](6, 2)
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @Now DATETIME = GETDATE()
	INSERT INTO [Bets]	(
		IdOpening,
		IdUser,
		RegisterBet,
		NumberBet,
		ColorBet,
		ValueBet)
		VALUES (
		@IdOpening,
		@IdUser,
		@Now,
		@NumberBet,
		@ColorBet,
		@ValueBet );
		
	SELECT @IdBet = SCOPE_IDENTITY();
	SELECT @RegisterBet = @Now;
END;
