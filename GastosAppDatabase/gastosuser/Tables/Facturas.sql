CREATE TABLE [gastosuser].[Facturas] (
    [FacturaID]          INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]             NVARCHAR (100)  NULL,
    [DescripFrecuenteId] INT             NULL,
    [Frecuencia]         CHAR (1)        DEFAULT ('M') NOT NULL,
    [Dia]                SMALLINT        NOT NULL,
    [Mes]                SMALLINT        NULL,
    [ProxFecha]          DATETIME        NULL,
    [UltFecha]           DATETIME        NULL,
    [UltMonto]           DECIMAL (18, 2) NULL,
    [Activo]             BIT             DEFAULT ((1)) NOT NULL,
    [UsuarioId]          INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([FacturaID] ASC)
);

