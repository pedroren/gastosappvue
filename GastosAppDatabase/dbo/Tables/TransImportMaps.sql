CREATE TABLE [dbo].[TransImportMaps] (
    [TransImportMapId] INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]           NVARCHAR (100) NOT NULL,
    [SkipRows]         INT            DEFAULT ((0)) NOT NULL,
    [EsExcel]          BIT            DEFAULT ((0)) NOT NULL,
    [Separador]        NVARCHAR (100) NULL,
    [UsuarioId]        INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([TransImportMapId] ASC)
);

