CREATE TABLE [dbo].[Tanfolyam] (
    [TanfolyamId]  INT             IDENTITY (1, 1) NOT NULL,
    [Nev]          NVARCHAR (50)   NOT NULL,
    [KezdetDatuma] DATETIME        NOT NULL,
    [VegzesDatuma] DATETIME        NULL,
    [KoltsegPerFo] DECIMAL (10, 2) NOT NULL,
    [Aktiv]        BIT             DEFAULT ((0)) NULL,
    [TanarID]      INT             NOT NULL,
    [Koltseg]      INT             NULL,
    PRIMARY KEY CLUSTERED ([TanfolyamId] ASC),
    FOREIGN KEY ([TanarID]) REFERENCES [dbo].[Tanar] ([TanarId])
);

