CREATE TABLE [dbo].[TipoTransacciones] (
    [TipoTransaccionId] INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]            NVARCHAR (60) NOT NULL,
    CONSTRAINT [PK_TipoTransacciones] PRIMARY KEY CLUSTERED ([TipoTransaccionId] ASC)
);

