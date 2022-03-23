import { Kompanija } from "./Kompanija.js";
import {Tribina} from "./Tribina.js";

fetch("https://localhost:5001/Kompanija/Kompanije").then(p => {
    if (p.ok) {
        p.json().then(
            kompanije => {
                kompanije.forEach(element => {
                    var divb = document.createElement("div");
                    divb.className = "izborKompanije";
                    divb.innerHTML = element.naziv;
                    document.body.appendChild(divb);

                    divb.onclick = (ev) => {
                        let listaTribina = [];
                        element.tribine.forEach( obj => {
                            let f = new Tribina(null,obj.tribina.naziv,obj.tribina.id);
                            listaTribina.push(f);
                        });

                        var b = new Kompanija(element.id, element.naziv, listaTribina);
                        var pozadina = document.body.querySelector(".pozadina");
                        if (pozadina !== null){
                            var rod = pozadina.parentNode;
                            rod.removeChild(pozadina);
                        }
                        var izborKompanije = document.body.querySelectorAll(".izborKompanije");
                        
                        izborKompanije.forEach(element => {
                            let rod = element.parentNode;
                            rod.removeChild(element);
                        });
                        pozadina = document.createElement("div");
                        pozadina.className="pozadina";
                        document.body.appendChild(pozadina);
                        b.crtajKompaniju(pozadina);

                    };
                });
            }
        );
    }
});

