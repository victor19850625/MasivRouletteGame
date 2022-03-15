
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OpeningClose]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[OpeningClose] AS' 
END
GO

-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure OpeningClose
-- =============================================

ALTER PROCEDURE [dbo].[OpeningClose]
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
GO