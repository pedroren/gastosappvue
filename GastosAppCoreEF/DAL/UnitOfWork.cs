using GastosAppCoreEF.Models;
using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GastosAppCoreEF.DAL
{
    public class UnitOfWork: IUnitOfWork
    {
        private GastosappContext context;
        private IGenericRepository<TipoCuenta> tipocuentaRepository;
        private IGenericRepository<Usuario> usuarioRepository;
        private IGenericRepository<Moneda> monedaRepository;
        private IGenericRepository<Cuenta> cuentaRepository;
        private IGenericRepository<Concepto> conceptoRepository;
        private IGenericRepository<TipoTransaccion> tipoTransaccionRepository;
        private IGenericRepository<Transaccion> transaccionRepository;
        private IGenericRepository<Factura> facturaRepository;
        private IGenericRepository<vCashFlow> vcashFlowRepository;
        private IGenericRepository<Presupuesto> presupuestoRepository;
        private IGenericRepository<PresupuestoDet> presupuestoDetRepository;


        public UnitOfWork(DbContextOptions<GastosappContext> options)
        {
            context = new GastosappContext(options);
        }

        public IGenericRepository<TipoCuenta> TipoCuentaRepository
        {
            get
            {
                if (this.tipocuentaRepository == null)
                {
                    this.tipocuentaRepository = new GenericRepository<TipoCuenta>(context);
                }
                return tipocuentaRepository;
            }
        }

        public IGenericRepository<Usuario> UsuarioRepository
        {
            get
            {
                if (this.usuarioRepository == null)
                {
                    this.usuarioRepository = new GenericRepository<Usuario>(context);
                }
                return usuarioRepository;
            }
        }

        public IGenericRepository<Moneda> MonedaRepository
        {
            get
            {
                if (this.monedaRepository == null)
                {
                    this.monedaRepository = new GenericRepository<Moneda>(context);
                }
                return monedaRepository;
            }
        }

        public IGenericRepository<Cuenta> CuentaRepository
        {
            get
            {
                if (this.cuentaRepository == null)
                {
                    this.cuentaRepository = new GenericRepository<Cuenta>(context);
                }
                return cuentaRepository;
            }
        }

        public IGenericRepository<Concepto> ConceptoRepository
        {
            get
            {
                if (this.conceptoRepository == null)
                {
                    this.conceptoRepository = new GenericRepository<Concepto>(context);
                }
                return conceptoRepository;
            }
        }

        public IGenericRepository<TipoTransaccion> TipoTransaccionRepository
        {
            get
            {
                if (this.tipoTransaccionRepository == null)
                {
                    this.tipoTransaccionRepository = new GenericRepository<TipoTransaccion>(context);
                }
                return tipoTransaccionRepository;
            }
        }

        public IGenericRepository<Transaccion> TransaccionRepository
        {
            get
            {
                if (this.transaccionRepository == null)
                {
                    this.transaccionRepository = new GenericRepository<Transaccion>(context);
                }
                return transaccionRepository;
            }
        }

        public IGenericRepository<Factura> FacturaRepository
        {
            get
            {
                if (this.facturaRepository == null)
                {
                    this.facturaRepository = new GenericRepository<Factura>(context);
                }
                return facturaRepository;
            }
        }

        public IGenericRepository<vCashFlow> vCashFlowRepository
        {
            get
            {
                if (this.vcashFlowRepository == null)
                {
                    this.vcashFlowRepository = new GenericRepository<vCashFlow>(context);
                }
                return vcashFlowRepository;
            }
        }

        public IGenericRepository<Presupuesto> PresupuestoRepository
        {
            get
            {
                if (this.presupuestoRepository == null)
                {
                    this.presupuestoRepository = new GenericRepository<Presupuesto>(context);
                }
                return presupuestoRepository;
            }
        }

        public IGenericRepository<PresupuestoDet> PresupuestoDetRepository
        {
            get
            {
                if (this.presupuestoDetRepository == null)
                {
                    this.presupuestoDetRepository = new GenericRepository<PresupuestoDet>(context);
                }
                return presupuestoDetRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public void RejectChanges()
        {
            foreach (var entry in context.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        #region Transacciones
        public Transaccion GetNewTransaccionRecord()
        {
            var resultado = new Transaccion
            {
                TipoTransaccionId = TipoTransaccionRepository.Get().FirstOrDefault().TipoTransaccionId,
                Fecha = DateTime.Today,
                Monto = 0
            };

            return resultado;
        }

        public IQueryable<Transaccion> FindTransByFechaByUsuario(string Usuario, DateTime FechaDesde, DateTime FechaHasta, int? ConceptoId, int? CuentaId)
        {
            if (ConceptoId == 0)
                ConceptoId = null;
            if (CuentaId == 0)
                CuentaId = null;

            var resultado = TransaccionRepository.Get(filter: t => t.Usuario.Login == Usuario);
            resultado = resultado.Where(t => t.Fecha >= FechaDesde && t.Fecha <= FechaHasta);
            if (ConceptoId.HasValue)
                resultado = resultado.Where(t => t.ConceptoId == ConceptoId);
            if (CuentaId.HasValue)
                resultado = resultado.Where(t => t.CuentaId == CuentaId);

            return resultado;
        }

        public Transaccion AddTransaccion(Transaccion newTransaccion)
        {
            newTransaccion.TipoTransaccion = TipoTransaccionRepository.GetByID(newTransaccion.TipoTransaccionId);
            if(newTransaccion.ConceptoId > 0)
                newTransaccion.Concepto = ConceptoRepository.GetByID(newTransaccion.ConceptoId);
            if (newTransaccion.CuentaId > 0)
                newTransaccion.Cuenta = CuentaRepository.GetByID(newTransaccion.CuentaId);
            if (newTransaccion.CuentaIdTransf > 0)
                newTransaccion.CuentaTransf = CuentaRepository.GetByID(newTransaccion.CuentaIdTransf);

            if (newTransaccion.Concepto != null)
            {
                if (newTransaccion.Concepto.EsGasto)
                {
                    newTransaccion.Monto = newTransaccion.Monto * -1;
                }
            }

            if (newTransaccion.TipoTransaccion.Nombre == "Transferencia")
            {
                if (newTransaccion.TasaTransf == 0)
                {
                    newTransaccion.TasaTransf = 1;
                }
                newTransaccion.MontoTransf = newTransaccion.Monto / newTransaccion.TasaTransf;
                ActualizaBalanceCuenta(newTransaccion.Cuenta.CuentaId, newTransaccion.Monto * -1);
                ActualizaBalanceCuenta(newTransaccion.CuentaTransf.CuentaId, newTransaccion.MontoTransf);
            }
            else
            {
                ActualizaBalanceCuenta(newTransaccion.Cuenta.CuentaId, newTransaccion.Monto);
            }
            TransaccionRepository.Insert(newTransaccion);
            //transaccionRepository.SaveChanges();

            if (newTransaccion.FacturaId != null)
            {
                Factura factura = FacturaRepository.GetByID(newTransaccion.FacturaId);
                factura.UltMonto = Math.Abs(newTransaccion.Monto);
                factura.UltFecha = newTransaccion.Fecha;
                factura.ProxFecha = GetProxFechaFact(factura);
                FacturaRepository.Update(factura);
            }

            Save();

            return newTransaccion;
        }

        public Transaccion UpdateTransaccion(Transaccion transaccion)
        {
            var transOriginal = TransaccionRepository.GetByID(transaccion.TransaccionId);
            if (transaccion.ConceptoId > 0)
                transaccion.Concepto = ConceptoRepository.GetByID(transaccion.ConceptoId);
            if (transaccion.CuentaId > 0)
            {
                //TO-DO: Actualizar cuenta
                transaccion.Cuenta = CuentaRepository.GetByID(transaccion.CuentaId);
            }
            
            if (transaccion.CuentaIdTransf > 0)
            {
                //TO-DO: Actualizar cuenta
                transaccion.CuentaTransf = CuentaRepository.GetByID(transaccion.CuentaIdTransf);
            }

            if (transOriginal.ConceptoId > 0)
                transOriginal.Concepto = ConceptoRepository.GetByID(transOriginal.ConceptoId);
            if (transOriginal.CuentaId > 0)
            {
                //TO-DO: Actualizar cuenta
                transOriginal.Cuenta = CuentaRepository.GetByID(transOriginal.CuentaId);
            }
                
            if (transOriginal.CuentaIdTransf > 0)
            {
                //TO-DO: Actualizar cuenta
                transOriginal.CuentaTransf = CuentaRepository.GetByID(transOriginal.CuentaIdTransf);
            }
                

            context.Entry(transOriginal).State = EntityState.Detached;
            TransaccionRepository.Update(transaccion);
            Save();
            return transaccion;
        }

        public bool DeleteTransaccion(int transaccionId)
        {
            var transaccion = TransaccionRepository.GetByID(transaccionId);
            //TO-DO: Actualizar cuenta
            if (transaccion.CuentaId > 0)
            {
                var cuenta = CuentaRepository.GetByID(transaccion.CuentaId);
                cuenta.Balance += (transaccion.Monto) * -1;
            }

            TransaccionRepository.Delete(transaccion);
            Save();
            return true;
        }
        #endregion

        #region Cuentas
        public void ActualizaBalanceCuenta(int? CuentaId, decimal Monto)
        {
            var cuenta = CuentaRepository.GetByID(CuentaId);
            cuenta.Balance += Monto;
            CuentaRepository.Update(cuenta);
            //Save();
        }
        #endregion

        #region Facturas
        public DateTime GetProxFechaFact(Factura factura)
        {
            DateTime proxfecha;

            DateTime ultfecha;
            ultfecha = factura.UltFecha ?? DateTime.Today;
            if (ultfecha < factura.ProxFecha)
                ultfecha = factura.ProxFecha;

            proxfecha = GetProxFechaFact(factura.Frecuencia, ultfecha, factura.Dia, factura.Mes);

            return proxfecha;
        }

        public DateTime GetProxFechaFact(String frecuencia, DateTime ultFecha, int dia, int mes)
        {
            DateTime proxfecha;

            if (frecuencia == "M")
            {
                //Mensual
                try
                {
                    proxfecha = DateTime.Parse(ultFecha.Year.ToString() + "-" + ultFecha.Month.ToString() + "-" + dia.ToString());
                }
                catch
                {
                    proxfecha = DateTime.Parse(ultFecha.Year.ToString() + "-" + ultFecha.Month.ToString() + "-01").AddMonths(1).AddDays(-1);
                }
                if (proxfecha <= DateTime.Today || proxfecha <= ultFecha)
                {
                    proxfecha = proxfecha.AddMonths(1);
                }
            }
            else
            {
                //Anual
                try
                {
                    proxfecha = DateTime.Parse(ultFecha.Year.ToString() + "-" + mes.ToString() + "-" + dia.ToString());
                }
                catch
                {
                    proxfecha = DateTime.Parse(ultFecha.Year.ToString() + "-" + mes.ToString() + "-" + dia.ToString()).AddMonths(1).AddDays(-1);
                }
                if (proxfecha <= DateTime.Today || proxfecha <= ultFecha)
                {
                    proxfecha = proxfecha.AddYears(1);
                }
            }

            return proxfecha;
        }

        public DateTime GetProxFechaFact(int FacturaId)
        {
            return GetProxFechaFact(FacturaRepository.GetByID(FacturaId));
        }
        #endregion

        #region Presupuesto

        #endregion

        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
