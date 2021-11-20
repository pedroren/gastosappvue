CREATE TABLE [dbo].[Cuentas] (
    [CuentaId]     INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]       NVARCHAR (100)  NOT NULL,
    [MonedaId]     INT             NOT NULL,
    [TipoCuentaId] INT             NOT NULL,
    [Balance]      DECIMAL (18, 2) NOT NULL,
    [UsuarioId]    INT             NULL,
    [Activo]       BIT             NULL,
    PRIMARY KEY CLUSTERED ([CuentaId] ASC),
    CONSTRAINT [Cuenta_Moneda] FOREIGN KEY ([MonedaId]) REFERENCES [dbo].[Monedas] ([MonedaId]) ON DELETE CASCADE,
    CONSTRAINT [Cuenta_TipoCuenta] FOREIGN KEY ([TipoCuentaId]) REFERENCES [dbo].[TipoCuentas] ([TipoCuentaId]) ON DELETE CASCADE
);

