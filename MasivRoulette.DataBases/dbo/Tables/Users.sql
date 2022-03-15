CREATE TABLE [dbo].[Users] (
    [IdUser]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [RegisterUser] DATETIME        NOT NULL,
    [NameUser]     VARCHAR (100)   NOT NULL,
    [EmailUser]    VARCHAR (200)   NOT NULL,
    [CreditUser]   DECIMAL (12, 2) NOT NULL,
    [StateUser]    BIT             NOT NULL,
    [LastModify]   DATETIME        NULL,
    CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([IdUser] ASC)
);

