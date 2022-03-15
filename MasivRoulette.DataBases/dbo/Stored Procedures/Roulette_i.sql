-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Roulette_i
-- =============================================

CREATE PROCEDURE [dbo].[Roulette_i]
	@IdRoulette [BIGINT] OUTPUT,
	@RegisterRoulette [DATETIME] OUTPUT,
	@TitleRoulette [varchar](100)
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @Now DATETIME = GETDATE()
	INSERT INTO [Roulettes]	(
		[RegisterRoulette],
		[TitleRoulette],
		[StateRoulette],
        [LastModify])
		VALUES (
		@Now,
		@TitleRoulette,
        1,
		NULL );
	SELECT @IdRoulette = SCOPE_IDENTITY();
	SELECT @RegisterRoulette = @Now;
END;
