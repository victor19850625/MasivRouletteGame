USE [Master]
CREATE DATABASE [MasivRouletteGame]

USE [MasivRouletteGame]
GO
/****** Object:  Table [dbo].[Bets]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bets](
	[IdBet] [bigint] IDENTITY(1,1) NOT NULL,
	[RegisterBet] [datetime] NOT NULL,
	[IdOpening] [bigint] NOT NULL,
	[IdUser] [bigint] NOT NULL,
	[NumberBet] [smallint] NULL,
	[ColorBet] [varchar](1) NULL,
	[ValueBet] [decimal](6, 2) NOT NULL,
 CONSTRAINT [PK_bet] PRIMARY KEY CLUSTERED 
(
	[IdBet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Openings]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Openings](
	[IdOpening] [bigint] IDENTITY(1,1) NOT NULL,
	[IdRoulette] [bigint] NOT NULL,
	[DateStartOpening] [datetime] NULL,
	[DateFinishOpening] [datetime] NULL,
	[NumberOpening] [smallint] NULL,
	[ColorOpening] [varchar](1) NULL,
	[EnableOpening] [bit] NOT NULL,
 CONSTRAINT [PK_opening] PRIMARY KEY CLUSTERED 
(
	[IdOpening] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roulettes]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roulettes](
	[IdRoulette] [bigint] IDENTITY(1,1) NOT NULL,
	[RegisterRoulette] [datetime] NOT NULL,
	[TitleRoulette] [varchar](100) NOT NULL,
	[StateRoulette] [bit] NOT NULL,
	[LastModify] [datetime] NULL,
 CONSTRAINT [PK_roulette] PRIMARY KEY CLUSTERED 
(
	[IdRoulette] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[IdUser] [bigint] IDENTITY(1,1) NOT NULL,
	[RegisterUser] [datetime] NOT NULL,
	[NameUser] [varchar](100) NOT NULL,
	[EmailUser] [varchar](200) NOT NULL,
	[CreditUser] [decimal](12, 2) NOT NULL,
	[StateUser] [bit] NOT NULL,
	[LastModify] [datetime] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bets]  WITH CHECK ADD  CONSTRAINT [FK_bets_openings] FOREIGN KEY([IdOpening])
REFERENCES [dbo].[Openings] ([IdOpening])
GO
ALTER TABLE [dbo].[Bets] CHECK CONSTRAINT [FK_bets_openings]
GO
ALTER TABLE [dbo].[Bets]  WITH CHECK ADD  CONSTRAINT [FK_bets_users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[Bets] CHECK CONSTRAINT [FK_bets_users]
GO
ALTER TABLE [dbo].[Openings]  WITH CHECK ADD  CONSTRAINT [FK_openings_roulettes] FOREIGN KEY([IdRoulette])
REFERENCES [dbo].[Roulettes] ([IdRoulette])
GO
ALTER TABLE [dbo].[Openings] CHECK CONSTRAINT [FK_openings_roulettes]
GO
/****** Object:  StoredProcedure [dbo].[Bet_g]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Bet_g
-- =============================================

CREATE PROCEDURE [dbo].[Bet_g]
	@IdBet [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT *
		FROM [Bets] WITH(NOLOCK)
		WHERE [IdBet] = @IdBet
END;
GO
/****** Object:  StoredProcedure [dbo].[Bet_i]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
GO
/****** Object:  StoredProcedure [dbo].[BetsIdOpening_g]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure BetsIdOpening_g
-- =============================================

CREATE PROCEDURE [dbo].[BetsIdOpening_g]
    @IdOpening [BIGINT]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT * FROM [Bets] WITH(NOLOCK)
		WHERE IdOpening = @IdOpening
END;
GO
/****** Object:  StoredProcedure [dbo].[BetsWinners_g]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure BetsWinners_g
-- =============================================

CREATE PROCEDURE [dbo].[BetsWinners_g]
    @IdOpening [BIGINT],
	@NumberBet [SMALLINT] = NULL,	
	@ColorBet [VARCHAR](1) = NULL
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT * FROM [Bets] WITH(NOLOCK)
		WHERE IdOpening = @IdOpening
		AND (NumberBet = @NumberBet
		OR ColorBet = ColorBet)
	


END;
GO
/****** Object:  StoredProcedure [dbo].[Opening_g]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Opening_g
-- =============================================

CREATE PROCEDURE [dbo].[Opening_g]
	@IdOpening [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT *
		FROM [Openings] WITH(NOLOCK)
		WHERE [IdOpening] = @IdOpening
END;
GO
/****** Object:  StoredProcedure [dbo].[OpeningClose]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure OpeningClose
-- =============================================

CREATE PROCEDURE [dbo].[OpeningClose]
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
/****** Object:  StoredProcedure [dbo].[OpeningOpen]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
GO
/****** Object:  StoredProcedure [dbo].[Roulette_g]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Roulette_g
-- =============================================

CREATE PROCEDURE [dbo].[Roulette_g]
	@IdRoulette [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT *
		FROM [Roulettes] WITH(NOLOCK)
		WHERE [IdRoulette] = @IdRoulette
END;
GO
/****** Object:  StoredProcedure [dbo].[Roulette_i]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[Roulette_u]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Roulette_u
-- =============================================

CREATE PROCEDURE [dbo].[Roulette_u]
	@IdRoulette [BigInt],
	@TitleRoulette [varchar](100) = NULL,
	@OpenRoulette [bit] = NULL,
	@StateRoulette [bit] = NULL
AS
BEGIN	
	SET NOCOUNT ON;
	UPDATE [Roulettes] 
		SET	
			TitleRoulette  = CASE
                              WHEN @TitleRoulette IS NULL
                              THEN [TitleRoulette]
                              ELSE @TitleRoulette
                          END,
			StateRoulette  = CASE
                              WHEN @StateRoulette IS NULL
                              THEN [StateRoulette]
                              ELSE @StateRoulette
                          END,
			LastModify = GETDATE()
			
		FROM [Roulettes] WITH(NOLOCK)
		WHERE [IdRoulette] = @IdRoulette
		
	SELECT * FROM [Roulettes] WHERE [IdRoulette] = @IdRoulette
END;
GO
/****** Object:  StoredProcedure [dbo].[Roulettes_g_All]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 11-March-22 
-- Description:	Stored Procedure Roulettes_g_All
-- =============================================

CREATE PROCEDURE [dbo].[Roulettes_g_All]
	@RowsCount [INT] OUTPUT
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT * INTO #TempResult
		FROM [Roulettes] WITH(NOLOCK)
		
	SET @RowsCount = (SELECT COUNT(IdRoulette) FROM #TempResult)
	
	SELECT * FROM #TempResult

	DROP TABLE #TempResult

END;
GO
/****** Object:  StoredProcedure [dbo].[User_g]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure User_g
-- =============================================
CREATE PROCEDURE [dbo].[User_g]
	@IdUser [BigInt]
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT *
		FROM [Users] WITH(NOLOCK)
		WHERE [IdUser] = @IdUser
END;
GO
/****** Object:  StoredProcedure [dbo].[User_i]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure User_i
-- =============================================

CREATE PROCEDURE [dbo].[User_i]
	@IdUser [BIGINT] OUTPUT,
	@RegisterUser [DATETIME] OUTPUT,
	@NameUser [VARCHAR](100),
	@EmailUser [VARCHAR](200),
	@CreditUser [DECIMAL](12, 2)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Now DATETIME = GETDATE()
	INSERT INTO [Users]	(
		[RegisterUser],
		[NameUser],
		[EmailUser],
		[CreditUser],
		[StateUser],
        [LastModify] )
		VALUES (
		@Now,
		@NameUser,
		@EmailUser,
		@CreditUser,
		1,
		NULL  );
	SELECT @IdUser = SCOPE_IDENTITY();
	SELECT @RegisterUser = @Now;
END;
GO
/****** Object:  StoredProcedure [dbo].[User_u]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure User_u
-- =============================================
CREATE PROCEDURE [dbo].[User_u]
	@IdUser [BigInt],
	@NameUser [varchar](100) = NULL,
	@EmailUser [varchar](200) = NULL,
	@CreditUser [DECIMAL](12, 2) = NULL,
	@StateUser [bit] = NULL
AS
BEGIN	
	SET NOCOUNT ON;
	UPDATE [Users] 
		SET	
			NameUser  = CASE
                              WHEN @NameUser IS NULL
                              THEN [NameUser]
                              ELSE @NameUser
                          END,
			EmailUser  = CASE
                              WHEN @EmailUser IS NULL
                              THEN [EmailUser]
                              ELSE @EmailUser
                          END,
			CreditUser  = CASE
                              WHEN @CreditUser IS NULL
                              THEN [CreditUser]
                              ELSE @CreditUser
                          END,
			StateUser  = CASE
                              WHEN @StateUser IS NULL
                              THEN [StateUser]
                              ELSE @StateUser
                          END,
			LastModify = GETDATE()
			
		FROM [Users] WITH(NOLOCK)
		WHERE [IdUser] = @IdUser

		SELECT * FROM Users WHERE [IdUser] = @IdUser
END;
GO
/****** Object:  StoredProcedure [dbo].[Users_g_All]    Script Date: 14/03/2022 21:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Victor Bravo
-- Create date: 8-March-22 
-- Description:	Stored Procedure Users_g_All
-- =============================================

CREATE PROCEDURE [dbo].[Users_g_All]
	@IdUserLow [BIGINT] = NULL,
	@IdUserHigh [BIGINT] = NULL,
	@RegisterUserFrom [DATETIME] = NULL,
	@RegisterUserUntil [DATETIME] = NULL,
	@NameUser [VARCHAR] = NULL,	
	@EmailUser [VARCHAR] = NULL,
	@CreditUserLow DECIMAL(12,2) = NULL,
	@CreditUserHigh DECIMAL(12,2) = NULL,
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
	DECLARE @CreditLow DECIMAL(12,2) = ISNULL(@CreditUserLow, (SELECT MIN(CreditUser) FROM #TempUsers))
	DECLARE @CreditHigh DECIMAL(12,2) = ISNULL(@CreditUserHigh, (SELECT MAX(CreditUser) FROM #TempUsers))
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
