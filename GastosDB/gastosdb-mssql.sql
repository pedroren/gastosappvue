USE [master]
GO
/****** Object:  Database [gastosdb]    Script Date: 05/29/2012 18:52:56 ******/
CREATE DATABASE [gastosdb] 
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [gastosdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [gastosdb] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [gastosdb] SET ANSI_NULLS OFF
GO
ALTER DATABASE [gastosdb] SET ANSI_PADDING OFF
GO
ALTER DATABASE [gastosdb] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [gastosdb] SET ARITHABORT OFF
GO
ALTER DATABASE [gastosdb] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [gastosdb] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [gastosdb] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [gastosdb] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [gastosdb] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [gastosdb] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [gastosdb] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [gastosdb] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [gastosdb] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [gastosdb] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [gastosdb] SET  DISABLE_BROKER
GO
ALTER DATABASE [gastosdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [gastosdb] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [gastosdb] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [gastosdb] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [gastosdb] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [gastosdb] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [gastosdb] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [gastosdb] SET  READ_WRITE
GO
ALTER DATABASE [gastosdb] SET RECOVERY FULL
GO
ALTER DATABASE [gastosdb] SET  MULTI_USER
GO
ALTER DATABASE [gastosdb] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [gastosdb] SET DB_CHAINING OFF
GO
USE [gastosdb]
GO
/****** Object:  User [gastosuser]    Script Date: 05/29/2012 18:52:56 ******/
CREATE USER [gastosuser] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[gastosuser]
GO
/****** Object:  Schema [gastosuser]    Script Date: 05/29/2012 18:52:56 ******/
CREATE SCHEMA [gastosuser] AUTHORIZATION [gastosuser]
GO
/****** Object:  Table [dbo].[Monedas]    Script Date: 05/29/2012 18:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monedas](
	[MonedaId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Abreviatura] [nvarchar](5) NULL,
	[EsPrincipal] [bit] NOT NULL,
	[Tasa] [decimal](10, 4) NULL,
	[UsuarioId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MonedaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 05/29/2012 18:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Clave] [nvarchar](250) NULL,
	[Nombre] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuarios] SET (LOCK_ESCALATION = AUTO)
GO
/****** Object:  Table [dbo].[TipoTransacciones]    Script Date: 05/29/2012 18:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoTransacciones](
	[TipoTransaccionId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_TipoTransacciones] PRIMARY KEY CLUSTERED 
(
	[TipoTransaccionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoCuentas]    Script Date: 05/29/2012 18:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoCuentas](
	[TipoCuentaId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[TipoCuentaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Conceptos]    Script Date: 05/29/2012 18:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conceptos](
	[ConceptoId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](101) NOT NULL,
	[EsGasto] [bit] NOT NULL,
	[ConceptoPadre_ConceptoId] [int] NULL,
	[EsFrecuente] [bit] NOT NULL,
	[UsuarioId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ConceptoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 05/29/2012 18:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[CuentaId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[MonedaId] [int] NOT NULL,
	[TipoCuentaId] [int] NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[UsuarioId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CuentaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParamUsuarios]    Script Date: 05/29/2012 18:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParamUsuarios](
	[ParamUsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[SalarioRecibido] [decimal](18, 2) NULL,
	[HorasTrabajadas] [decimal](18, 0) NULL,
	[GastosTrabajo] [decimal](18, 2) NULL,
	[HorasGastadas] [decimal](18, 0) NULL,
	[SalarioHoraReal] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ParamUsuarios] PRIMARY KEY CLUSTERED 
(
	[ParamUsuarioId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ParamUsuarios] SET (LOCK_ESCALATION = AUTO)
GO
/****** Object:  Table [dbo].[Transacciones]    Script Date: 05/29/2012 18:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transacciones](
	[TransaccionId] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Descripcion] [nvarchar](100) NULL,
	[ConceptoId] [int] NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[CuentaId] [int] NOT NULL,
	[CuentaIdTransf] [int] NULL,
	[Comentario] [nvarchar](250) NULL,
	[TipoTransaccionId] [int] NOT NULL,
	[TasaTransf] [decimal](10, 4) NOT NULL,
	[MontoTransf] [decimal](18, 2) NOT NULL,
	[UsuarioId] [int] NULL,
 CONSTRAINT [PK__Transacc__86A849FE1273C1CD] PRIMARY KEY CLUSTERED 
(
	[TransaccionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DescripFrecuentes]    Script Date: 05/29/2012 18:52:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DescripFrecuentes](
	[DescripFrecuenteId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[ConceptoId] [int] NULL,
	[CuentaId] [int] NULL,
	[CuentaIdTransf] [int] NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[UsuarioId] [int] NULL,
 CONSTRAINT [PK_DescripFrecuentes] PRIMARY KEY CLUSTERED 
(
	[DescripFrecuenteId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vCashFlow]    Script Date: 05/29/2012 18:52:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vCashFlow] as
SELECT Transacciones.Fecha, Transacciones.Monto, Conceptos.Nombre as NombreConcepto, 
	   Conceptos.ConceptoId, null as CuentaIdTransf , Transacciones.UsuarioId
FROM dbo.Transacciones INNER JOIN
	 dbo.TipoTransacciones on TipoTransacciones.TipoTransaccionId = Transacciones.TipoTransaccionId inner join 
     dbo.Conceptos ON Transacciones.ConceptoId = Conceptos.ConceptoId INNER JOIN
     dbo.Cuentas ON Transacciones.CuentaId = Cuentas.CuentaId INNER JOIN
     dbo.TipoCuentas ON Cuentas.TipoCuentaId = TipoCuentas.TipoCuentaId 
where TipoCuentas.Nombre = 'Efectivo'
and TipoTransacciones.Nombre='Simple'
union
SELECT Transacciones.Fecha, Transacciones.Monto*-1 Monto, CuentasT.Nombre AS NombreCuentaTransf, 
		null, Transacciones.CuentaIdTransf, Transacciones.UsuarioId
FROM dbo.Transacciones INNER JOIN
	 dbo.TipoTransacciones on TipoTransacciones.TipoTransaccionId = Transacciones.TipoTransaccionId inner join 
     dbo.Cuentas ON Transacciones.CuentaId = Cuentas.CuentaId INNER JOIN
     dbo.TipoCuentas ON Cuentas.TipoCuentaId = TipoCuentas.TipoCuentaId INNER JOIN
     dbo.Cuentas AS CuentasT ON Transacciones.CuentaIdTransf = CuentasT.CuentaId INNER JOIN
	 dbo.TipoCuentas TipoCuentasT ON CuentasT.TipoCuentaId = TipoCuentasT.TipoCuentaId 
where TipoCuentas.Nombre = 'Efectivo'
and TipoCuentasT.Nombre <> 'Efectivo'
and TipoTransacciones.Nombre='Transferencia'
GO
/****** Object:  Default [DF_Conceptos_EsFrecuente]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[Conceptos] ADD  CONSTRAINT [DF_Conceptos_EsFrecuente]  DEFAULT ((0)) FOR [EsFrecuente]
GO
/****** Object:  Default [DF_ParamUsuarios_SalarioRecibido]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[ParamUsuarios] ADD  CONSTRAINT [DF_ParamUsuarios_SalarioRecibido]  DEFAULT ((0)) FOR [SalarioRecibido]
GO
/****** Object:  Default [DF_ParamUsuarios_HorasTrabajo]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[ParamUsuarios] ADD  CONSTRAINT [DF_ParamUsuarios_HorasTrabajo]  DEFAULT ((0)) FOR [HorasTrabajadas]
GO
/****** Object:  Default [DF_ParamUsuarios_HorasGastadas]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[ParamUsuarios] ADD  CONSTRAINT [DF_ParamUsuarios_HorasGastadas]  DEFAULT ((0)) FOR [HorasGastadas]
GO
/****** Object:  Default [DF_ParamUsuarios_SalarioHoraReal]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[ParamUsuarios] ADD  CONSTRAINT [DF_ParamUsuarios_SalarioHoraReal]  DEFAULT ((0)) FOR [SalarioHoraReal]
GO
/****** Object:  Default [DF__Transacci__TasaT__29572725]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[Transacciones] ADD  DEFAULT ((0)) FOR [TasaTransf]
GO
/****** Object:  Default [DF__Transacci__Monto__2A4B4B5E]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[Transacciones] ADD  DEFAULT ((0)) FOR [MontoTransf]
GO
/****** Object:  Default [DF__DescripFr__Monto__2B3F6F97]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[DescripFrecuentes] ADD  DEFAULT ((0)) FOR [Monto]
GO
/****** Object:  ForeignKey [Concepto_ConceptoPadre]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[Conceptos]  WITH CHECK ADD  CONSTRAINT [Concepto_ConceptoPadre] FOREIGN KEY([ConceptoPadre_ConceptoId])
REFERENCES [dbo].[Conceptos] ([ConceptoId])
GO
ALTER TABLE [dbo].[Conceptos] CHECK CONSTRAINT [Concepto_ConceptoPadre]
GO
/****** Object:  ForeignKey [Cuenta_Moneda]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD  CONSTRAINT [Cuenta_Moneda] FOREIGN KEY([MonedaId])
REFERENCES [dbo].[Monedas] ([MonedaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cuentas] CHECK CONSTRAINT [Cuenta_Moneda]
GO
/****** Object:  ForeignKey [Cuenta_TipoCuenta]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD  CONSTRAINT [Cuenta_TipoCuenta] FOREIGN KEY([TipoCuentaId])
REFERENCES [dbo].[TipoCuentas] ([TipoCuentaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cuentas] CHECK CONSTRAINT [Cuenta_TipoCuenta]
GO
/****** Object:  ForeignKey [FK_ParamUsuarios_Usuarios]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[ParamUsuarios]  WITH CHECK ADD  CONSTRAINT [FK_ParamUsuarios_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO
ALTER TABLE [dbo].[ParamUsuarios] CHECK CONSTRAINT [FK_ParamUsuarios_Usuarios]
GO
/****** Object:  ForeignKey [Transaccion_Concepto]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [Transaccion_Concepto] FOREIGN KEY([ConceptoId])
REFERENCES [dbo].[Conceptos] ([ConceptoId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [Transaccion_Concepto]
GO
/****** Object:  ForeignKey [Transaccion_Cuenta]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [Transaccion_Cuenta] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuentas] ([CuentaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [Transaccion_Cuenta]
GO
/****** Object:  ForeignKey [Transaccion_CuentaTransf]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [Transaccion_CuentaTransf] FOREIGN KEY([CuentaIdTransf])
REFERENCES [dbo].[Cuentas] ([CuentaId])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [Transaccion_CuentaTransf]
GO
/****** Object:  ForeignKey [Transaccion_TipoTransaccion]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [Transaccion_TipoTransaccion] FOREIGN KEY([TipoTransaccionId])
REFERENCES [dbo].[TipoTransacciones] ([TipoTransaccionId])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [Transaccion_TipoTransaccion]
GO
/****** Object:  ForeignKey [DescripFrecuente_Concepto]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[DescripFrecuentes]  WITH CHECK ADD  CONSTRAINT [DescripFrecuente_Concepto] FOREIGN KEY([ConceptoId])
REFERENCES [dbo].[Conceptos] ([ConceptoId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DescripFrecuentes] CHECK CONSTRAINT [DescripFrecuente_Concepto]
GO
/****** Object:  ForeignKey [DescripFrecuente_Cuenta]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[DescripFrecuentes]  WITH CHECK ADD  CONSTRAINT [DescripFrecuente_Cuenta] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuentas] ([CuentaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DescripFrecuentes] CHECK CONSTRAINT [DescripFrecuente_Cuenta]
GO
/****** Object:  ForeignKey [DescripFrecuente_CuentaTransf]    Script Date: 05/29/2012 18:52:57 ******/
ALTER TABLE [dbo].[DescripFrecuentes]  WITH CHECK ADD  CONSTRAINT [DescripFrecuente_CuentaTransf] FOREIGN KEY([CuentaIdTransf])
REFERENCES [dbo].[Cuentas] ([CuentaId])
GO
ALTER TABLE [dbo].[DescripFrecuentes] CHECK CONSTRAINT [DescripFrecuente_CuentaTransf]
GO
