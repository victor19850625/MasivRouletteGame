
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Roulette_i]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Roulette_i] AS' 
END
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Roulette_i
-- =============================================

ALTER PROCEDURE [dbo].[Roulette_i]
	@IdRoulette [BIGINT] OUTPUT,
	@RegisterRoulette [DATETIME] OUTPUT,
	@TitleRoulette [varchar](100)
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @Now DATETIME = GETDATE()
	INSERT INTO [Roulettes]	(
		[RegisterRoulette],
		[TitleRoulette]
		[StateRoulette],
        [LastModify])
		VALUES (
		@Now,
		@TitleRoulette
        1,
		NULL );
	SELECT @IdRoulette = SCOPE_IDENTITY();
	SELECT @RegisterRoulette = @Now;
END;
GO
