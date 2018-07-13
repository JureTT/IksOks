using System;
using System.Collections.Generic;
using System.Text;

namespace iksoks
{
    class Igrac
    {
        public string Ime;
        public Znak BiraniZnak;
        public virtual void PovuciPotez(Igra partija)
        {
            bool provjeraPoteza = false;    //staljen je false kao defaultna vrijednost kako bi se porenula while petlja   
            while (provjeraPoteza == false)
            {
                Console.WriteLine("Unesite kordinate željenog polja:");
                string kordinate = Program.SrediUnos(Console.ReadLine());     // **POZOR**: ovdje je dodano "Program." ispred metode jer se ta metoda nalazi u klasi 'Program' gdje se nalazi i 'Main()' metoda, što znači da i metoda iz glavne klase možemo pozivati u pod klasama
                kordinate = Znak.ProvjeraKota(kordinate);     //provjerava dal je prvi znak u kordinatama slovo, a drui znak broj, ako nije dodaje još znakova

                while (kordinate.Length != 2)
                {
                    kordinate = Program.KriviUnos(kordinate);     // **POZOR**: ovdje je dodano "Program." ispred metode jer se ta metoda nalazi u klasi 'Program' gdje se nalazi i 'Main()' metoda, što znači da i metoda iz glavne klase možemo pozivati u pod klasama
                    kordinate = Znak.ProvjeraKota(kordinate);     //provjerava dal je prvi znak u kordinatama slovo, a drugi znak broj, ako nije dodaje još znakova
                }

                BiraniZnak.Upisi(kordinate);    //unos kordinata u svojstvo 'Polje' objekta '.Birani Znak' (koji je svojstvo klase Igrac)
                provjeraPoteza = partija.UnosZnaka(BiraniZnak); //unos svojstva 'Polje', svojstva/objekta 'BiraniZnak' u tablicu

                if (provjeraPoteza == false)
                {
                    Console.WriteLine("Birano polje je popunjeno, molimo pokušajte ponovo.");
                }
            }
        }
        //public string provjeraZnaka(Znak simbol)  // metoda je izgleda nepotrebna
        //{
        //    string slovo = (simbol.Vrsta == "X") ? "O" : "X";
        //    return slovo;
        //}
    }
    class SlabiKomp:Igrac
    {
        public override void PovuciPotez(Igra partija)
        {
            
            Random broj = new Random();    // instanciranje objekta "broj" klase Random naredbom "Random()"(konstruktorom - metoda za stvaranje objekata/instanciranje instanici) 
            void SlucajnoPolje()
            {
                //float a = broj.Next();     //objekt "broj" generira naredbom(metodom) ".Next()" proizvoljan broj tipa/(vrste/kalupa/modle) float i dodjeljuje ga varijabli "a"
                //int b = broj.Next(10);     //objekt "broj" generira naredbom(metodom) ".Next(10)" proizvoljan broj do 10 tipa int i dodjeljuje ga varijabli "b" 
                //int c = broj.Next(11, 20);    //objekt "broj" generira naredbom(metodom) ".Next(11,20)" proizvoljan broj od 11 do 20 tipa int i dodjeljuje ga varijabli "c"

                int proizvoljno1 = broj.Next(0, 3);    // pravi random borjeve od 0 DO 3 (znači bira brojeve od sljedećih: 0, 1, 2)
                int proizvoljno2 = broj.Next(0, 3);
                string kordinate = Znak.Konverzija(proizvoljno1, proizvoljno2);
                //string[] nizApscise = new string[3] { "A", "B", "C" };
                //string apscisa = nizApscise[proizvoljno1];
                //string ordinata = (proizvoljno2 + 1).ToString();
                //string kordinate = apscisa + ordinata;
                BiraniZnak.Upisi(kordinate);
            }
            SlucajnoPolje();

            bool provjeraPoteza = partija.UnosZnaka(BiraniZnak);      //unos svojstva 'Polje', svojstva/objekta 'BiraniZnak' u tablicu
            while (provjeraPoteza == false)
            {
                SlucajnoPolje();
                provjeraPoteza = partija.UnosZnaka(BiraniZnak);
            }
        }
    }
    class NormalniKomp : SlabiKomp
    {
        public override void PovuciPotez(Igra partija)      //provjerava se tablica no ovaj put ne zbog pobjede, nego ako postoje negdje 2 ista znaka u nizu

        {
            string provjera = partija.ProvjeraTablice();
            int i = provjera.Length;
            if (i > 0)
            {
                int aps, ord;
                int kuka = provjera.IndexOf(BiraniZnak.Vrsta);
                if (kuka > 0)
                {
                    ord = int.Parse(provjera[kuka - 2].ToString());
                    aps = int.Parse(provjera[kuka     - 1].ToString());
                }
                else
                {
                    ord = int.Parse(provjera[0].ToString());      //prvi broj je os y  (kriva su mjesta kordinata, ono što bi trebalo biti na osi x je na osi y)
                    aps = int.Parse(provjera[1].ToString());      //drugi broj je os x (razlisliti o zamjeni "A B C" i "1 2 3")
                }
                string kordinate = Znak.Konverzija(aps, ord);
                BiraniZnak.Upisi(kordinate);
                bool provjeraPoteza = partija.UnosZnaka(BiraniZnak);
                
            }
            else
            {
                base.PovuciPotez(partija);
            }
        }

    }
    class JakiKomp : NormalniKomp
    {
        public override void PovuciPotez(Igra partija)
        {
            string provjera = partija.ProvjeraTablice();
            int i = provjera.Length;
            if (i > 0)
            {
                
            }
            else
            {
                base.PovuciPotez(partija);
            }
        }
    }
}
