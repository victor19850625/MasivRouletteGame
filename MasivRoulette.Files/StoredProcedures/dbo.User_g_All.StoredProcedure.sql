
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[Users_g_All]') AND type in (N'P', N'PC'))
BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Users_g_All] AS' 
END
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Users_g_All
-- =============================================

ALTER PROCEDURE [dbo].[Users_g_All]
	@IdUserLow [BIGINT] = NULL,
	@IdUserHigh [BIGINT] = NULL,
	@RegisterUserFrom [DATETIME] = NULL,
	@RegisterUserUntil [DATETIME] = NULL,
	@NameUser [VARCHAR] = NULL,	
	@EmailUser [VARCHAR] = NULL,
	@CreditUserLow DECIMAL(20,2) = NULL,
	@CreditUserHigh DECIMAL(20,2) = NULL,
    @StateUser [BIT] = NULL,
	@RowsCount [INT] OUTPUT
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT * INTO #TempUsers FROM Users
	DECLARE @IdLow BIGINT = ISNULL(@IdUserLow, (SELECT MIN(IdUser) FROM #TempUsers))
	DECLARE @IdHigh BIGINT = ISNULL(@IdUserHigh, (SELECT MAX(IdUser) FROM #TempUsers))
	DECLARE @RegisterFrom DATETIME = ISNULL(@RegisterUserFrom, (SELECT MIN(RegisterUser) FROM #TempUsers))
	DECLARE @RegisterUntil DATETIME = ISNULL(@RegisterUserUntil, (SELECT MAX(RegisterUser) FROM #TempUsers))
	DECLARE @Name VARCHAR(100) = ISNULL(@NameUser, '')
	DECLARE @Email VARCHAR(200) = ISNULL(@EmailUser, '')
	DECLARE @CreditLow DECIMAL(20,2) = ISNULL(@CreditUserLow, (SELECT MIN(CreditUser) FROM #TempUsers))
	DECLARE @CreditHigh DECIMAL(20,2) = ISNULL(@CreditUserHigh, (SELECT MAX(CreditUser) FROM #TempUsers))
	DECLARE @State1 BIT, @State2 BIT
    
	IF (@StateUser IS NULL)
	BEGIN
		SET @State1 = 0;
		SET @State2 = 1;
	END
	ELSE IF (@StateUser = 1)
	BEGIN
		SET @State1 = 1;
		SET @State2 = 1;
	END
	ELSE 
	BEGIN
		SET @State1 = 0;
		SET @State2 = 0;
	END
	SELECT * INTO #TempResult
		FROM [Users] WITH(NOLOCK)
		WHERE IdUser BETWEEN  @IdLow AND @IdHigh
		AND (RegisterUser BETWEEN @RegisterFrom AND @RegisterUntil)
		AND NameUser LIKE '%@Name%'
		AND EmailUser = @Email
		AND (CreditUser BETWEEN @CreditLow AND @CreditHigh)
		AND StateUser IN (@State1, @State2)
	
	SET @RowsCount = (SELECT COUNT(IdUser) FROM #TempResult)
	
	SELECT * FROM TempResult

	DROP TABLE #TempUsers
	DROP TABLE #TempResult

END;
GO
