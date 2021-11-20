
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

