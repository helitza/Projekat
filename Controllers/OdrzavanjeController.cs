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
    public class OdrzavanjeController : ControllerBase
    {
        public KompanijaContext Context { get; set; }

        public OdrzavanjeController(KompanijaContext context)
        {
            Context = context;
        }



       

        [Route("PromenitiOdrzavanje/{idOdrzavanja}/{Datum2}")]
        [HttpPut]
        public async Task<ActionResult> PromenitiOdrzavanje(int idOdrzavanja, string Datum2)
        {

            if (string.IsNullOrEmpty(Datum2))
            {
                return BadRequest("Nije unet korigovani datum projekcije");

            }
            if (idOdrzavanja < 0)
            {
                return BadRequest("Nije unet dobar id kompanije");
            }

            try
            {
                var v2 = DateTime.ParseExact(Datum2, "yyyy-MM-dd HH:mm", null);
                
                

                var odrzavanje = await Context.Odrzavanja.Where( p => p.Id == idOdrzavanja).Include(p => p.Tribina).FirstOrDefaultAsync();
                if (odrzavanje == null)
                    return BadRequest("Ne postoji odrzavanje tribine");

                var tribina = odrzavanje.Tribina;
                if (tribina == null) {
                    return BadRequest("Tribina ne postoji");
                }
                                                   
                var vreme = Datum2.Split(" ");
                if (tribina.datumKraja.ToString("yyyy-MM-dd").CompareTo(vreme[0]) < 0 || tribina.datumPocetka.ToString("yyyy-MM-dd").CompareTo(vreme[0]) > 0)
                {
                    return BadRequest("Tribina nije dostupna tog datuma");
                }



                odrzavanje.vreme = v2;

                Context.Odrzavanja.Update(odrzavanje);

                await Context.SaveChangesAsync();
                return Ok("Uspešno izmenjeno odrzavanje tribine ");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}