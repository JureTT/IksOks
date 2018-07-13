using System;

namespace iksoks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pozdrav, dobrodošli u igru poznatu igru Iks-Oks. Vjerovatno ste svi već upoznati pravilima igre, no ne škodi ponoviti ih.\n" +
                "Pravila igre su vrlo jednostavna potrebno je povezat 3 jednaka znaka u niz/liniju, okomito, vodoravno ili dijagolanlno.\n" +
                "Igru igraju dva igrača nazimjence, stavljajući X ili O u polja (svaki igrač kosiriti uvijek isti znak ovisno o tome koji izabere na poečtku igre).\n" +
                "Ova verzija igre daje mogućnost igranja protiv kompjutera. Buduće verzije, ako ih bude, će omogućiti i osobama da igraju jedna protiv druge.\n" +
                "Tablica je dimanzija 3x3, što znači da ima 9 polja i sljedeći oblik:\n" +
                "\n" +
                "X/O  A   B   C\n" +
                " 1   _   _   _\n" +
                " 2   _   _   _\n" +
                " 3   _   _   _\n" +
                "\n" +
                "Da li želite odigrati jednu igru?");
            string odluka = SrediUnos(Console.ReadLine());

            while (odluka != "DA" && odluka != "NE")
            {
                odluka = KriviUnos(odluka);
            }

            if (odluka == "DA")
            {
               
                Console.WriteLine("Izvrsno, molimo vas upišite svoje ime:");
                string name = SrediUnos(Console.ReadLine());

                while (name == "" || name == null)
                {
                    name = KriviUnos(name);
                }

                Igrac player1 = new Igrac();
                player1.Ime = name;
                               
                //kreirati i objekt igrač1 ili player1 kako bi se kasnije moglo s njim raditi
                // ovdje dolazi upit dali želimo igrati protiv kompjutera ili čovjeka
                // sa 3 zvjezice *** ću označiti sve mjesta gdje je potrebna promjena 
                Console.WriteLine("Odlično, igrati ćete protiv kompjutera. Kompjuter može biti protivnik različitih igraćih sposobnosti.\n" +
                    "Da li želite igrati protiv: a)lakšeg, b)srednjeg ili c)jakog protivnika?(odaberite slovo za željenu razinu):");
                string razina = SrediUnos(Console.ReadLine());

                while (razina != "A" && razina != "B" && razina != "C")
                {
                    razina = KriviUnos(razina);
                }
                ////// METODA ZA PROVJERU ZNAKA ZA IGRU SA KOMPJUTEROM (privremeno ovdje)
                Igrac komp = new JakiKomp();//NormalniKomp();//SlabiKomp();   //privremeno samo jedna razina
                komp.Ime = "Računer";   // možda staviti neko ima iz sprdnje u konstruktore ( Noob, Good, Pro)

                Console.WriteLine("Odlično, odaberite znak koji želite korstiti tijekom igre, tako što ćete upisati X ili O :");
                string izbor = SrediUnos(Console.ReadLine());

                while (izbor != "X" && izbor != "O")
                {
                    izbor = KriviUnos(izbor);
                }

                //Znak simbol;    //kreiramo ovdje znak objket klase Znak, kako bi joj kasnije dodjelili podtip/podklasu Iks ili Osk

                if (izbor == "X")
                {
                    Console.WriteLine("Odlično, odabrali ste X.\n");
                    Znak simbol = new Iks();
                    player1.BiraniZnak = simbol;
                    komp.BiraniZnak = new Oks();    //***  dodano kako se nebi ponavljao uvjet provjere znaka
                }
                else
                {
                    Console.WriteLine("Odlično, odabrali ste O.\n");
                    Znak simbol = new Oks();
                    player1.BiraniZnak = simbol;
                    komp.BiraniZnak = new Iks();    //***
                }

                while (odluka == "DA")
                {
                    Igra partija = new Igra();
                    partija.PokreniPartiju();   //ovdje bi bilo vjerovatno bolje riješenje da smo pokretanje partije (odnosno nove igre) stavili u sam konstruktor

                    Console.WriteLine("Da li želite igrati prvi?");
                    string odgovor = SrediUnos(Console.ReadLine());
                    int j;
                    if (odgovor == "DA")
                    {
                        j = 0;
                    }
                    else
                    {
                        j = 1;
                    }

                    for (int i = 0; i < 9; i++)
                    {
                        if (i % 2 == j)
                        {
                            Console.WriteLine("Igrač " + player1.Ime + " je na potezu.");

                            player1.PovuciPotez(partija);
                            
                            Console.WriteLine(player1.Ime + " je stavio znak " + player1.BiraniZnak.Vrsta + " na polje " + player1.BiraniZnak.Polje);
                        }
                        else
                        {
                            Console.WriteLine("Igrač " + komp.Ime + " je na potezu.");

                            komp.PovuciPotez(partija);
                 
                            Console.WriteLine(komp.Ime + " je stavio znak " + komp.BiraniZnak.Vrsta + " na polje " + komp.BiraniZnak.Polje);

                        }

                        partija.PrikazTablice();
                        //pretvoriti vrijedonst stringa "odabirZnaka" u objekt klasa Znak i Iks ili Oks)

                        string slovo = (partija.ProvjeraTablice().Length == 1) ? partija.ProvjeraTablice() : "";
                        //Console.WriteLine(slovo);             debugiranje
                        if (slovo == player1.BiraniZnak.Vrsta)
                        {
                            Console.WriteLine("Čestitamo, " + player1.Ime + " je pobijedio sa znakom " + slovo + ", a " + komp.Ime + " je pružio dobru partiju.");
                            //i = 9;
                            break;
                        }
                        else if (slovo == komp.BiraniZnak.Vrsta)
                        {
                            Console.WriteLine("Čestitamo, " + komp.Ime + " je pobijedio sa znakom " + slovo + ", a " + player1.Ime + " je pružio dobru partiju.");
                            //i = 9;
                            break;
                        }
                        //provjeriti dal je partija gotova i sko postoji pobjednik završiti petlju sa i = 9
                    }
                    
                    Console.WriteLine("Igra je završila. Da li želite odigrati još jednu partiju?");
                    odluka = SrediUnos(Console.ReadLine());

                    while (odluka != "DA" && odluka != "NE")
                    {
                        odluka = KriviUnos(odluka);
                    }

                }

                Console.WriteLine("Hvala vam na vašem vremenu. Partija je završila, pritisnite \"Enter\" za kraj.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Hvala vam na vašem vremenu. Pritisnite 'Enter' za izlazak iz igre. Lijep pozdrav.");
                Console.ReadLine();
            }
        }
        public static string KriviUnos(string ponovi)
        {
            Console.WriteLine("Netočan unos, molimo pokušajte ponovo:");
            string sadrzaj = Console.ReadLine();
            ponovi = sadrzaj.ToUpper().Trim();
            return ponovi;
        }
        public static string SrediUnos(string sadrzaj)
        {
            sadrzaj = sadrzaj.ToUpper().Trim();
            return sadrzaj;
        }
    }
}
