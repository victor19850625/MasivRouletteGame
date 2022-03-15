
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Openings]') AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[Openings](
	[IdOpening] [BigInt] IDENTITY(1,1) NOT NULL,
	[IdRoulette] [BigInt] NOT NULL,	
	[DateStartOpening] [DateTime] NULL,
	[DateFinishOpening] [DateTime] NULL,
	[NumberOpening] [SmallInt] NULL,
	[ColorOpening] [Varchar](1) NULL,
	[EnableOpening] [Bit] NOT NULL
	CONSTRAINT [PK_opening] PRIMARY KEY CLUSTERED 
	( [IdOpening] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)	ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[Openings]  WITH CHECK ADD  CONSTRAINT [FK_openings_roulettes] FOREIGN KEY([IdRoulette])
	REFERENCES [dbo].[Roulettes] ([IdRoulette])
	ALTER TABLE [dbo].[Openings] CHECK CONSTRAINT [FK_openings_roulettes]
END;
