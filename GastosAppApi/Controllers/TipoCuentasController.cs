using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GastosAppCoreEF.DAL;
using GastosAppCoreEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastosAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TipoCuentasController : ControllerBase
    {
        private IUnitOfWork rep;

        public TipoCuentasController(IUnitOfWork uow)
        {
            this.rep = uow;
        }

        // GET api/<controller>/gettipocuentas
        [HttpOptions, HttpGet]
        public async Task<IActionResult> GetTipoCuentas()
        {
            return Ok(await rep.TipoCuentaRepository.Get().ToListAsync());
        }

    }
}