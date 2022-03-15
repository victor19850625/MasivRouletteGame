
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roulette_g]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Roulette_g] AS' 
END
GO

-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Roulette_g
-- =============================================

ALTER PROCEDURE [dbo].[Roulette_g]
	@IdRoulette [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT *
		FROM [Roulettes] WITH(NOLOCK)
		WHERE [IdRoulette] = @IdRoulette
END;
GO