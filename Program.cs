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

            
            Console.Write("Oluşturmak istediğiniz Çukur Sayısını Giriniz: ");
            int çukurSay = Int32.Parse(Console.ReadLine());
            int[] çukurBoyutları = ÇukurKonumAl(çukurSay);
            List<Stack> tListe = TuzakOluştur();
            object[] alan = DiziAlanOluştur(tListe,çukurBoyutları);

           /* Karınca[] karıncaDizi = DiziKarıncaOluştur();
            for (int i = 0; i < karıncaDizi.Length; i++)
            {
                karıncaDizi[i].Can = 1;
            }
            */
            
           // ÇözümMetodu(alan,çukurDizi,karıncaDizi,tListe);

            Console.WriteLine("List versiyonu");
             List<Canlı> karıncaListe = ListeKarıncaOluştur();

            
            for (int i = 0; i < karıncaListe.Count; i++)
            {
                karıncaListe[i].Can = 5;
            }


            ÇözümMetodu(alan, çukurBoyutları, karıncaListe, tListe);


            for (int i = 0; i < karıncaListe.Count; i++)
            {
                Console.WriteLine(karıncaListe[i].Can);
            }


            //Console.WriteLine("ANA PROBLEM" + "\n***********");
            //IlkProblem();






            Console.ReadKey();



        }

        //Arı Oluştur metodu olacak! (Proje notasyonuna göre sadece A seçeneği için??, olmasa bile problem oluşturmaz)

        static void IlkProblem()
        {
            string harfler = "ABCDE";
            Karınca[] karıncaDizi = new Karınca[harfler.Length];

            for (int i = 0; i < harfler.Length; i++)
            {
                Karınca karınca = new Karınca(harfler[i].ToString());
                karıncaDizi[i] = karınca;
                karıncaDizi[i].Can = 1;
            }

            int[] çukurBoyutları = { 3, 2, 4 };
            object[] alan = new object[10];
            alan[2] = çukurBoyutları[0];
            alan[4] = çukurBoyutları[1];
            alan[8] = çukurBoyutları[2];

            Stack tuzak = new Stack();
            Stack tuzak1 = new Stack();
            List<Stack> tListe = new List<Stack>();
            tListe.Add(tuzak);

            alan[6] = tuzak;

            Console.WriteLine("----BAŞLANGIÇ NOKTASI SIRALAMA----");
            for (int i = karıncaDizi.Length - 1; 0 <= i; i--)
            {
                if (karıncaDizi[i] != null)
                    Console.Write(karıncaDizi[i].Ad + (i > 0 && karıncaDizi[i - 1] != null ? "-" : ""));
            }
            Console.WriteLine();
            ÇözümMetodu(alan, çukurBoyutları, karıncaDizi, tListe);
            Console.WriteLine();
            Console.WriteLine("----VARIŞ NOKTASI SIRALAMA----");
            for (int i = karıncaDizi.Length - 1; 0 <= i; i--)
            {
                if (karıncaDizi[i] != null)
                    Console.Write(karıncaDizi[i].Ad + (i > 0 && karıncaDizi[i - 1] != null ? "-" : ""));
            }





        }

        static void ÇözümMetodu(object[] alan, int[] çukurBoyutları, Karınca[] karıncaDizi, List<Stack> tListe)
        {
            Console.WriteLine("----BAŞLANGIÇ NOKTASI SIRALAMA----");
            for (int i = karıncaDizi.Length - 1; 0 <= i; i--)
            {
                if (karıncaDizi[i] != null)
                    Console.Write(karıncaDizi[i].Ad + (i > 0 && karıncaDizi[i - 1] != null ? "-" : ""));
            }

            for (int i = 0; i < alan.Length; i++)
            {
                for (int j = 0; j < çukurBoyutları.Length; j++)
                {

                    if (alan[i] != null && alan[i].GetType().IsValueType)
                    {
                        if (çukurBoyutları[j] != 0 && ((int)alan[i]) == çukurBoyutları[j])
                        {
                            Console.WriteLine("\n{0} boyutlu Çukur", çukurBoyutları[j]);
                            ÇukurDüş(karıncaDizi, çukurBoyutları[j]);
                            çukurBoyutları[j] = 0;

                        }
                    }
                }
                for (int z = 0; z < tListe.Count; z++)
                {
                    if (alan[i] != null)
                    {
                        if (alan[i].Equals(tListe[z]))
                        {
                            karıncaDizi=TuzakDüş(karıncaDizi);
                            tListe.RemoveAt(z);
                        }
                    }
                }

            }
            Console.WriteLine();
            Console.WriteLine("----VARIŞ NOKTASI SIRALAMA----");
            for (int i = karıncaDizi.Length - 1; 0 <= i; i--)
            {
                if (karıncaDizi[i] != null)
                    Console.Write(karıncaDizi[i].Ad + (i > 0 && karıncaDizi[i - 1] != null ? "-" : ""));
            }
           
        }
        static void ÇözümMetodu(object[] alan, int[] çukurBoyutları, List<Canlı> karıncaListe, List<Stack> tListe) //arı parametresi
        {
            Console.WriteLine("----BAŞLANGIÇ NOKTASI SIRALAMA----");
            for (int i = karıncaListe.Count - 1; 0 <= i; i--)
            {
                if (karıncaListe[i] != null)
                    Console.Write(karıncaListe[i].Ad + (i > 0 && karıncaListe[i - 1] != null ? "-" : ""));
            }

            int buluşma = alan.Length/2;

            for (int i = 0; i < alan.Length; i++)
            {
                for (int j = 0; j < çukurBoyutları.Length; j++)
                {

                    if (alan[i] != null && alan[i].GetType().IsValueType)
                    {
                        if (çukurBoyutları[j] != 0 && ((int)alan[i]) == çukurBoyutları[j])
                        {
                            Console.WriteLine("\n{0} boyutlu Çukur", çukurBoyutları[j]);
                            ÇukurDüş(karıncaListe, çukurBoyutları[j]);
                            //çukurBoyutları[j] = 0;

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



            Console.WriteLine();
            Console.WriteLine("----VARIŞ NOKTASI SIRALAMA----");
            for (int i = karıncaListe.Count - 1; 0 <= i; i--)
            {
                    Console.Write(karıncaListe[i].Ad + (i > 0 && karıncaListe[i - 1] != null ? "-" : ""));
            }

        }

        static void ÇözümMetoduKalan(List<Karınca> kListe,List<Arı> aListe,int buluşma)
        {

        }

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


         static List<Canlı> ListeKarıncaOluştur()
         {
             List<Canlı> karıncaListe = new List<Canlı>();

             Console.Write("Karınca Sayısını Giriniz: ");
             int karıncaSay = Int32.Parse(Console.ReadLine());

             for (int i = 0; i < karıncaSay; i++)
             {
                 Canlı k = new Karınca();
                 k.Ad = nameGenerator();
                 karıncaListe.Add(k);
             }
             return karıncaListe;
         }
         

        static void ÇukurDüş(List<Canlı> karıncaListe, int çukurBoyut)
        {
            Stack çukur = new Stack();


            for (int i = 0; i < çukurBoyut; i++)
            {
                çukur.Push(karıncaListe[0]);
                karıncaListe.RemoveAt(0);
            }

            for (int i = 0; i < çukurBoyut; i++)
            {
                Karınca k = (Karınca)çukur.Pop();
                karıncaListe.Add(k);
            }

            Console.WriteLine("****ÇIKIŞ");
            for (int i = karıncaListe.Count - 1; 0 <= i; i--)
            {
                if (karıncaListe[i] != null)
                    Console.Write(karıncaListe[i].Ad + (i > 0 && karıncaListe[i - 1] != null ? "-" : ""));
            }

        }
        static void ÇukurDüş(Karınca[] karıncaDizi, int çukurBoyut)
        {
            Stack çukur = new Stack();



            for (int i = 0; i < çukurBoyut; i++)
            {
                çukur.Push(karıncaDizi[i]);
                karıncaDizi[i] = null;

            }
            int a = 0;
            for (int i = 0; i < karıncaDizi.Length; i++)
            {
                if (karıncaDizi[i] != null)
                {
                    karıncaDizi[a] = karıncaDizi[i];
                    karıncaDizi[i] = null;
                    a++;
                }

            }

            Karınca k = null;
            for (int i = 0; i < karıncaDizi.Length && çukur.Count != 0; i++)
            {

                if (karıncaDizi[i] == null)
                {
                    k = (Karınca)çukur.Pop();
                    karıncaDizi[i] = k;
                }
            }


            for (int i = karıncaDizi.Length - 1; 0 <= i; i--)
            {
                if (karıncaDizi[i] != null)
                    Console.Write(karıncaDizi[i].Ad + (i > 0 && karıncaDizi[i - 1] != null ? "-" : ""));
            }
            Console.WriteLine();

        }

        static void TuzakDüş(List<Canlı> karıncaListe) //Can değerleri eklendiğinde bool kontrolü parametre olarak verilebilir.
        {
            karıncaListe[0].Can--;
            if (karıncaListe[0].Can != 0)
            {

                Console.WriteLine("\nTuzağa Girildi.");
                ÇukurDüş(karıncaListe, 1);
            }
            else
            {
                Console.WriteLine("\nTuzak Giriş, {0} Karıncası Öldü.", karıncaListe[0].Ad);
                karıncaListe.RemoveAt(0);
            }

        }
        static Karınca[] TuzakDüş(Karınca[] karıncaDizi)
        {
            
            Karınca[] karıncaDizi1 = new Karınca[karıncaDizi.Length - 1];
            karıncaDizi[0].Can--;

            if (karıncaDizi[0].Can != 0)
            {
                
                Console.WriteLine("Tuzağa Girildi.");
                ÇukurDüş(karıncaDizi, 1);
            }
            else
            {
                int a = 0;
                Console.WriteLine("\nTuzak Giriş, {0} Karıncası Öldü.", karıncaDizi[0].Ad);
                karıncaDizi[0] = null;
                foreach (var item in karıncaDizi)
                {
                    if (item != null)
                    {
                        karıncaDizi1[a] = item;
                        a++;
                    }

                }
            }
            

           
            return karıncaDizi1;
        }
        //ÖNEMLİ!!!!
        //C seçeneğinde yapılacak hesaplama dolayısıyla, Çukur ve Tuzak metodları sayı ve boyut değerlerini Paramtre olarak alsa daha iyi olabilir.

        //Çukur Karınca tipinde tanımlanabilirdi, ama arılar devreye girdiğinde sorun oluşabilir...

        //Toplam Karınca sayısından fazla boyutta bir çukur programın hata vermesine neden olur.


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

        static int[] ÇukurKonumAl(int çukurSay)
        {
            int[] çukurDizi = new int[çukurSay];

            for (int i = 0; i < çukurSay; i++)
            {
                Console.Write("{0}. Çukurun Boyutunu Giriniz: ", i + 1);
                int çukurBoyut = Int32.Parse(Console.ReadLine());
                çukurDizi[i] = çukurBoyut;
            }
            return çukurDizi;

        }

        static object[] DiziAlanOluştur(List<Stack> tuzakListe, int[] çukurDizi)
        {


            Console.WriteLine("\n Alanın Boyutunu Giriniz: ");  //Alanın Boyutu Çukur+Tuzak Toplamından Küçük Olmamalı!!
            int alanBoyut = Int32.Parse(Console.ReadLine());

            object[] alan = new object[alanBoyut];

            for (int i = 0; i < çukurDizi.Length; i++)
            {
                int çukurKonum = rand.Next(1, alanBoyut);
                if (alan[çukurKonum] == null)
                {
                    alan[çukurKonum] = çukurDizi[i];
                }
                else
                {
                    i--;
                }
            }
            //Tek fora'a düşürülebilir, şimdilik işlevsel.          
            for (int i = 0; i < tuzakListe.Count; i++)
            {
                int tuzakKonum = rand.Next(1, alanBoyut);
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