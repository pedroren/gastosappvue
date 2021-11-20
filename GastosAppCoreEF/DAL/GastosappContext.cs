using GastosAppCoreEF.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GastosAppCoreEF.DAL
{
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class GastosappContext: DbContext
    {
        public GastosappContext(DbContextOptions<GastosappContext> options) : base(options)
        {
            //base.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<TipoCuenta> TipoCuentas { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Concepto> Conceptos { get; set; }
        public DbSet<TipoTransaccion> TipoTransacciones { get; set; }
        public DbSet<DescripFrecuente> DescripFrecuentes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<PresupuestoDet> PresupuestoDets { get; set; }
        public DbSet<vCashFlow> vCashFlows { get; set; }
        public DbSet<TransImportMap> TransImportMaps { get; set; }
        public DbSet<TransImportMapItem> TransImportMapItems { get; set; }
        public DbSet<TransImportTag> TransImportTags { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("name=GastosappContext");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
