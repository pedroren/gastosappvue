using GastosAppCoreEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosAppCoreEF.DAL
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<TipoCuenta> TipoCuentaRepository { get; }
        IGenericRepository<Usuario> UsuarioRepository { get; }
        IGenericRepository<Moneda> MonedaRepository { get; }
        IGenericRepository<Cuenta> CuentaRepository { get; }
        IGenericRepository<Concepto> ConceptoRepository { get; }
        IGenericRepository<TipoTransaccion> TipoTransaccionRepository { get; }
        IGenericRepository<Transaccion> TransaccionRepository { get; }
        IGenericRepository<Factura> FacturaRepository { get; }
        IGenericRepository<vCashFlow> vCashFlowRepository { get; }
        IGenericRepository<Presupuesto> PresupuestoRepository { get; }
        IGenericRepository<PresupuestoDet> PresupuestoDetRepository { get; }

        void Save();
        Task<int> SaveAsync();
        void RejectChanges();

        Transaccion GetNewTransaccionRecord();
        IQueryable<Transaccion> FindTransByFechaByUsuario(string Usuario, DateTime FechaDesde, DateTime FechaHasta, int? ConceptoId, int? CuentaId);
        Transaccion AddTransaccion(Transaccion newTransaccion);
        Transaccion UpdateTransaccion(Transaccion transaccion);
        bool DeleteTransaccion(int transaccionId);
        void ActualizaBalanceCuenta(int? CuentaId, decimal Monto);
        DateTime GetProxFechaFact(Factura factura);
        DateTime GetProxFechaFact(String frecuencia, DateTime ultFecha, int dia, int mes);
        DateTime GetProxFechaFact(int FacturaId);
    }
}
