CREATE TABLE [gastosuser].[PresupuestoDets] (
    [PresupuestoDetId] INT             IDENTITY (1, 1) NOT NULL,
    [PresupuestoId]    INT             NOT NULL,
    [ConceptoId]       INT             NOT NULL,
    [Monto]            DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PresupuestoDets] PRIMARY KEY CLUSTERED ([PresupuestoDetId] ASC),
    CONSTRAINT [PresupuestoDet_Presupuesto] FOREIGN KEY ([PresupuestoId]) REFERENCES [gastosuser].[Presupuestos] ([PresupuestoId])
);

