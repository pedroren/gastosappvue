use gastosdb
go

create table dbo.ParamDynUsuarios(
	[ParamDynUsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	Nombre nvarchar(100) not null,
	Valor nvarchar(250) null,
	CONSTRAINT [PK_ParamDynUsuarios] PRIMARY KEY CLUSTERED 
(
	[ParamDynUsuarioId] ASC
)
)
go

ALTER TABLE [dbo].ParamDynUsuarios  WITH CHECK ADD  CONSTRAINT [FK_ParamDynUsuarios_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
