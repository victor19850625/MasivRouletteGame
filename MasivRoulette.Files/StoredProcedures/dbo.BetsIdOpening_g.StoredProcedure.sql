
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[BetsIdOpening_g]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BetsIdOpening_g] AS' 
END
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure BetsIdOpening_g
-- =============================================

ALTER PROCEDURE [dbo].[BetsIdOpening_g]
    @IdOpening [BIGINT]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT * FROM [Bets] WITH(NOLOCK)
		WHERE IdOpening = @IdOpening
END;
GO
