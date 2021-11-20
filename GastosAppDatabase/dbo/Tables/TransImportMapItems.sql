CREATE TABLE [dbo].[TransImportMapItems] (
    [TransImportMapItemId] INT            IDENTITY (1, 1) NOT NULL,
    [TransImportMapId]     INT            NULL,
    [Nombre]               NVARCHAR (100) NOT NULL,
    [Posicion]             INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([TransImportMapItemId] ASC),
    CONSTRAINT [TransImportMapItem_TransImportMap] FOREIGN KEY ([TransImportMapId]) REFERENCES [dbo].[TransImportMaps] ([TransImportMapId]) ON DELETE CASCADE
);

