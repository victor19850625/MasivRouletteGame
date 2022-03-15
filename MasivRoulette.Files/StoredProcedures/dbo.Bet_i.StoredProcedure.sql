
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bet_i]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Bet_i] AS' 
END
GO

-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Bet_i
-- =============================================
ALTER PROCEDURE [dbo].[Bet_i]
	@IdBet [BigInt] OUTPUT,
	@RegisterBet [DATETIME] OUTPUT,
	@IdOpening [BigInt],
	@IdUser [BigInt],
	@NumberBet [SmallInt],
	@ColorBet [varchar](1),
	@ValueBet [DECIMAL](7, 2)
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @Now DATETIME = GETDATE()
	INSERT INTO [BetsRoulettes]	(
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
GO

	
