
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[Users](
	[IdUser] [BigInt] IDENTITY(1,1) NOT NULL,
	[RegisterUser] [DateTime] NOT NULL,
	[NameUser] [Varchar](100) NOT NULL,
	[EmailUser] [Varchar](200) NOT NULL,
	[CreditUser] [Decimal](20, 2) NOT NULL,
	[StateUser] [Bit] NOT NULL,	
	[LastModify] [DateTime] NULL
	CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
	( [IdUser] ASC	)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)	ON [PRIMARY]
	) ON [PRIMARY]
END;
