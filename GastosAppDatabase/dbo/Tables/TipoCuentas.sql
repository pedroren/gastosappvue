CREATE TABLE [dbo].[TipoCuentas] (
    [TipoCuentaId] INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]       NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([TipoCuentaId] ASC)
);

