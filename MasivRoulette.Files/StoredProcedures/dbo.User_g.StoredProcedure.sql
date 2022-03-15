
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[User_g]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[User_g] AS' 
END
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure User_g
-- =============================================
ALTER PROCEDURE [dbo].[User_g]
	@IdUser [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT *
		FROM [Users] WITH(NOLOCK)
		WHERE [IdUser] = @IdUser
END;
GO
