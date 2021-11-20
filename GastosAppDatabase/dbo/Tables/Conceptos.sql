CREATE TABLE [dbo].[Conceptos] (
    [ConceptoId]               INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]                   NVARCHAR (101) NOT NULL,
    [EsGasto]                  BIT            NOT NULL,
    [ConceptoPadre_ConceptoId] INT            NULL,
    [EsFrecuente]              BIT            CONSTRAINT [DF_Conceptos_EsFrecuente] DEFAULT ((0)) NOT NULL,
    [UsuarioId]                INT            NULL,
    [Activo]                   BIT            CONSTRAINT [DF_Conceptos_Activo] DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([ConceptoId] ASC),
    CONSTRAINT [Concepto_ConceptoPadre] FOREIGN KEY ([ConceptoPadre_ConceptoId]) REFERENCES [dbo].[Conceptos] ([ConceptoId])
);

