CREATE TABLE [dbo].[Usuarios] (
    [UsuarioId] INT            IDENTITY (1, 1) NOT NULL,
    [Login]     NVARCHAR (50)  NOT NULL,
    [Clave]     NVARCHAR (250) NULL,
    [Nombre]    NVARCHAR (100) NULL,
    [Email]     NVARCHAR (100) NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([UsuarioId] ASC)
);


GO
ALTER TABLE [dbo].[Usuarios] SET (LOCK_ESCALATION = AUTO);

