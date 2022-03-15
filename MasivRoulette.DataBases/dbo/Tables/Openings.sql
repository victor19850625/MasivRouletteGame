CREATE TABLE [dbo].[Openings] (
    [IdOpening]         BIGINT      IDENTITY (1, 1) NOT NULL,
    [IdRoulette]        BIGINT      NOT NULL,
    [DateStartOpening]  DATETIME    NULL,
    [DateFinishOpening] DATETIME    NULL,
    [NumberOpening]     SMALLINT    NULL,
    [ColorOpening]      VARCHAR (1) NULL,
    [EnableOpening]     BIT         NOT NULL,
    CONSTRAINT [PK_opening] PRIMARY KEY CLUSTERED ([IdOpening] ASC),
    CONSTRAINT [FK_openings_roulettes] FOREIGN KEY ([IdRoulette]) REFERENCES [dbo].[Roulettes] ([IdRoulette])
);

