CREATE TABLE [gastosuser].[Presupuestos] (
    [PresupuestoId] INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]        NVARCHAR (101) NOT NULL,
    [FechaDesde]    DATETIME       NULL,
    [FechaHasta]    DATETIME       NULL,
    [UsuarioId]     INT            NOT NULL,
    CONSTRAINT [PK_Presupuestos] PRIMARY KEY CLUSTERED ([PresupuestoId] ASC)
);

