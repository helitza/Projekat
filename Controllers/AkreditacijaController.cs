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
    public class AkreditacijaController : ControllerBase
    {
        public KompanijaContext Context { get; set; }

        public AkreditacijaController(KompanijaContext context)
        {
            Context = context;
        }

        [Route("KupiAkreditaciju/{idOdrzavanja}/{Red}/{BrojURedu}/{korisnickoIme}")]
        [HttpPost]
        public async Task<ActionResult> DodajAkreditaciju(int idOdrzavanja, int Red, int BrojURedu, string korisnickoIme)
        {

            if (idOdrzavanja < 0)
            {
                return BadRequest("Nije uneta kompanija");

            }

            if (Red == 0)
            {
                return BadRequest("Zaboravili ste da unesete red");
            }
            if (BrojURedu == 0)
            {
                return BadRequest("Zaboravili ste da unesete broj sedista");
            }
            if (string.IsNullOrEmpty(korisnickoIme))
            {
                return BadRequest("Nema email!");

            }


            try
            {


                
                var odrzavanje = await Context.Odrzavanja.Where(p => p.Id == idOdrzavanja).Include(p=>p.Tribina).FirstOrDefaultAsync();

                if (odrzavanje == null)
                    return BadRequest("Odrzavanje ne postoji");

                double kotizacija = odrzavanje.Tribina.Kotizacija;

                Korisnik k;

                k = await Context.Korisnici.Where(p => p.email == korisnickoIme).FirstOrDefaultAsync();

                if (k == null)
                {
                    return BadRequest("Ne postoji korisnik sa korisnickim imenom");
                }

                Akreditacija akreditacija = new Akreditacija
                {
                    Odrzavanje = odrzavanje,
                    sediste = new Sediste
                    {
                        mesto = odrzavanje.mesto,
                        BrReda = Red,
                        BrSedistaURedu = BrojURedu

                    },
                    korisnik = k

                };

                Context.Akreditacije.Add(akreditacija);

                await Context.SaveChangesAsync();

                return Ok($"Rezervisana je akreditacija za tribinu {odrzavanje.Tribina.naziv}, sediste u redu {Red},broj mesta {BrojURedu}, po ceni {kotizacija}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}