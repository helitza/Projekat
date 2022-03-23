import { Korisnik } from "./Korisnik.js";
import { Odrzavanje } from "./Odrzavanje.js";

export class Kompanija {

    constructor(id, naziv, tribine) {
        this.id = id;
        this.naziv = naziv;
        this.kont = null;
        this.tribine = tribine;
        this.listaOdrzavanje = null;
    }

    crtajKompaniju(host) {

        this.kont = host;

        var red = document.createElement("div");
        red.innerHTML = this.naziv;
        red.className = "Naziv";
        this.kont.appendChild(red);
        let glavniDeo = document.createElement("div");
        glavniDeo.className = "GlavniKontejner";

        var korisnik = new Korisnik(this);

        var red2 = document.createElement("div");
        red2.className = "KorisnikZaglavlje";
        var labela = document.createElement("label");
        labela.className = "KorisnikZaglavljeLabela";
        labela.innerHTML = "Uloguj se";
        red2.appendChild(labela);

        labela.onclick = (ev) => korisnik.crtajKorisnikaSkraceno(glavniDeo);
        labela = document.createElement("label");
        labela.className = "KorisnikZaglavljeLabela";
        labela.innerHTML = "Registruj se";
        red2.appendChild(labela);
        this.kont.appendChild(red2);
        labela.onclick = (ev) => korisnik.crtajKorisnika(glavniDeo);
        this.kont.appendChild(glavniDeo);

        this.prikaziTribine(glavniDeo);

        this.crtajTribinaFormu(glavniDeo);
        this.crtajAkreditaciju(glavniDeo, korisnik);

    }

    napraviTribinaTabelu(host) {
        var teloTabele = host.querySelector(".TabelaTribinaPodaci");

        if (teloTabele !== null) {
            var roditelj = teloTabele.parentNode;
            roditelj.removeChild(teloTabele);
        }


        teloTabele = document.createElement("tbody");
        teloTabele.className = "TabelaFilmPodaci";
        host.appendChild(teloTabele);

        return teloTabele;
    }

    dodajUTabelu(tabela, el) {
        var tr = document.createElement("tr");
        tr.className = "tribinaTabela";

        var td = document.createElement("td");
        td.className = "tribinaTabela";
        td.innerHTML = el;
        tr.appendChild(td);
        tabela.appendChild(tr);
    }
    prikaziTribine(host) {
        let rasporedForma = document.createElement("div");
        host.appendChild(rasporedForma);
        rasporedForma.className = "TribinaRaspored";
        let naslov = document.createElement("div");
        naslov.className = "naslovRasporeda";
        naslov.innerHTML = "Raspored"
        rasporedForma.appendChild(naslov);
        let el = document.createElement("div");
        el.className = "TableRow";
        rasporedForma.appendChild(el);
        let tabela = this.napraviTribinaTabelu(el);
        this.tribine.forEach(el => {
            this.dodajUTabelu(tabela, el.naziv);
        });
    }

