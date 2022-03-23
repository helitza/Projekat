import { Kompanija } from "./Kompanija.js";
import { Korisnik } from "./Korisnik.js";

export class Odrzavanje {
    constructor(id, imeTribine, datum, vreme, mesto, idKompanije) {
        this.id = id;
        this.imeTribine = imeTribine;
        this.vreme = vreme;
        this.datum = datum;
        this.mesto = mesto;
        this.sedista = null;
        this.idKompanije = idKompanije;

    }



    dodajUTabelu(host) {
        var tr = document.createElement("tr");
       var pod = ["imeTribine", "vreme", "datum", "mesto"];

        var td = document.createElement("td");
        td.innerHTML = this.imeTribine;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = this.datum;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = this.vreme;
        tr.appendChild(td);

        td = document.createElement("td");
        td.innerHTML = this.mesto;
        tr.appendChild(td);




        host.appendChild(tr);
    }

    prikaziMesto(host) {

        
        if (this.imeTribine === "") {
            alert("Nije unet naziv tribine");
            return;
        }
        if (this.datum === "") {
            alert("Nije unet datum odrzavanja");
            return;
        }
        if (this.vreme === "") {
            alert("Nije uneto vreme odrzavanja");
            return;
        }

        fetch("https://localhost:5001/Mesto/Mesto/" + this.id, { method: "GET" }).then(
            p => {
                if (p.ok) {
                    p.json().then(
                        res => {
                          
                            let m = res[0].brRedova;
                            let n = res[0].brSedistaPoRedu;
                            let akreditacije = res[0].akreditacije;
                            this.sedista = Array(m).fill().map(() => Array(n).fill(0));
                        
                            karte.forEach(odg => {
                                
                                var i = odg.sediste.brReda - 1;
                                var j = odg.sediste.brSedistaURedu - 1;



                                var tmp = this.sedista[i];
                                tmp[j] = 1;
                                this.sedista[i] = tmp;
                            });
                            this.crtajMesto(host);
 
                        })
                }
                else
                    throw p;
            }).catch(err => err.text().then(errMsg => alert(errMsg)));


    }
    crtajMesto(host) {
        var mesto = host.querySelector(".Mesto");
        
        if (mesto !== null) {
            var roditelj = mesto.parentNode;
            roditelj.removeChild(mesto);
        }

        mesto = document.createElement("div");
        mesto.className = "Mesto";
        host.appendChild(mesto);
        var red;
        var polje;

        var labela;



        for (let i = 0; i < this.sedista.length; i++) {

            var tmp = this.sedista[i];
            labela = document.createElement("label");
            labela.innerHTML = i + 1;
            labela.className = "LabelaSedista";
            red = document.createElement("div");
            red.className = "RedUSali";
            red.appendChild(labela);
            for (let j = 0; j < tmp.length; j++) {

                polje = document.createElement("div");
                polje.ondblclick = (ev) => {
                   

                    this.updateMesto(i, j, host);
                }

                if (tmp[j] === 1) {
                    polje.className = "ZauzetoSediste";
                    polje.innerHTML = "z";

                }
                else if (tmp[j] == 2) {
                    polje.className = "RezervisanoSediste";
                    polje.innerHTML = "r";
                    polje.text = (i + 1) + " " + (j + 1);
                }
                else {

                    polje.className = "SlobodnoSediste";
                    polje.innerHTML = "s";

                }


                red.appendChild(polje);

            }
            sala.appendChild(red);
        }


        red = document.createElement("div");
        red.className = "RedUSali";

        for (let j = 0; j < tmp.length + 1; j++) {
            labela = document.createElement("label");
            if (j > 0)
                labela.innerHTML = j;
            else
                labela.innerHTML = "";
            labela.className = "LabelaSedista";
            red.appendChild(labela);
        }
        mesto.appendChild(red);
        

    }
    updateSalu(i, j, host) {
        
        var tmp = this.sedista[i];
        if (tmp[j]==1)
        {
            alert("Izabrano mesto je zauzeto");
            return;
        }
        tmp[j] = 2;
        this.sedista[i] = tmp;

        this.crtajMesto(host);
    }

}


