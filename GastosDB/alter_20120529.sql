use gastosdb
go

create table dbo.TransImportTags(
	TransImportTagId int IDENTITY(1,1) NOT NULL,
	Tag [nvarchar](100) NOT NULL,
	ConceptoId int NOT NULL,
	UsuarioId int NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		TransImportTagId ASC
	)
)
GO

create table dbo.TransImportMaps(
	TransImportMapId int IDENTITY(1,1) NOT NULL,
	Nombre [nvarchar](100) NOT NULL,
	SkipRows int not null default 0,
	EsExcel bit not null default 0,
	Separador [nvarchar](100),
	UsuarioId int not null,
	PRIMARY KEY CLUSTERED 
	(
		TransImportMapId ASC
	)
)
GO

create table dbo.TransImportMapItems(
	TransImportMapItemId int IDENTITY(1,1) NOT NULL,
	TransImportMapId int not null,
	Nombre [nvarchar](100) NOT NULL,
	Posicion int not null,
	PRIMARY KEY CLUSTERED 
	(
		TransImportMapItemId ASC
	)
)
GO

ALTER TABLE [dbo].TransImportMapItems  WITH CHECK ADD  CONSTRAINT [TransImportMapItem_TransImportMap] FOREIGN KEY(TransImportMapId)
REFERENCES [dbo].TransImportMaps (TransImportMapId)
ON DELETE CASCADE
GO