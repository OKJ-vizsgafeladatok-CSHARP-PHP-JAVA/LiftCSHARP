using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace LiftCSHARP
{
    class Lift
    {
        public DateTime ido { get; set; }
        public int kartyaID { get; set; }
        public int indulo { get; set; }
        public int erkezo { get; set; }

        public Lift(DateTime ido, int kartyaID, int indulo, int erkezo)
        {
            this.ido = ido;
            this.kartyaID = kartyaID;
            this.indulo = indulo;
            this.erkezo = erkezo;
        }
    }
    class Program
    {
        public static List<Lift> lista = beolvas();
        public static List<Lift> beolvas()
        {
            List<Lift> list = new List<Lift>();
            try
            {
                using (StreamReader sr=new StreamReader(new FileStream("lift.txt", FileMode.Open),Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        var split = sr.ReadLine().Split(' ');
                        var o = new Lift(
                                Convert.ToDateTime(split[0]),
                                Convert.ToInt32(split[1]),
                                Convert.ToInt32(split[2]),
                                Convert.ToInt32(split[3])
                            );
                        list.Add(o);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba a beolvasásnál. "+e.Message);
            }
            return list;
        }

        static void Main(string[] args)
        {
            #region 3. feladat
            Console.WriteLine("3. feladat: Összes lifthasználat: "+lista.Count());
            #endregion

            #region 4. feladat
            Console.WriteLine("4. feladat: Időszak: "+lista.First().ido.ToString("yyyy.MM.dd")+" - "+lista.Last().ido.ToString("yyyy.MM.dd"));
            #endregion

            #region 5. feladat
            Console.WriteLine("5. feladat: Célszint max: "+lista.Max(x=>x.erkezo));
            #endregion

            #region 6-7. feladat

            try
            {
            Console.WriteLine("6. feladat: ");
                Console.Write("\tKártya száma: ");
                    var bekerK = Console.ReadLine();
                var kartya=0;
                Int32.TryParse(bekerK, out kartya);
                kartya = kartya == 0 ? 5 : kartya;
                Console.Write("\tCélszint száma: ");
                
                var bekerC = Console.ReadLine();
                int celszint=0;
                Int32.TryParse(bekerC,out celszint);
                celszint = celszint==0 ? 5 : celszint;
                var valasz = "nem ";
                var talalat = lista.Where(x => x.kartyaID == kartya && x.erkezo == celszint).Count();
                if (talalat!=0)
                {
                    valasz = "";
                }
                Console.WriteLine($"\tA(z) {kartya}. kártyával {valasz}utaztak a(z) {celszint}. emeletre!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba a bevitelkor: Kártya" + e.Message);
            }
            #endregion

            #region 8. feladat
            Console.WriteLine("8. feladat:");
            var naponkent = lista
                .GroupBy(x=>x.ido.ToString("yyyy.MM.dd"))
                .Select(y => new
                {
                    key=y.Key,
                    value=y.Count()
                })
                .ToList();
            naponkent.ForEach(x=>
                Console.WriteLine("\t"+x.key+" - "+x.value+"x")
            );
            #endregion

            Console.ReadKey();
        }
    }
}
