CREATE TABLE [dbo].[Bets] (
    [IdBet]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [RegisterBet] DATETIME       NOT NULL,
    [IdOpening]   BIGINT         NOT NULL,
    [IdUser]      BIGINT         NOT NULL,
    [NumberBet]   SMALLINT       NULL,
    [ColorBet]    VARCHAR (1)    NULL,
    [ValueBet]    DECIMAL (6, 2) NOT NULL,
    CONSTRAINT [PK_bet] PRIMARY KEY CLUSTERED ([IdBet] ASC),
    CONSTRAINT [FK_bets_openings] FOREIGN KEY ([IdOpening]) REFERENCES [dbo].[Openings] ([IdOpening]),
    CONSTRAINT [FK_bets_users] FOREIGN KEY ([IdUser]) REFERENCES [dbo].[Users] ([IdUser])
);

