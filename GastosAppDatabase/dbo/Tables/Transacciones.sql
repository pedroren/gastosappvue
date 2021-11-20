CREATE TABLE [dbo].[Transacciones] (
    [TransaccionId]     INT             IDENTITY (1, 1) NOT NULL,
    [Fecha]             DATETIME        NOT NULL,
    [Descripcion]       NVARCHAR (100)  NULL,
    [ConceptoId]        INT             NULL,
    [Monto]             DECIMAL (18, 2) NOT NULL,
    [CuentaId]          INT             NOT NULL,
    [CuentaIdTransf]    INT             NULL,
    [Comentario]        NVARCHAR (250)  NULL,
    [TipoTransaccionId] INT             NOT NULL,
    [TasaTransf]        DECIMAL (10, 4) DEFAULT ((0)) NOT NULL,
    [MontoTransf]       DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [UsuarioId]         INT             NULL,
    CONSTRAINT [PK__Transacc__86A849FE1273C1CD] PRIMARY KEY CLUSTERED ([TransaccionId] ASC),
    CONSTRAINT [Transaccion_Concepto] FOREIGN KEY ([ConceptoId]) REFERENCES [dbo].[Conceptos] ([ConceptoId]) ON DELETE CASCADE,
    CONSTRAINT [Transaccion_Cuenta] FOREIGN KEY ([CuentaId]) REFERENCES [dbo].[Cuentas] ([CuentaId]) ON DELETE CASCADE,
    CONSTRAINT [Transaccion_CuentaTransf] FOREIGN KEY ([CuentaIdTransf]) REFERENCES [dbo].[Cuentas] ([CuentaId]),
    CONSTRAINT [Transaccion_TipoTransaccion] FOREIGN KEY ([TipoTransaccionId]) REFERENCES [dbo].[TipoTransacciones] ([TipoTransaccionId])
);