    crtajTribinaFormu(host) {
        let tribinaForma = document.createElement("div");
        tribinaForma.className = "TribinaForma";
        host.appendChild(tribinaForma);



        var red = document.createElement("div");
        red.className = "TribinaZaglavlje";
        var labela = document.createElement("label");
        labela.innerHTML = "Naziv tribine: ";
        red.appendChild(labela);

        var se = document.createElement("select");
        se.className = "comboBox";

        this.tribine.forEach((p, i) => {
            var op = document.createElement("option");
            op.innerHTML = p.naziv;
            op.value = p.id;
            se.appendChild(op);
        })

        red.appendChild(se);


        var btnPretrazi = document.createElement("button");
        btnPretrazi.onclick = (ev) => this.pretrazi(tribinaForma);
        btnPretrazi.innerHTML = "Pretrazi";
        red.appendChild(btnPretrazi);
        btnPretrazi.className = "PretraziDugme";
        tribinaForma.appendChild(red);



    }
    pretrazi(host) {
        let tribina = host.querySelector(".comboBox");
        let idTribine = tribina.options[film.selectedIndex].value;

        var url = "https://localhost:5001/Tribina/Odrzavanje/" + tribina.value + "/" + this.id;

        fetch(url, {
            method: "GET",

            headers: {
                'Content-Type': 'application/json'
            }

        }).then(s => {
            if (s.ok) {

                s.json().then(data => {
                    var tabela = this.napraviTabelu(host);

                    data.forEach(element => {
                       
                        var vreme = element.vreme.split("T");
                        var p = new Odrzavanje(element.id, element.tribina.naziv, vreme[0], vreme[1].substring(0, 5), element.mesto.naziv, this.id);

                        p.dodajUTabelu(tabela);

                    });
                })
            }
        })

    }
    napraviTabelu(host) {
        var teloTabele = host.querySelector(".TabelaPodaci");

        if (teloTabele !== null) {
            var roditelj = teloTabele.parentNode;
            roditelj.removeChild(teloTabele);
        }


        teloTabele = document.createElement("tbody");
        teloTabele.className = "TabelaPodaci";
        host.appendChild(teloTabele);





        var tr = document.createElement("tr");
        var zag = ["TRIBINA", "DATUM", "VREME", "MESTO"];
        zag.forEach(el => {
            var th = document.createElement("th");
            th.innerHTML = el;
            tr.appendChild(th);
        })

        teloTabele.appendChild(tr);



        return teloTabele;
    }
    crtajAkreditaciju(host, korisnik) {
        var Akreditacija = document.createElement("div");
        Akreditacija.className = "Akreditacija";
        host.appendChild(Akreditacija);

        var red0 = document.createElement("div");
        red0.className = "KartaRed0";
        var labela = document.createElement("labela");
        labela.innerHTML = "brza rezervacija";
        red0.appendChild(labela);
        Karta.appendChild(red0);


        var red = document.createElement("div");
        red.className = "AkreditacijaRed1";
        var labela = document.createElement("labela");
        labela.innerHTML = "Naziv tribine:";
        red.appendChild(labela);

        var se = document.createElement("select");
        se.className = "comboBox2";
        red.appendChild(se);


        Karta.appendChild(red);

        var op = document.createElement("option");
        op.innerHTML = "Unesite naziv tribine";
        op.value = "";
        se.appendChild(op);


        this.tribine.forEach(p => {
            op = document.createElement("option");
            op.innerHTML = p.naziv;
            op.value = p.id;
            se.appendChild(op);
        })
        red = document.createElement("div");
        red.className = "AkreditacijaRed2";
        labela = document.createElement("labela");
        labela.innerHTML = "Datum odrzavanja:";
        var se3 = document.createElement("select");
        se3.className = "comboBox3";
        var op = document.createElement("option");
        op.innerHTML = "Unesite datum odrzavanja";
        op.value = 0;
        se3.appendChild(op);
        red.appendChild(labela);
        red.appendChild(se3);
        Akreditacija.appendChild(red);

        var red2 = document.createElement("div");
        red2.className = "AkreditacijaRed3";
        labela = document.createElement("labela");
        labela.innerHTML = "Vreme odrzavanja:";
        var se4 = document.createElement("select");
        se4.className = "comboBox4";
        op = document.createElement("option");
        op.innerHTML = "Unesite vreme odrzavanja";
        op.value = 0;
        se4.appendChild(op);
        red2.appendChild(labela);
        red2.appendChild(se4);

        Akreditacija.appendChild(red2);

        se.onchange = (ev) => {
            this.obrisi(Akreditacija);
            let tribina = host.querySelector(".comboBox2");

            if (tribina.selectedIndex === 0) {
                alert("Nije unet naziv tribine");
                return;
            }
            se3.value = 0;

            se4.value = 0;

            let idTribine = tribina.options[tribina.selectedIndex].value;

            var url = "https://localhost:5001/Tribina/Odrzavanja/" + idTribine + "/" + this.id;

            fetch(url, {
                method: "GET",

                headers: {
                    'Content-Type': 'application/json'
                }

            }).then(s => {
                if (s.ok) {

                    s.json().then(data => {
                        var listaP = [];
                        data.forEach(element => {


                            let vreme = element.vreme.split("T");

                            listaP.push(new Odrzavanje(element.id, null, vreme[0], vreme[1].substring(0, 5), null, this.id));


                        });

                        let n = se3.length;
                        for (let i = 1; i < n; i++) {
                            se3.removeChild(se3.options[1]);

                        }

                        var listaDatuma = [];
                        listaP.forEach(data => {

                            if (listaDatuma.indexOf(data.datum) === -1) {
                                listaDatuma.push(data.datum);
                                var op = document.createElement("option");
                                op.innerHTML = data.datum;

                                se3.appendChild(op);
                            }
                        });

                        red.appendChild(se3);



                        se3.onchange = (ev) => {
                            this.obrisi(Akreditacija);
                            se4.selectedIndex = 0;
                            let n = se4.length;
                            for (let i = 1; i < n; i++) {
                                se4.removeChild(se4.options[1]);

                            }

                            var listaTermina = ["Unesite vreme odrzavanja"];

                            let datum = host.querySelector(".comboBox3");
                            var vrednostDatume = datum.options[datum.selectedIndex].innerHTML;
                            for (let i = 1; i < se4.length; i++) {
                                se4.remove(i);
                            }
                            listaP.forEach(el => {

                                if (el.datum === vrednostDatume && listaTermina.indexOf(el.vreme) === -1) {

                                    var op = document.createElement("option");
                                    op.innerHTML = el.vreme;
                                    op.value = el.id;
                                    se4.appendChild(op);


                                }
                            })




                            red2.appendChild(se4);
                            se4.onchange = (ev) => {
                                this.obrisi(Akreditacija);
                                var datum = host.querySelector(".comboBox4");

                                let idProj = datum.options[datum.selectedIndex].value;

                                let p;
                                listaP.forEach(pa => {
                                    if (pa.id === parseInt(idProj))
                                        p = pa;

                                })

                                p.prikaziMesto(Akreditacija);

                                var kupiDugme = host.querySelector(".KupiDugme");
                                if (kupiDugme !== null) {
                                    var rod = kupiDugme.parentNode;
                                    rod.removeChild(kupiDugme);
                                }
                                var red3 = document.createElement("div");
                                red3.className = "KupiDugme";
                                var btnKupiAkreditaciju = document.createElement("button");
                                btnKupiAkreditaciju.className = "KupiAkreditacijuDugme"
                                btnKupiAkreditaciju.innerHTML = "kupi akreditaciju";
                                btnKupiAkreditaciju.onclick = (ev) => this.btnKupiAkreditaciju(korisnik, Akreditacija, p);
                                red3.appendChild(btnKupiAkreditaciju);
                                Akreditacija.appendChild(red3);

                            }
                        };







                    })
                }
            })

        };







    }
    obrisi(host) {
        var mestoForma = host.querySelector(".Mesto");
        if (mestoForma !== null) {
            var rod = mestoForma.parentNode;
            rod.removeChild(mestoForma);
        }
        var kupiDugme = host.querySelector(".KupiDugme");
        if (kupiDugme !== null) {
            var rod = kupiDugme.parentNode;
            rod.removeChild(kupiDugme);
        }

    }
    btnKupiAkreditaciju(korisnik, host, p) {
        var tribina = host.querySelector(".comboBox2");
        if (tribina.selectedIndex === 0) {
            alert("Nije izabrana tribina");
            return;
        }
        var nazivTribine = tribina.options[tribina.selectedIndex].value;
        var datum = host.querySelector(".comboBox3");

        if (datum.selectedIndex === 0) {
            alert("Nije izabran datum odrzavanja");
            return;
        }
        var nazivDatuma = datum.options[datum.selectedIndex].value;
        var vreme = host.querySelector(".comboBox4");
        if (vreme.selectedIndex === 0) {
            alert("Nije izabrano vreme odrzavanja");
            return;

        }
        var nazivVremena = vreme.options[vreme.selectedIndex].value;

        if (korisnik.email === null) {
            alert("Nije prijavljen korisnik");
            return;
        }
        let sedista = p.sedista;

        for (let i = 0; i < sedista.length; i++) {

            var tmp = sedista[i];

            for (let j = 0; j < tmp.length; j++) {


                if (tmp[j] === 2) {

                    let red = i + 1;
                    let brURedu = j + 1;
                    fetch("https://localhost:5001/Akreditacija/KupiAkreditaciju/" + p.id + "/" + red + "/"
                        + brURedu + "/" + korisnik.email, { method: "POST" }).then(
                            s => {
                                if (s.ok) {
                                    s.text().then(
                                        t => {

                                            p.prikaziMesto(host);
                                            alert(t);
                                        }
                                    )
                                }
                                else
                                    throw s;

                            }).catch(err => err.text().then(errMsg => alert(errMsg)));

                }


            }
        }






    }
    removeAdminFormu(host) {
        var adminForma = host.querySelector(".AdminForma");

        if (adminForma !== null) {
            let rod = adminForma.parentNode;
            rod.removeChild(adminForma);
        }
    }
    crtajAdminFormu(host) {

        this.removeAdminFormu(host);

        var adminForma = document.createElement("div");
        adminForma.className = "AdminForma";
        host.appendChild(adminForma);



        var red = document.createElement("div");
        labela = document.createElement("label");
        labela.innerHTML = "Naziv tribine:";
        red.appendChild(labela);




        var se = document.createElement("select");
        se.className = "comboBox5";

        red.appendChild(se);
        var op = document.createElement("option");
        op.innerHTML = "Uneti naziv tribine";
        op.value = 0;
        se.appendChild(op);
        this.tribine.forEach(p => {
            op = document.createElement("option");
            op.innerHTML = p.naziv;
            op.value = p.id;
            se.appendChild(op);
        })

        red.className = "AdminRedovi";
        adminForma.appendChild(red);//dodat prvi

        red = document.createElement("div");
        var labela = document.createElement("label");
        labela.innerHTML = "Datum odrzavanja:";
        var se3 = document.createElement("select");
        se3.className = "comboBox6";
        var op = document.createElement("option");
        op.innerHTML = "Unesite datum odrzavanja";
        op.value = 0;
        se3.appendChild(op);
        red.appendChild(labela);
        red.appendChild(se3);
        red.className = "AdminRedovi";
        adminForma.appendChild(red);//dodat drugi

        var red2 = document.createElement("div");
        red2.className = "AdminRedovi";
        labela = document.createElement("label");
        labela.innerHTML = "Vreme odrzavanja:";
        var se4 = document.createElement("select");
        se4.className = "comboBox7";
        op = document.createElement("option");
        op.innerHTML = "Unesite vreme odrzavanja";
        op.value = 0;
        se4.appendChild(op);
        red2.appendChild(labela);
        red2.appendChild(se4);
        adminForma.appendChild(red2);//dodat 3.
        this.dodajUnos(adminForma);

        se.onchange = (ev) => {

            let tribina = host.querySelector(".comboBox5");

            if (tribina.selectedIndex === 0) {
                alert("Nije unet naziv tribine");
                return;
            }


            let idTribine = tribina.options[tribina.selectedIndex].value;

            var url = "https://localhost:5001/Tribina/Odrzavanja/" + idTribine + "/" + this.id;

            fetch(url, {
                method: "GET",

                headers: {
                    'Content-Type': 'application/json'
                }

            }).then(s => {
                if (s.ok) {

                    s.json().then(data => {
                        var listaP = [];
                        data.forEach(element => {

                            let vreme = element.vreme.split("T");

                            listaP.push(new Odrzavanje(element.id, null, vreme[0], vreme[1].substring(0, 5), null, this.id));


                        });

                        let n = se3.length;
                        for (let i = 1; i < n; i++) {
                            se3.removeChild(se3.options[1]);

                        }

                        var listaDatuma = [];
                        listaP.forEach(data => {

                            if (listaDatuma.indexOf(data.datum) === -1) {
                                listaDatuma.push(data.datum);
                                var op = document.createElement("option");
                                op.innerHTML = data.datum;

                                se3.appendChild(op);
                            }
                        });

                        red.appendChild(se3);



                        se3.onchange = (ev) => {

                            se4.selectedIndex = 0;
                            let n = se4.length;
                            for (let i = 1; i < n; i++) {
                                se4.removeChild(se4.options[1]);

                            }

                            var listaTermina = ["Unesite vreme odrzavanja"];

                            let datum = host.querySelector(".comboBox6");
                            var vrednostDatume = datum.options[datum.selectedIndex].innerHTML;

                            for (let i = 1; i < se4.length; i++) {
                                se4.remove(i);
                            }

                            listaP.forEach(el => {

                                if (el.datum === vrednostDatume && listaTermina.indexOf(el.vreme) === -1) {
                                    var op = document.createElement("option");
                                    op.innerHTML = el.vreme;
                                    op.value = el.id;

                                    se4.appendChild(op);


                                }
                            })


                            red2.appendChild(se4);
                          


                        };







                    })
                }
            })


        };


    }
    dodajUnos(host) {

        var red = host.querySelector(".AdminRedovitb");
        while (red !== null) {
            var rod = red.parentNode;
            rod.removeChild(red);
            red = host.querySelector(".AdminRedovitb");
        }


        red = document.createElement("div");
        var labela = document.createElement("label");
        labela.innerHTML = "Novi datum odrzavanja:";
        red.appendChild(labela);
        var tb = document.createElement("input");
        tb.setAttribute("type", "text");

        tb.className = "tbNDatumProj";
        red.appendChild(tb);
        red.className = "AdminRedovi";
        host.appendChild(red);

        red = document.createElement("div");
        labela = document.createElement("label");
        labela.innerHTML = "Novo vreme odrzavanja:";
        red.appendChild(labela);
        tb = document.createElement("input");
        tb.setAttribute("type", "text");

        tb.className = "tbNVremeOdrz";
        red.appendChild(tb);
        red.className = "AdminRedovi";

        host.appendChild(red);

        red = document.createElement("div");
        var btnPromeni = document.createElement("button");
        btnPromeni.onclick = (ev) => this.promeni(host);
        btnPromeni.className = "AdminDugmePromeni";
        btnPromeni.innerHTML = "Promeni";
        red.className = "AdminDugmici";
        red.appendChild(btnPromeni);
        host.appendChild(red);


        var btnIzbrisi = document.createElement("button");
        btnIzbrisi.onclick = (ev) => this.izbrisi(host);
        btnIzbrisi.className = "AdminDugmeBrisi";
        btnIzbrisi.innerHTML = "Izbrisi";
        red.className = "AdminDugmici";
        red.appendChild(btnIzbrisi);
        host.appendChild(red);


    }

