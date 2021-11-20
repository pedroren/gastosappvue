using GastosAppCoreEF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GastosAppCoreEF.DAL
{
    public interface ITipoCuentaRepository: IDisposable
    {
        IEnumerable<TipoCuenta> GetTipoCuentas();
        TipoCuenta GetTipoCuentaByID(int id);
        void InsertTipoCuenta(TipoCuenta record);
        void DeleteTipoCuenta(int id);
        void UpdateTipoCuenta(TipoCuenta record);
        void Save();
        
    }
}
