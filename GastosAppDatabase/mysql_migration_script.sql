-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: gastosdb
-- Source Schemata: gastosdb
-- Created: Wed Jan 07 09:22:04 2015
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;;

-- ----------------------------------------------------------------------------
-- Schema gastosdb
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `gastosdb` ;
CREATE SCHEMA IF NOT EXISTS `gastosdb` ;

-- ----------------------------------------------------------------------------
-- Table gastosdb.Monedas
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`Monedas` (
  `MonedaId` INT NOT NULL,
  `Nombre` VARCHAR(100) NOT NULL,
  `Abreviatura` VARCHAR(5) NULL,
  `EsPrincipal` TINYINT(1) NOT NULL,
  `Tasa` DECIMAL(10,4) NULL,
  `UsuarioId` INT NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.ParamDynUsuarios
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`ParamDynUsuarios` (
  `ParamDynUsuarioId` INT NOT NULL,
  `UsuarioId` INT NOT NULL,
  `Nombre` VARCHAR(100) NOT NULL,
  `Valor` VARCHAR(250) NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.ParamUsuarios
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`ParamUsuarios` (
  `ParamUsuarioId` INT NOT NULL,
  `UsuarioId` INT NOT NULL,
  `SalarioRecibido` DECIMAL(18,2) NULL,
  `HorasTrabajadas` BIGINT NULL,
  `GastosTrabajo` DECIMAL(18,2) NULL,
  `HorasGastadas` BIGINT NULL,
  `SalarioHoraReal` DECIMAL(18,2) NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.TipoCuentas
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`TipoCuentas` (
  `TipoCuentaId` INT NOT NULL,
  `Nombre` VARCHAR(128) NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.TipoTransacciones
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`TipoTransacciones` (
  `TipoTransaccionId` INT NOT NULL,
  `Nombre` VARCHAR(60) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.Transacciones
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`Transacciones` (
  `TransaccionId` INT NOT NULL,
  `Fecha` DATETIME NOT NULL,
  `Descripcion` VARCHAR(100) NULL,
  `ConceptoId` INT NULL,
  `Monto` DECIMAL(18,2) NOT NULL,
  `CuentaId` INT NOT NULL,
  `CuentaIdTransf` INT NULL,
  `Comentario` VARCHAR(250) NULL,
  `TipoTransaccionId` INT NOT NULL,
  `TasaTransf` DECIMAL(10,4) NOT NULL,
  `MontoTransf` DECIMAL(18,2) NOT NULL,
  `UsuarioId` INT NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.TransImportMapItems
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`TransImportMapItems` (
  `TransImportMapItemId` INT NOT NULL,
  `TransImportMapId` INT NULL,
  `Nombre` VARCHAR(100) NOT NULL,
  `Posicion` INT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.TransImportMaps
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`TransImportMaps` (
  `TransImportMapId` INT NOT NULL,
  `Nombre` VARCHAR(100) NOT NULL,
  `SkipRows` INT NOT NULL,
  `EsExcel` TINYINT(1) NOT NULL,
  `Separador` VARCHAR(100) NULL,
  `UsuarioId` INT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.TransImportTags
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`TransImportTags` (
  `TransImportTagId` INT NOT NULL,
  `Tag` VARCHAR(100) NOT NULL,
  `ConceptoId` INT NOT NULL,
  `UsuarioId` INT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.Usuarios
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`Usuarios` (
  `UsuarioId` INT NOT NULL,
  `Login` VARCHAR(50) NOT NULL,
  `Clave` VARCHAR(250) NULL,
  `Nombre` VARCHAR(100) NULL,
  `Email` VARCHAR(100) NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.Facturas
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`Facturas` (
  `FacturaID` INT NOT NULL,
  `Nombre` VARCHAR(100) NULL,
  `DescripFrecuenteId` INT NULL,
  `Frecuencia` CHAR(1) NOT NULL,
  `Dia` SMALLINT NOT NULL,
  `Mes` SMALLINT NULL,
  `ProxFecha` DATETIME NULL,
  `UltFecha` DATETIME NULL,
  `UltMonto` DECIMAL(18,2) NULL,
  `Activo` TINYINT(1) NOT NULL,
  `UsuarioId` INT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.PresupuestoDets
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`PresupuestoDets` (
  `PresupuestoDetId` INT NOT NULL,
  `PresupuestoId` INT NOT NULL,
  `ConceptoId` INT NOT NULL,
  `Monto` DECIMAL(18,2) NOT NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.Presupuestos
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`Presupuestos` (
  `PresupuestoId` INT NOT NULL,
  `Nombre` VARCHAR(101) NOT NULL,
  `FechaDesde` DATETIME NULL,
  `FechaHasta` DATETIME NULL,
  `UsuarioId` INT NOT NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.Conceptos
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`Conceptos` (
  `ConceptoId` INT NOT NULL,
  `Nombre` VARCHAR(101) NOT NULL,
  `EsGasto` TINYINT(1) NOT NULL,
  `ConceptoPadre_ConceptoId` INT NULL,
  `EsFrecuente` TINYINT(1) NOT NULL,
  `UsuarioId` INT NULL,
  `Activo` TINYINT(1) NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.Cuentas
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`Cuentas` (
  `CuentaId` INT NOT NULL,
  `Nombre` VARCHAR(100) NOT NULL,
  `MonedaId` INT NOT NULL,
  `TipoCuentaId` INT NOT NULL,
  `Balance` DECIMAL(18,2) NOT NULL,
  `UsuarioId` INT NULL,
  `Activo` TINYINT(1) NULL);

-- ----------------------------------------------------------------------------
-- Table gastosdb.DescripFrecuentes
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `gastosdb`.`DescripFrecuentes` (
  `DescripFrecuenteId` INT NOT NULL,
  `Nombre` VARCHAR(100) NOT NULL,
  `ConceptoId` INT NULL,
  `CuentaId` INT NULL,
  `CuentaIdTransf` INT NULL,
  `Monto` DECIMAL(18,2) NOT NULL,
  `UsuarioId` INT NULL,
  `Activo` TINYINT(1) NULL);

-- ----------------------------------------------------------------------------
-- View gastosdb.vCashFlow
-- ----------------------------------------------------------------------------
-- USE `gastosdb`;
-- 
-- CREATE  OR REPLACE view [dbo].[vCashFlow] as
-- SELECT Transacciones.Fecha, Transacciones.Monto, Conceptos.Nombre as NombreConcepto, 
-- 	   Conceptos.ConceptoId, null as CuentaIdTransf , Transacciones.UsuarioId
-- FROM dbo.Transacciones INNER JOIN
-- 	 dbo.TipoTransacciones on TipoTransacciones.TipoTransaccionId = Transacciones.TipoTransaccionId inner join 
--      dbo.Conceptos ON Transacciones.ConceptoId = Conceptos.ConceptoId INNER JOIN
--      dbo.Cuentas ON Transacciones.CuentaId = Cuentas.CuentaId INNER JOIN
--      dbo.TipoCuentas ON Cuentas.TipoCuentaId = TipoCuentas.TipoCuentaId 
-- where TipoCuentas.Nombre = 'Efectivo'
-- and TipoTransacciones.Nombre='Simple'
-- union
-- SELECT Transacciones.Fecha, Transacciones.Monto*-1 Monto, CuentasT.Nombre AS NombreCuentaTransf, 
-- 		null, Transacciones.CuentaIdTransf, Transacciones.UsuarioId
-- FROM dbo.Transacciones INNER JOIN
-- 	 dbo.TipoTransacciones on TipoTransacciones.TipoTransaccionId = Transacciones.TipoTransaccionId inner join 
--      dbo.Cuentas ON Transacciones.CuentaId = Cuentas.CuentaId INNER JOIN
--      dbo.TipoCuentas ON Cuentas.TipoCuentaId = TipoCuentas.TipoCuentaId INNER JOIN
--      dbo.Cuentas AS CuentasT ON Transacciones.CuentaIdTransf = CuentasT.CuentaId INNER JOIN
-- 	 dbo.TipoCuentas TipoCuentasT ON CuentasT.TipoCuentaId = TipoCuentasT.TipoCuentaId 
-- where TipoCuentas.Nombre = 'Efectivo'
-- and TipoCuentasT.Nombre <> 'Efectivo'
-- and TipoTransacciones.Nombre='Transferencia'
-- 
-- ;
SET FOREIGN_KEY_CHECKS = 1;;
