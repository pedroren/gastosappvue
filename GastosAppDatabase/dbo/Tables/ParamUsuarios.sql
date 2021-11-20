CREATE TABLE [dbo].[ParamUsuarios] (
    [ParamUsuarioId]  INT             IDENTITY (1, 1) NOT NULL,
    [UsuarioId]       INT             NOT NULL,
    [SalarioRecibido] DECIMAL (18, 2) CONSTRAINT [DF_ParamUsuarios_SalarioRecibido] DEFAULT ((0)) NULL,
    [HorasTrabajadas] DECIMAL (18)    CONSTRAINT [DF_ParamUsuarios_HorasTrabajo] DEFAULT ((0)) NULL,
    [GastosTrabajo]   DECIMAL (18, 2) NULL,
    [HorasGastadas]   DECIMAL (18)    CONSTRAINT [DF_ParamUsuarios_HorasGastadas] DEFAULT ((0)) NULL,
    [SalarioHoraReal] DECIMAL (18, 2) CONSTRAINT [DF_ParamUsuarios_SalarioHoraReal] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_ParamUsuarios] PRIMARY KEY CLUSTERED ([ParamUsuarioId] ASC),
    CONSTRAINT [FK_ParamUsuarios_Usuarios] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([UsuarioId])
);


GO
ALTER TABLE [dbo].[ParamUsuarios] SET (LOCK_ESCALATION = AUTO);

