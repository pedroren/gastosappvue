CREATE TABLE [dbo].[TransImportTags] (
    [TransImportTagId] INT            IDENTITY (1, 1) NOT NULL,
    [Tag]              NVARCHAR (100) NOT NULL,
    [ConceptoId]       INT            NOT NULL,
    [UsuarioId]        INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([TransImportTagId] ASC)
);

