using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MestoController : ControllerBase
    {
        public KompanijaContext Context { get; set; }

        public MestoController(KompanijaContext context)
        {
            Context = context;
        }

        [Route("Mesto/{idOdrzavanja}")]
        [HttpGet]
        public async Task<ActionResult> VratiMesto(int idOdrzavanja)
        {
            try
            {
                return Ok(await Context.Odrzavanja.Where(p => p.Id == idOdrzavanja).Include(p => p.Akreditacije ).ThenInclude(f=>f.sediste)
                .Include( p => p.mesto).Select(s=> new {s.mesto.BrRedova, s.mesto.BrSedistaPoRedu, s.Akreditacije}).ToListAsync());
                        
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}