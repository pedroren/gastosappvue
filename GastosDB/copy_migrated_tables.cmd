REM Workbench Table Data copy script
REM 
REM Execute this to copy table data from a source RDBMS to MySQL.
REM Edit the options below to customize it. You will need to provide passwords, at least.
REM 
REM Source DB: Mssql@gastos_local (Microsoft SQL Server)
REM Target DB: Mysql@104.131.4.84:3306


@ECHO OFF
REM Source and target DB passwords
set arg_source_password=Gastos1
set arg_target_password=Gastos1

IF [%arg_source_password%] == [] (
    IF [%arg_target_password%] == [] (
        ECHO WARNING: Both source and target RDBMSes passwords are empty. You should edit this file to set them.
    )
)
set arg_worker_count=1
REM Uncomment the following options according to your needs

REM Whether target tables should be truncated before copy
set arg_truncate_target=--truncate-target
REM Enable debugging output
REM set arg_debug_output=--log-level=debug3


REM Creation of file with table definitions for copytable

set table_file="%TMP%\wb_tables_to_migrate.txt"
TYPE NUL > "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[ParamDynUsuarios]	`gastosdb`	`ParamDynUsuarios`			[ParamDynUsuarioId], [UsuarioId], [Nombre], [Valor] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[ParamUsuarios]	`gastosdb`	`ParamUsuarios`			[ParamUsuarioId], [UsuarioId], [SalarioRecibido], CAST([HorasTrabajadas] as BIGINT) as [HorasTrabajadas], [GastosTrabajo], CAST([HorasGastadas] as BIGINT) as [HorasGastadas], [SalarioHoraReal] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[TransImportMapItems]	`gastosdb`	`TransImportMapItems`			[TransImportMapItemId], [TransImportMapId], [Nombre], [Posicion] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[DescripFrecuentes]	`gastosdb`	`DescripFrecuentes`			[DescripFrecuenteId], [Nombre], [ConceptoId], [CuentaId], [CuentaIdTransf], [Monto], [UsuarioId], [Activo] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[Presupuestos]	`gastosdb`	`Presupuestos`			[PresupuestoId], [Nombre], [FechaDesde], [FechaHasta], [UsuarioId] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[TransImportMaps]	`gastosdb`	`TransImportMaps`			[TransImportMapId], [Nombre], [SkipRows], [EsExcel], [Separador], [UsuarioId] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[Monedas]	`gastosdb`	`Monedas`			[MonedaId], [Nombre], [Abreviatura], [EsPrincipal], [Tasa], [UsuarioId] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[Transacciones]	`gastosdb`	`Transacciones`			[TransaccionId], [Fecha], [Descripcion], [ConceptoId], [Monto], [CuentaId], [CuentaIdTransf], [Comentario], [TipoTransaccionId], [TasaTransf], [MontoTransf], [UsuarioId] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[Usuarios]	`gastosdb`	`Usuarios`			[UsuarioId], [Login], [Clave], [Nombre], [Email] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[Conceptos]	`gastosdb`	`Conceptos`			[ConceptoId], [Nombre], [EsGasto], [ConceptoPadre_ConceptoId], [EsFrecuente], [UsuarioId], [Activo] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[TipoCuentas]	`gastosdb`	`TipoCuentas`			[TipoCuentaId], [Nombre] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[Cuentas]	`gastosdb`	`Cuentas`			[CuentaId], [Nombre], [MonedaId], [TipoCuentaId], [Balance], [UsuarioId], [Activo] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[Facturas]	`gastosdb`	`Facturas`			[FacturaID], [Nombre], [DescripFrecuenteId], CAST([Frecuencia] as NCHAR(1)) as [Frecuencia], [Dia], [Mes], [ProxFecha], [UltFecha], [UltMonto], [Activo], [UsuarioId] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[PresupuestoDets]	`gastosdb`	`PresupuestoDets`			[PresupuestoDetId], [PresupuestoId], [ConceptoId], [Monto] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[TransImportTags]	`gastosdb`	`TransImportTags`			[TransImportTagId], [Tag], [ConceptoId], [UsuarioId] >> "%TMP%\wb_tables_to_migrate.txt"
ECHO [gastosdb]	[dbo].[TipoTransacciones]	`gastosdb`	`TipoTransacciones`			[TipoTransaccionId], [Nombre] >> "%TMP%\wb_tables_to_migrate.txt"


"C:\Program Files\MySQL\MySQL Workbench 6.2 CE\wbcopytables.exe" --odbc-source="DSN=gastos_local;DATABASE=gastosdb;UID=" --target="gastosuser@104.131.4.84:3306" --source-password="%arg_source_password%" --target-password="%arg_target_password%" --table-file="%table_file%" --thread-count=%arg_worker_count% %arg_truncate_target% %arg_debug_output%

REM Removes the file with the table definitions
DEL "%TMP%\wb_tables_to_migrate.txt"


PAUSE