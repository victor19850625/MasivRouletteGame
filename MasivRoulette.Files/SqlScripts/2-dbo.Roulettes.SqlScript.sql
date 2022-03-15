
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Roulettes]') AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[Roulettes](
	[IdRoulette] [BigInt] IDENTITY(1,1) NOT NULL,
	[RegisterRoulette] [DateTime] NOT NULL,
	[TitleRoulette] [Varchar](100) NOT NULL,
	[StateRoulette] [Bit] NOT NULL,	
	[LastModify] [DateTime] NULL
	CONSTRAINT [PK_roulette] PRIMARY KEY CLUSTERED 
	( [IdRoulette] ASC )
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END;
