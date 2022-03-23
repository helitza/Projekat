using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KompanijaController : ControllerBase
    {
        public KompanijaContext Context { get; set; }

        public KompanijaController(Kompanija context)
        {
            Context = context;
        }

        [Route("Kompanije")]
        [HttpGet]
        public async Task<ActionResult> VratiKompanije()
        {
            try
            {
                return Ok(
                    await Context.Kompanije.Include(kompanija => kompanija.Tribine).ThenInclude(bf => bf.Tribina).ToListAsync());
                      
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}