CREATE TABLE [dbo].[ParamDynUsuarios] (
    [ParamDynUsuarioId] INT            IDENTITY (1, 1) NOT NULL,
    [UsuarioId]         INT            NOT NULL,
    [Nombre]            NVARCHAR (100) NOT NULL,
    [Valor]             NVARCHAR (250) NULL,
    CONSTRAINT [PK_ParamDynUsuarios] PRIMARY KEY CLUSTERED ([ParamDynUsuarioId] ASC),
    CONSTRAINT [FK_ParamDynUsuarios_Usuarios] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([UsuarioId])
);

