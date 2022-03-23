import { Kompanija } from "./Kompanija.js";

export class Korisnik {
    constructor(kompanija) {
        this.ime = null;
        this.prezime = null;
        this.email = null;
        this.sifra = null;
        this.admin = false;
        this.kont = null;
        this.kompanija = kompanija;
    }
    crtajKorisnika(host) {
        let pom = this.kompanija.kont.querySelector(".AdminForma");
       
        if (pom !== null){
            let rod= pom.parentNode;
            rod.removeChild(pom);
        }
      
        var korisnikForma = host.querySelector(".KorisnikForma");
        if (korisnikForma !== null) {
            var rod = korisnikForma.parentNode;
            rod.removeChild(korisnikForma);
        }

        korisnikForma = document.createElement("div");
        korisnikForma.className = "KorisnikForma";
        this.kont = korisnikForma;
        host.appendChild(korisnikForma);





        var pod = ["Ime", "Prezime"];



        var red = document.createElement("div");
        red.className = "KorisnikRed";

        var labela = document.createElement("label");
        labela.className = "KorisnikLabela"



        pod.forEach(p => {
            red = document.createElement("div");
            red.className = "KorisnikRed";
            labela = document.createElement("label");
            labela.innerHTML = p + ":";
            labela.className = "KorisnikLabela"
            let tb = document.createElement("input");

            tb.setAttribute("type", "text");
            tb.className = "KorisnikTextBox" + p;

            red.appendChild(labela);
            red.appendChild(tb);
            korisnikForma.appendChild(red);

        });



        this.crtajPolja(korisnikForma);





        var btnRegistrujSe = document.createElement("button");
        btnRegistrujSe.onclick = (ev) => this.registracija(this.kont);
        btnRegistrujSe.innerHTML = "OK";

        red = document.createElement("div");
        red.className = "KorisnikDugmici";

        red.appendChild(btnRegistrujSe);
        korisnikForma.appendChild(red);
    }
    crtajPolja(host) {



        var pod = ["email", "sifra"];



        var red = document.createElement("div");
        red.className = "KorisnikRed";

        var labela = document.createElement("label");
        labela.className = "KorisnikLabela"



        pod.forEach(p => {
            red = document.createElement("div");
            red.className = "KorisnikRed";
            labela = document.createElement("label");
            labela.innerHTML = p + ":";
            labela.className = "KorisnikLabela"
            let tb = document.createElement("input");

            if (p === "sifra")
                tb.setAttribute("type", "password");
            else
                tb.setAttribute("type", "text");
            tb.className = "KorisnikTextBox" + p;
            red.appendChild(labela);
            red.appendChild(tb);
            host.appendChild(red);

        });



    }
    crtajKorisnikaSkraceno(host) {
        var korisnikForma = host.querySelector(".KorisnikForma");
        if (korisnikForma !== null) {
            var rod = korisnikForma.parentNode;
            rod.removeChild(korisnikForma);
        }

        korisnikForma = document.createElement("div");
        korisnikForma.className = "KorisnikForma";
        this.kont = korisnikForma;
        host.appendChild(korisnikForma);

        this.crtajPolja(korisnikForma);

        red = document.createElement("div");
        var cb = document.createElement("input");
        cb.type = "checkbox";
        cb.className = "KorisnikAdmin";
        var l = document.createElement("label");
        l.innerHTML = "Admin:";
        red.appendChild(l);
        red.appendChild(cb);
        korisnikForma.appendChild(red);

        var btnUlogujSe = document.createElement("button");
        btnUlogujSe.onclick = (ev) => this.logovanje(this.kont);
        btnUlogujSe.innerHTML = "OK";



        var red = document.createElement("div");
        red.className = "KorisnikDugmici";
        red.appendChild(btnUlogujSe);

        korisnikForma.appendChild(red);

    }
    proveriKorisnika(host) {
        if (host.querySelector(".KorisnikTextBoxemail").value !== "")
            this.email = host.querySelector(".KorisnikTextBoxemail").value;
        else {
            alert("Nije uneto korisnicko ime");
            return;
        }
        if (host.querySelector(".KorisnikTextBoxsifra").value !== "")
            this.sifra = host.querySelector(".KorisnikTextBoxsifra").value;
        else {
            alert("Nije uneta sifra");
            return;
        }



    }
    kreirajKorisnika(host) {

        if (host.querySelector(".KorisnikTextBoxIme").value !== "")
            this.ime = host.querySelector(".KorisnikTextBoxIme").value;
        else {
            alert("Nije uneto ime");
            return;
        }

        if (host.querySelector(".KorisnikTextBoxPrezime").value !== "")
            this.prezime = host.querySelector(".KorisnikTextBoxPrezime").value;
        else {
            alert("Nije uneto prezime");
            return;
        }

        this.proveriKorisnika(host);


    }
    logovanje(host) {

        this.proveriKorisnika(host);
        var cb = host.querySelector(".KorisnikAdmin");
        this.admin = cb.checked;
        if (this.email === "") {
            alert("Niste uneli korisnicko ime");
            return;
        }
        if (this.sifra === "") {
            alert("Niste uneli sifru");
            return;
        }

        fetch("https://localhost:5001/Koristnik/RegistrujSe/" + this.email + "/" + this.sifra + "/" + this.admin)
            .then(s => {
                if (s.ok) {

                    if (this.admin) {
                        let host = this.kompanija.kont.querySelector(".GlavniKontejner");
                        this.kompanija.crtajAdminFormu(host);
                    }
                    alert("uspesno ste se ulogovali");
                    s.json().then(
                        data => {
                            this.ime = data.ime;
                            this.prezime = data.prezime;
                        }
                    )
                    let rod = host.parentNode;
                    rod.removeChild(host);
                }
                else {
                    let host = this.kompanija.kont.querySelector(".GlavniKontejner");
                    this.kompanija.removeAdminFormu(host);
                    throw s;

                }
            }).catch(err =>
                err.text().then(errMsg =>
                    alert(errMsg)));

    }
    registracija(host) {

        this.kreirajKorisnika(host);
        this.admin = false;
          fetch("https://localhost:5001/Koristnik/RegistrujSe/" + this.ime + "/" + this.prezime + "/" + this.email + "/" + this.sifra + "/" + this.admin,
            {


                method: "POST",


                headers: {
                    "Content-type": "application/json; charset=UTF-8"
                }
            }).then(s => {
                let host = this.kompanija.kont.querySelector(".KorisnikForma");
                if (host !== null){
                    let rod= host.parentNode;
                    rod.removeChild(host);
                }
                

                if (s.ok) {


                    alert("uspesno ste se registrovali");


                }
                else {

                    throw s;

                }
            }).catch(err =>
                err.text().then(errMsg =>
                    alert(errMsg)));


    }


}