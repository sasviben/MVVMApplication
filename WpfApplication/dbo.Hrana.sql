CREATE TABLE [dbo].[Hrana] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [vrsta_obroka]    NVARCHAR (30) NULL,
    [naziv_proizvoda] NVARCHAR (50) NOT NULL,
    [tezina]          FLOAT (53)    NOT NULL,
    [kalorije]        FLOAT (53)    NOT NULL,
	[bjelancevine]	  FLOAT (53)	NOT NULL,
	[ugljikohidrati]  FLOAT (53)	NOT NULL,
	[masti]	  FLOAT (53)	NOT NULL,
    [suma_kalorija]   FLOAT (53)    NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

