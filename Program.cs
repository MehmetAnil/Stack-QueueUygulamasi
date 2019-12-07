using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje2A2
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {

            object[] alan=DiziAlanOluştur();

            foreach (var item in alan)
            {
                Console.WriteLine(item);
            }


            Console.ReadKey();

          

        }

        //Arı Oluştur metodu olacak! (Proje notasyonuna göre sadece A seçeneği için??, olmasa bile problem oluşturmaz)
        static List<Karınca> ListeKarıncaOluştur()
        {
            List<Karınca> karıncaListe = new List<Karınca>();
            
            Console.Write("Karınca Sayısını Giriniz: ");
            int karıncaSay = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < karıncaSay; i++)
            {
                Karınca k = new Karınca(nameGenerator());
                karıncaListe.Add(k);
            }
            return karıncaListe;
        }


        
        //ÖNEMLİ!!!!
        //C seçeneğinde yapılacak hesaplama dolayısıyla, Çukur ve Tuzak metodları sayı ve boyut değerlerini Paramtre olarak alsa daha iyi olabilir.
        
        static List<Stack> ÇukurOluştur()       
        {
            List<Stack> çukurListe = new List<Stack>();

            Console.Write("Oluşturmak istediğiniz Çukur Sayısını Giriniz: ");
            int çukurSay = Int32.Parse(Console.ReadLine());
                                                            //Toplam Karınca sayısından fazla boyutta bir çukur programın hata vermesine neden olur.
            for (int i = 0; i < çukurSay; i++)              //Çukur Karınca tipinde tanımlanabilirdi, ama arılar devreye girdiğinde sorun oluşabilir...
            {
                Console.Write("{0}. Çukurun Boyutunu Giriniz: ",i+1);
                int çukurBoyut = Int32.Parse(Console.ReadLine());
                Stack çukur = new Stack(çukurBoyut);

                çukurListe.Add(çukur);
            }

            return çukurListe;
        }

        static List<Stack> TuzakOluştur() //A seçeneği için karıncaların tuzaktan çıkışı yok, ama Can Değerleri devreye girdiğinde Tuzakların Stack olması şart.
        {
            List<Stack> tuzakListe = new List<Stack>();
            Console.Write("Oluşturmak istediğiniz Tuzak Sayısını Giriniz: ");
            int tuzakSay = Int32.Parse(Console.ReadLine());
            
            for (int i = 0; i < tuzakSay; i++)          
            {
                Stack çukur = new Stack(1);

                tuzakListe.Add(çukur);
            }
            
            return tuzakListe;
        }

        static object[] DiziAlanOluştur()
        {
            
            
            List<Stack> çukurListe = ÇukurOluştur();
            List<Stack> tuzakListe = TuzakOluştur();

            Console.WriteLine("\n Alanın Boyutunu Giriniz: ");  //Alanın Boyutu Çukur+Tuzak Toplamından Küçük Olmamalı!!
            int alanBoyut = Int32.Parse(Console.ReadLine());
            
            object[] alan = new object[alanBoyut];

            for (int i = 0; i <çukurListe.Count; i++)
            {
                int çukurKonum = rand.Next(1,alanBoyut);
                if (alan[çukurKonum] == null)
                {
                    alan[çukurKonum] = çukurListe[i];
                }
                else
                {
                    i--;
                }
            }
                                                          //Tek fora'a düşürülebilir, şimdilik işlevsel.          
            for (int i = 0; i < tuzakListe.Count; i++)
            {
                int tuzakKonum = rand.Next(1,alanBoyut);
                if (alan[tuzakKonum] == null)
                {
                    alan[tuzakKonum] = tuzakListe[i];
                }
                else
                {
                    i--;
                }
            }
            return alan;
        }
 
        static String nameGenerator()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[1];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[rand.Next(chars.Length)];
            }
            String name = new String(stringChars);
            return name;
        }
    }
}
