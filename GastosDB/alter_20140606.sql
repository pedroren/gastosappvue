
CREATE TABLE Presupuestos(
PresupuestoId INT IDENTITY(1,1) NOT NULL,
Nombre NVARCHAR(101) NOT NULL,
FechaDesde DATETIME,
FechaHasta DATETIME,
UsuarioId INT NOT NULL,
CONSTRAINT PK_Presupuestos PRIMARY KEY (PresupuestoId)
)
GO

CREATE TABLE PresupuestoDets(
PresupuestoDetId INT IDENTITY(1,1) NOT NULL,
PresupuestoId INT NOT NULL,
ConceptoId INT NOT NULL,
Monto DECIMAL(18,2) NOT NULL DEFAULT 0,
CONSTRAINT PK_PresupuestoDets PRIMARY KEY (PresupuestoDetId)
)
GO

ALTER TABLE PresupuestoDets  WITH CHECK ADD  CONSTRAINT [PresupuestoDet_Presupuesto] FOREIGN KEY([PresupuestoId])
REFERENCES Presupuestos ([PresupuestoId])
GO
ALTER TABLE PresupuestoDets CHECK CONSTRAINT [PresupuestoDet_Presupuesto]
GO