ALTER TABLE dbo.DescripFrecuentes
ADD Activo BIT DEFAULT 1
GO

UPDATE dbo.DescripFrecuentes
SET Activo = 1
WHERE Activo IS null