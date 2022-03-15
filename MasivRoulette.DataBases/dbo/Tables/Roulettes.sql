CREATE TABLE [dbo].[Roulettes] (
    [IdRoulette]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [RegisterRoulette] DATETIME      NOT NULL,
    [TitleRoulette]    VARCHAR (100) NOT NULL,
    [StateRoulette]    BIT           NOT NULL,
    [LastModify]       DATETIME      NULL,
    CONSTRAINT [PK_roulette] PRIMARY KEY CLUSTERED ([IdRoulette] ASC)
);

