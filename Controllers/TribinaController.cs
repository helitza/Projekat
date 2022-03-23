using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TribinaController : ControllerBase
    {
        public KompanijaContext Context { get; set; }

        public TribinaController(KompanijaContext context)
        {
            Context = context;
        }



        [Route("Odrzavanja/{idTribine}/{idKompanije}")]
        [HttpGet]
        public async Task<ActionResult> VratiOdrzavanja(int idTribina, int idKompanija)
        {
            try
            {
             
                var ret = await Context.Odrzavanja.Where(p => p.Tribina.Id==idTribina && p.Kompanija.Id == idKompanija).Include(p => p.Tribina).Include(p => p.mesto).ToListAsync();
                return Ok(ret);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("IzbrisiTribinu/{IdKompanije}/{idTribine}")]
        [HttpDelete]
        public async Task<ActionResult> izbrisiTribinu(int IdKompanije, int idTribine)
        {
            if (IdKompanije == 0)
                return BadRequest("Nije odabrana kompanija");
            if (idTribine == 0)
                return BadRequest("Nije odabrana tribina");

            try
            {

                var tribina = await Context.KopmanijeTribine.Where(b => b.Tribina.Id == idTribine && b.Kompanija.Id == IdKompanije).FirstOrDefaultAsync();

                if (tribina == null)
                    return BadRequest("Ne postoji tribina kompanije.");

                List<Odrzavanje> odrzavanja = await Context.Odrzavanja.Where(p => p.Tribina.Id == idTribine && p.Kompanija.Id == IdKompanije).ToListAsync();



                if (odrzavanja != null)
                {
                    foreach (var pr in odrzavanja)
                    {
                        List<Akreditacija> akreditacije = await Context.Akreditacije.Where(p => p.Odrzavanje.Id == pr.Id).ToListAsync();

                        if (akreditacije != null)
                            Context.Akreditacije.RemoveRange(akreditacije);
                    }
                    Context.Odrzavanja.RemoveRange(odrzavanja);

                }

                Context.KopmanijeTribine.Remove(tribina);

                await Context.SaveChangesAsync();


                return Ok("Uspesno izbrisana tribina!");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }

}