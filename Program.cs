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
        static readonly Random rand = new Random();
        static void Main(string[] args)
        {

           
           List<Karınca> karıncaListe=ListeKarıncaOluştur();
            List<Stack> sListe = ÇukurOluştur();
            Stack s = new Stack(10);
            sListe.Add(s);
            
            foreach (var item in sListe)
            {
                Console.WriteLine(item.);
            }

            List<Stack> tListe = TuzakOluştur();
            object[] alan = DiziAlanOluştur(sListe,tListe);
            Console.WriteLine("GİRİŞ");
            foreach (Karınca item in karıncaListe)
            {
                Console.WriteLine(item.Ad);
            }
            for (int i = 0; i < alan.Length; i++)
            {

                for (int j = 0; j < sListe.Count; j++)
                {
                    
                    if (alan[i]!=null)
                    {
                        if (alan[i].Equals(sListe[j]))
                        {
                            ÇukurDüş(karıncaListe, sListe[j].Count);
                            sListe.RemoveAt(j);

                        }
                    }
                }
                for (int z = 0; z < tListe.Count; z++)
                {
                    if (alan[i] != null)
                    {
                        if (alan[i].Equals(tListe[z]))
                        {
                            TuzakDüş(karıncaListe);
                            tListe.RemoveAt(z);
                        }
                    }
                }
                
            }
            Console.WriteLine("ÇIKIŞ");
            foreach (Karınca item in karıncaListe)
            {
                Console.WriteLine(item.Ad);
            }
            
            
           // ÇukurDüş(k,3);
            



            Console.ReadKey();

          

        }

        //Arı Oluştur metodu olacak! (Proje notasyonuna göre sadece A seçeneği için??, olmasa bile problem oluşturmaz)

        static Karınca[] DiziKarıncaOluştur()
        {
            Console.Write("Karınca Sayısını Giriniz: ");
            int karıncaSay = Int32.Parse(Console.ReadLine());

            Karınca[] karıncaDizi = new Karınca[karıncaSay];

            for (int i = 0; i < karıncaSay; i++)
            {
                Karınca k = new Karınca(nameGenerator());
                karıncaDizi[i] = k;
            }
            return karıncaDizi;
        }


        static List<Karınca> ListeKarıncaOluştur()
        {
            List<Karınca> karıncaListe = new List<Karınca>();
            
            Console.Write("Karınca Sayısını Giriniz: ");
            int karıncaSay = Int32.Parse(Console.ReadLine());
            var chars = "ABCDE";
            for (int i = 0; i < karıncaSay; i++)
            {
                Karınca k = new Karınca(chars[i].ToString());
                karıncaListe.Add(k);
            }
            return karıncaListe;
        }

        static void ÇukurDüş(List<Karınca> karıncaListe,int çukurBoyutu)
        {
            Stack çukur = new Stack(çukurBoyutu);

            Console.WriteLine("****GİRİŞ");
            foreach (var item in karıncaListe)
            {
                Console.WriteLine(item.Ad);
            }
            for (int i = 0; i < çukurBoyutu; i++)
            {
                çukur.Push(karıncaListe[0]);
                karıncaListe.RemoveAt(0);
            }
            
            for (int i = 0; i < çukurBoyutu; i++)
            {
                Karınca k =(Karınca)çukur.Pop();
                karıncaListe.Add(k);
            }
            Console.WriteLine("****ÇIKIŞ");
            foreach (var item in karıncaListe)
            {
                Console.WriteLine(item.Ad);
            }

        }

        static void TuzakDüş(List<Karınca> karıncaListe)
        {
            karıncaListe.RemoveAt(0);
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

        static object[] DiziAlanOluştur(List<Stack> çukurListe, List<Stack> tuzakListe)
        {
          

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
            var chars = "ABCDE";

            String name = null;
            for (int i = 0;i< 5; i++)
            {
                name = chars[i].ToString();
            }
           
            return name;
        }
    }
}