    promeni(host) {
        var tribina = host.querySelector(".comboBox5");
        var idTribine = tribina.options[tribina.selectedIndex].value;


        if (tribina.selectedIndex === 0) {
            alert("Nije izabrana tribina");
            return;
        }
        var datum = host.querySelector(".comboBox6");

       
        if (datum.selectedIndex === 0) {
            alert("Nije izabran datum odrzavanja");
            return;
        }

        var vreme = host.querySelector(".comboBox7");
        let idProj = vreme.options[vreme.selectedIndex].value;
     
        if (vreme.selectedIndex === 0) {
            alert("Nije izabrano vreme odrzavanja");
            return;

        }

        var datum2 = host.querySelector(".tbNDatumOdrz").value;

        if (datum2 === "") {
            alert("Nije unet datum odrzavanja");
            return;
        }
        var vreme = datum2 + " " + host.querySelector(".tbNVremeOdrz").value;
        if (vreme === "") {
            alert("Nije izabrano vreme odrzavanja");
            return;

        }

        fetch("https://localhost:5001/Odrzavanje/PromenitiOdrzavanje/" + idProj + "/" + vreme,
            {
                method: "PUT"
            }).then(s => {
                if (s.ok) {
                    alert("Uspesno ste izmenili odrzavanje");
                    let form = this.kont.querySelector(".TribinaForma");
                    let se = this.kont.querySelector(".comboBox");
                   
                    let opt = se.querySelectorAll("option");
                    
                    opt.forEach((p, i) => {
                       
                        if (p.value === idTribine)
                            se.selectedIndex = i;
                    });
                    this.pretrazi(form);

                }
                else {
                    throw s;
                }
            }).catch(err => {
                err.text().then(errMsg =>
                    alert(errMsg))
            });




    }
    izbrisi(host) {

        let tribina = host.querySelector(".comboBox5");
        let idTribine = tribina.options[tribina.selectedIndex].value;
        if (tribina.selectedIndex == 0) {
            alert("Nije izabrana tribina");
            return;
        }

        fetch("https://localhost:5001/Tribina/IzbrisiTribinu/" + this.id + "/" + idTribine,
            {
                method: "DELETE",
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }

            }).then(s => {
                if (s.ok) {
                    alert("Izbrisana je tribina");
                    this.tribine = this.tribine.filter(f => f.id != idTribine);

                    let rasForma = this.kont.querySelector(".TribinaRaspored");
                    let gh = this.kont.querySelector(".GlavniKontejner");
                    if (rasForma !== null) {
                        let rod = rasForma.parentNode;
                        rod.removeChild(rasForma);
                        this.prikaziTribine(gh);
                    }
                    this.updateComboBox(this.kont);

                } else
                    throw s;
            }).catch(err => {
                console.log(err);
                err.text().then(errMsg => alert(errMsg));

            });

    }
    updateComboBox(host) {
        let cb = host.querySelector(".comboBox");
        let n = cb.length;
        for (let i = 1; i < n; i++) {
            cb.removeChild(cb.options[1]);

        }
        this.tribine.forEach(p => {
            var op = document.createElement("option");
            op.innerHTML = p.naziv;
            op.value = p.id;
            cb.appendChild(op);
        })


        cb = host.querySelector(".comboBox2");
        n = cb.length;
        for (let i = 1; i < n; i++) {
            cb.removeChild(cb.options[1]);

        }
        this.tribine.forEach(p => {
            var op = document.createElement("option");
            op.innerHTML = p.naziv;
            op.value = p.id;
            cb.appendChild(op);
        })

        cb = host.querySelector(".comboBox5");
        n = cb.length;
        for (let i = 1; i < n; i++) {
            cb.removeChild(cb.options[1]);

        }
        this.tribine.forEach(p => {
            var op = document.createElement("option");
            op.innerHTML = p.naziv;
            op.value = p.id;
            cb.appendChild(op);
        })
        
    }
}