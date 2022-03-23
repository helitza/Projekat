using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : ControllerBase
    {
        public KompanijaContext Context{get;set;}

        public KorisnikController(KompanijaContext context){
            Context=context;
        }

      [Route("RegistrujSe/{Ime}/{Prezime}/{email}/{sifra}/{admin}")]
      [HttpPost]
        public async Task<ActionResult> DodatiKorisnika(string Ime,string Prezime,string email,string sifra,bool admin){

            if(String.IsNullOrEmpty(Ime))
              {
                    return BadRequest("Zaboravili ste da uneste ime");
              }
            if(String.IsNullOrEmpty(Prezime))
              {
                    return BadRequest("Zaboravili ste da uneste prezime");
              }  
            if(String.IsNullOrEmpty(email))
              {
                    return BadRequest("Zaboravili ste da uneste korisnicko ime");
              }  
            if(String.IsNullOrEmpty(sifra))
              {
                    return BadRequest("Zaboravili ste da uneste sifru");
              }  
            if(sifra.Length<8)
              {
                    return BadRequest("Sifra mora imati najmanje 8 karaktera");
              }  
            var korisnik= await Context.Korisnici.Where(acc=>acc.email==email).FirstOrDefaultAsync();
            if(korisnik!=null)
                return BadRequest("Korisnik sa unetim korisnickim imenom vec postoji");   
            try{
                
              
                
                Korisnik k = new Korisnik{
                    Ime=Ime,
                    Prezime=Prezime,
                    email=email,
                    sifra=sifra,
                    admin = admin

                };
                Context.Korisnici.Add(k);
                await Context.SaveChangesAsync();
                return Ok("Kreiran je novi korisnik");

            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
            
        }
      [Route("UlogujSe/{email}/{sifra}/{admin}")]
      [HttpGet]
        public async Task<ActionResult> VratitiKorisnika(string email,string sifra,bool admin){

            if(String.IsNullOrEmpty(email))
              {
                    return BadRequest("Zaboravili ste da uneste korisnicko ime");
              }  
            if(String.IsNullOrEmpty(sifra))
              {
                    return BadRequest("Zaboravili ste da uneste sifru");
              }  
            if(email.Length<8)
              {
                    return BadRequest("Sifra mora imati minimum 8 karaktera");
              }  
          
            try{
                var korisnik=await Context.Korisnici.Where(acc=>acc.email==email).FirstOrDefaultAsync();
                if(korisnik==null)
                    return BadRequest("Korisnik sa unetim korisnickim imenom ne postoji");   
                if(korisnik.sifra!=sifra)
                    return BadRequest("Uneta je pogresna lozinka");   
                if (korisnik.admin != admin)
                      return BadRequest("Unet je pogresan pristup");   
                
                return Ok(korisnik);   
                
       

            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
         
        }


    }
}
