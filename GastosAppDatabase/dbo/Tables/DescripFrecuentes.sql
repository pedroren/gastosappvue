CREATE TABLE [dbo].[DescripFrecuentes] (
    [DescripFrecuenteId] INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]             NVARCHAR (100)  NOT NULL,
    [ConceptoId]         INT             NULL,
    [CuentaId]           INT             NULL,
    [CuentaIdTransf]     INT             NULL,
    [Monto]              DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [UsuarioId]          INT             NULL,
    [Activo]             BIT             DEFAULT ((1)) NULL,
    CONSTRAINT [PK_DescripFrecuentes] PRIMARY KEY CLUSTERED ([DescripFrecuenteId] ASC),
    CONSTRAINT [DescripFrecuente_Concepto] FOREIGN KEY ([ConceptoId]) REFERENCES [dbo].[Conceptos] ([ConceptoId]) ON DELETE CASCADE,
    CONSTRAINT [DescripFrecuente_Cuenta] FOREIGN KEY ([CuentaId]) REFERENCES [dbo].[Cuentas] ([CuentaId]) ON DELETE CASCADE,
    CONSTRAINT [DescripFrecuente_CuentaTransf] FOREIGN KEY ([CuentaIdTransf]) REFERENCES [dbo].[Cuentas] ([CuentaId])
);

