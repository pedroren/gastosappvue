using System;
using System.Collections.Generic;
using System.Linq;
using GastosAppCoreEF.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosAppCoreEF.DAL
{
    public class TipoCuentaRepository : ITipoCuentaRepository
    {
        private GastosappContext context;

        public TipoCuentaRepository(GastosappContext context)
        {
            this.context = context;
        }

        public void DeleteTipoCuenta(int id)
        {
            TipoCuenta record = context.TipoCuentas.Find(id);
            context.TipoCuentas.Remove(record);
        }

        public TipoCuenta GetTipoCuentaByID(int id)
        {
            return context.TipoCuentas.Find(id);
        }

        public IEnumerable<TipoCuenta> GetTipoCuentas()
        {
            return context.TipoCuentas.ToList();
        }

        public void InsertTipoCuenta(TipoCuenta record)
        {
            context.TipoCuentas.Add(record);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateTipoCuenta(TipoCuenta record)
        {
            context.Entry(record).State = EntityState.Modified;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
