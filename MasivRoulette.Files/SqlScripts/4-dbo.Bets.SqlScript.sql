
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Bets]') AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[Bets](
	[IdBet] [BigInt] IDENTITY(1,1) NOT NULL,
	[RegisterBet] [DateTime] NOT NULL,
	[IdOpening] [BigInt] NOT NULL,
	[IdUser] [BigInt] NOT NULL,
	[NumberBet] [SmallInt] NULL,
	[ColorBet] [Varchar](1) NULL,
	[ValueBet] [DECIMAL](7, 2) NOT NULL
	CONSTRAINT [PK_bet] PRIMARY KEY CLUSTERED 
	( [IdBet] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[Bets]  WITH CHECK ADD  CONSTRAINT [FK_bets_openings] FOREIGN KEY([IdOpening])
	REFERENCES [dbo].[Openings] ([IdOpening])
	ALTER TABLE [dbo].[Bets] CHECK CONSTRAINT [FK_bets_openings]
	
	ALTER TABLE [dbo].[Bets]  WITH CHECK ADD  CONSTRAINT [FK_bets_users] FOREIGN KEY([IdUser])
	REFERENCES [dbo].[Users] ([IdUser])
	ALTER TABLE [dbo].[Bets] CHECK CONSTRAINT [FK_bets_users]
END;
