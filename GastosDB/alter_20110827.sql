use GastosDB
go

CREATE TABLE dbo.ParamUsuarios
	(
	ParamUsuarioId int NOT NULL IDENTITY (1, 1),
	UsuarioId int NOT NULL,
	SalarioRecibido decimal(18, 2) NULL,
	HorasTrabajadas decimal(18, 0) NULL,
	GastosTrabajo decimal(18, 2) NULL,
	HorasGastadas decimal(18, 0) NULL,
	SalarioHoraReal decimal(18, 2) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ParamUsuarios ADD CONSTRAINT
	DF_ParamUsuarios_SalarioRecibido DEFAULT 0 FOR SalarioRecibido
GO
ALTER TABLE dbo.ParamUsuarios ADD CONSTRAINT
	DF_ParamUsuarios_HorasTrabajo DEFAULT 0 FOR HorasTrabajadas
GO
ALTER TABLE dbo.ParamUsuarios ADD CONSTRAINT
	DF_ParamUsuarios_HorasGastadas DEFAULT 0 FOR HorasGastadas
GO
ALTER TABLE dbo.ParamUsuarios ADD CONSTRAINT
	DF_ParamUsuarios_SalarioHoraReal DEFAULT 0 FOR SalarioHoraReal
GO
ALTER TABLE dbo.ParamUsuarios ADD CONSTRAINT
	PK_ParamUsuarios PRIMARY KEY CLUSTERED 
	(
	ParamUsuarioId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.ParamUsuarios SET (LOCK_ESCALATION = AUTO)
GO

ALTER TABLE dbo.ParamUsuarios ADD CONSTRAINT
	FK_ParamUsuarios_Usuarios FOREIGN KEY
	(
	UsuarioId
	) REFERENCES dbo.Usuarios
	(
	UsuarioId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO