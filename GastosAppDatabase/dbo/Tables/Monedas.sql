CREATE TABLE [dbo].[Monedas] (
    [MonedaId]    INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]      NVARCHAR (100)  NOT NULL,
    [Abreviatura] NVARCHAR (5)    NULL,
    [EsPrincipal] BIT             NOT NULL,
    [Tasa]        DECIMAL (10, 4) NULL,
    [UsuarioId]   INT             NULL,
    PRIMARY KEY CLUSTERED ([MonedaId] ASC)
);

