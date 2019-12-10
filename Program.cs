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

            string devam = "e";

            while(devam.Equals("e", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Karınca Sürüsü Hareketi ve Köprü Kurma Problemi"+"\n-------------------");
                Console.WriteLine("Ilk Problemi Simule Etmek için 1'e"+"\nKendiniz Oyun Alanı Oluşturup Simüle Etmek için 2'ye,"+"\n List ve Dizi Performans Kıyaslaması için 3'e basınız.");
                int seçim = Convert.ToInt32(Console.ReadLine());
                if (seçim == 1)
                {
                    Console.WriteLine("\nKarıncaların Can Değerini Belirleyiniz. ");
                    int karıncaCan = Convert.ToInt32(Console.ReadLine());
                    
                    IlkProblem(karıncaCan,true);
                }
                else if (seçim == 2)
                {
                    List<Canlı> karıncaListe = ListeKarıncaOluştur();
                    Console.WriteLine("\nKarıncaların Can Değerini Belirleyiniz. ");
                    int karıncaCan = Convert.ToInt32(Console.ReadLine());

                    foreach (Canlı k in karıncaListe)
                    {
                        k.Can = karıncaCan;
                    }

                    Console.WriteLine("Çukur Sayısını Giriniz.");
                    int çukurSay = Convert.ToInt32(Console.ReadLine());
                    int[] çukurBoyutları = ÇukurKonumAl(çukurSay);
                    List<Stack> tListe = TuzakOluştur();

                    object[] alan = DiziAlanOluştur(tListe, çukurBoyutları);

                    Console.WriteLine("Arıları Dahil Etmek istiyorsanız 1'e istemiyorsanız 2'ye Basınız.");
                    int seçim2= Convert.ToInt32(Console.ReadLine());
                    if (seçim2 == 1)
                    {
                        List<Canlı> arıListe = ListeArıOluştur();
                        ÇözümMetodu(alan,çukurBoyutları,karıncaListe,arıListe,tListe); //Düzeltilecek
                    }
                    else if (seçim2 == 2)
                    {
                        ÇözümMetodu(alan,çukurBoyutları,karıncaListe,tListe,true);
                    }
                }
                else if (seçim == 3)
                {
                    int islemsay = IslemSay();
                    Console.WriteLine(islemsay);

                    int islemsay2 = IslemSay2();
                    Console.WriteLine(islemsay2);
                }


                Console.Write("\nPrograma devam etmek için e tuşuna tıklayınız.");
                devam = Console.ReadLine();
            }






            Console.ReadKey();



        }

        //Arı Oluştur metodu olacak! (Proje notasyonuna göre sadece A seçeneği için??, olmasa bile problem oluşturmaz)

        static void IlkProblem(int canDeğeri,bool islem)
        {
            string harfler = "ABCDE";
            Karınca[] karıncaDizi = new Karınca[harfler.Length];

            for (int i = 0; i < harfler.Length; i++)
            {
                Karınca karınca = new Karınca(harfler[i].ToString());
                karıncaDizi[i] = karınca;
                karıncaDizi[i].Can = canDeğeri;
            }


            int[] çukurBoyutları = { 3, 2, 4 };
            object[] alan = new object[10];
            alan[2] = çukurBoyutları[0];
            alan[4] = çukurBoyutları[1];
            alan[8] = çukurBoyutları[2];

            Stack tuzak = new Stack();
            
            List<Stack> tListe = new List<Stack>();
            tListe.Add(tuzak);

            alan[6] = tuzak;


            if (islem)
            {
                Console.WriteLine();
            }
            ÇözümMetodu(alan, çukurBoyutları, karıncaDizi, tListe,islem);
            if (islem)
            {
                Console.WriteLine();
            }


        }
        static void IlkProblem2(int canDeğeri, bool islem)
        {
            string harfler = "ABCDE";
            List<Canlı> karıncaListe = new List<Canlı>();

            for (int i = harfler.Length - 1; 0 <= i; i--)
            {
                Karınca karınca = new Karınca(harfler[i].ToString());
                karıncaListe.Add(karınca);
                karınca.Can = canDeğeri;
            }


            int[] çukurBoyutları = { 3, 2, 4 };
            object[] alan = new object[10];
            alan[2] = çukurBoyutları[0];
            alan[4] = çukurBoyutları[1];
            alan[8] = çukurBoyutları[2];

            Stack tuzak = new Stack();

            List<Stack> tListe = new List<Stack>();
            tListe.Add(tuzak);

            alan[6] = tuzak;

            if (islem)
            {
                Console.WriteLine();
            }
            ÇözümMetodu(alan, çukurBoyutları, karıncaListe, tListe, islem);
            if (islem)
            {
                Console.WriteLine();
            }


        }

        static List<Canlı> karşılaştır(List<Canlı> karıncaListe, List<Canlı> arıListe)
        {

    
            while (karıncaListe.Count!=0 && arıListe.Count!=0)
            {
                if (karıncaListe[0].Can > arıListe[0].Can) //ARI KESİN ÖLECEK
                {
                    karıncaListe[0].Can = karıncaListe[0].Can - arıListe[0].Can;
                    arıListe.Remove(arıListe[0]);
                 

                }
                else if (karıncaListe[0].Can < arıListe[0].Can) //KARINCA KESİN ÖLECEK
                {
                    arıListe[0].Can = arıListe[0].Can - karıncaListe[0].Can;
                    karıncaListe.Remove(karıncaListe[0]);
                 

                }
                else //EŞİTLERSE, İKİSİ DE ÖLECEK
                {
                    karıncaListe.Remove(karıncaListe[0]);
                    arıListe.Remove(arıListe[0]);
                   
                }
            }
            return (karıncaListe.Count == 0 ? arıListe : karıncaListe); // her ikisi de 0 ise ??
        }

        static void ÇözümMetodu(object[] alan, int[] çukurBoyutları, Karınca[] karıncaDizi, List<Stack> tListe,bool islem)
        {
            if (islem)
            {
                Console.WriteLine("----BAŞLANGIÇ NOKTASI SIRALAMA----");
                for (int i = karıncaDizi.Length - 1; 0 <= i; i--)
                {
                    if (karıncaDizi[i] != null)
                        Console.Write(karıncaDizi[i].Ad + (i > 0 && karıncaDizi[i - 1] != null ? "-" : ""));
                }
            }
            for (int i = 0; i < alan.Length; i++)
            {
                for (int j = 0; j < çukurBoyutları.Length; j++)
                {

                    if (alan[i] != null && alan[i].GetType().IsValueType)
                    {
                        if (çukurBoyutları[j] != 0 && ((int)alan[i]) == çukurBoyutları[j])
                        {
                            if (islem)
                            {
                                Console.WriteLine("\n{0} boyutlu Çukur", çukurBoyutları[j]);
                            }
                            ÇukurDüş(karıncaDizi, çukurBoyutları[j]);
                            çukurBoyutları[j] = 0;
                            if (islem)
                            {
                                for (int z = karıncaDizi.Length - 1; 0 <= z; z--)
                                {
                                    if (karıncaDizi[z] != null)
                                        Console.Write(karıncaDizi[z].Ad + (z > 0 && karıncaDizi[z - 1] != null ? "-" : ""));
                                }
                                Console.WriteLine();
                            }

                        }
                    }
                }
                for (int z = 0; z < tListe.Count; z++)
                {
                    if (alan[i] != null)
                    {
                        if (alan[i].Equals(tListe[z]))
                        {
                            
                            karıncaDizi=TuzakDüş(karıncaDizi,false);

                            tListe.RemoveAt(z);
                        }
                    }
                }

            }
            if (islem)
            {
                Console.WriteLine();
                Console.WriteLine("----VARIŞ NOKTASI SIRALAMA----");
                for (int i = karıncaDizi.Length - 1; 0 <= i; i--)
                {
                    if (karıncaDizi[i] != null)
                        Console.Write(karıncaDizi[i].Ad + (i > 0 && karıncaDizi[i - 1] != null ? "-" : ""));
                }
            }
        }
        static void ÇözümMetodu(object[] alan, int[] çukurBoyutları, List<Canlı> karıncaListe, List<Stack> tListe,bool islem) //arı parametresi
        {
            if (islem)
            {
                Console.WriteLine("----BAŞLANGIÇ NOKTASI SIRALAMA----");
                for (int i = karıncaListe.Count - 1; 0 <= i; i--)
                {
                    if (karıncaListe[i] != null)
                        Console.Write(karıncaListe[i].Ad + (i > 0 && karıncaListe[i - 1] != null ? "-" : ""));
                }

            }

            for (int i = 0; i < alan.Length; i++)
            {
                for (int j = 0; j < çukurBoyutları.Length; j++)
                {

                    if (alan[i] != null && alan[i].GetType().IsValueType)
                    {
                        if (çukurBoyutları[j] != 0 && ((int)alan[i]) == çukurBoyutları[j])
                        {
                            if (islem)
                            {
                                Console.WriteLine("\n{0} boyutlu Çukur", çukurBoyutları[j]);
                            }
                            ÇukurDüş(karıncaListe, çukurBoyutları[j]);
                            çukurBoyutları[j] = 0;
                            if (islem)
                            {
                                Console.WriteLine("\nÇIKIŞ");
                                for (int z = karıncaListe.Count - 1; 0 <= z; z--)
                                {
                                    if (karıncaListe[z] != null)
                                        Console.Write(karıncaListe[z].Ad + (z > 0 && karıncaListe[z - 1] != null ? "-" : ""));
                                }
                            }

                        }
                    }
                }
                for (int z = 0; z < tListe.Count; z++)
                {
                    if (alan[i] != null)
                    {
                        if (alan[i].Equals(tListe[z]))
                        {
                            
                            TuzakDüş(karıncaListe,false);
                            tListe.RemoveAt(z);
                        }
                    }
                }

            }


            if (islem)
            {
                Console.WriteLine();
                Console.WriteLine("----VARIŞ NOKTASI SIRALAMA----");
                for (int i = karıncaListe.Count - 1; 0 <= i; i--)
                {
                    Console.Write(karıncaListe[i].Ad + (i > 0 && karıncaListe[i - 1] != null ? "-" : ""));
                }
            }

        }
        static void ÇözümMetodu(object[] alan, int[] çukurBoyutları, List<Canlı> karıncaListe,List<Canlı> arıListe, List<Stack> tListe) //arı parametresi
        {
            Console.WriteLine("----BAŞLANGIÇ NOKTASI SIRALAMA----");
            for (int i = karıncaListe.Count - 1; 0 <= i; i--)
            {
                if (karıncaListe[i] != null)
                    Console.Write(karıncaListe[i].Ad + (i > 0 && karıncaListe[i - 1] != null ? "-" : ""));
            }

            int buluşma = alan.Length / 2;

            for (int i = 0; i < buluşma; i++)
            {
                for (int j = 0; j < çukurBoyutları.Length; j++)
                {

                    if (alan[i] != null && alan[i].GetType().IsValueType)
                    {
                        if (çukurBoyutları[j] != 0 && ((int)alan[i]) == çukurBoyutları[j])
                        {
                            Console.WriteLine("\n{0} boyutlu Çukur", çukurBoyutları[j]);
                            ÇukurDüş(karıncaListe, çukurBoyutları[j]);
                            
                            
                                Console.WriteLine("\nÇIKIŞ");
                                for (int z = karıncaListe.Count - 1; 0 <= z; z--)
                                {
                                    if (karıncaListe[z] != null)
                                        Console.Write(karıncaListe[z].Ad + (z > 0 && karıncaListe[z - 1] != null ? "-" : ""));
                                }
                            
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
                            TuzakDüş(karıncaListe,true);
                            //tListe.RemoveAt(z);
                        }
                    }
                }

            }
            for (int i = alan.Length-1; buluşma < i; i--)
            {
                for (int j = 0; j < çukurBoyutları.Length; j++)
                {

                    if (alan[i] != null && alan[i].GetType().IsValueType)
                    {
                        if (çukurBoyutları[j] != 0 && ((int)alan[i]) == çukurBoyutları[j])
                        {
                            Console.WriteLine("\n{0} boyutlu Çukur", çukurBoyutları[j]);
                            
                            
                            ÇukurDüş(arıListe, çukurBoyutları[j]);
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
                            TuzakDüş(arıListe,true);
                            //tListe.RemoveAt(z);
                        }
                    }
                }

            }
            // SAVAŞ METODU ÇAĞIR 
            Console.WriteLine("MAXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"+çukurBoyutları.Max());

            if (karıncaListe.Count > çukurBoyutları.Max())
            {
                List<Canlı> c = karşılaştır(karıncaListe, arıListe);
                foreach (Canlı item in c)
                {
                    Console.WriteLine("Sağ Kalan Canlılar " + item.Ad);
                }
            }
            Console.WriteLine();
            

            foreach (Canlı item in karıncaListe)
            {
                Console.WriteLine("Karınca"+item.Ad+item.Can);
            }
            foreach (Canlı item in arıListe)
            {
                Console.WriteLine("Arı"+item.Ad+item.Can);
            }
            if (karıncaListe.Count == 0)
            {
                for (int i = buluşma; 0 < i; i--)
                {


                    for (int j = 0; j < çukurBoyutları.Length; j++)
                    {

                        if (alan[i] != null && alan[i].GetType().IsValueType)
                        {
                            if (çukurBoyutları[j] != 0 && ((int)alan[i]) == çukurBoyutları[j])
                            {
                                Console.WriteLine("\n{0} boyutlu Çukur", çukurBoyutları[j]);
                                ÇukurDüş(arıListe, çukurBoyutları[j]);
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
                                TuzakDüş(arıListe,true);
                                tListe.RemoveAt(z);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = buluşma; i< alan.Length; i++)
                {


                    for (int j = 0; j < çukurBoyutları.Length; j++)
                    {

                        if (alan[i] != null && alan[i].GetType().IsValueType)
                        {
                            if (çukurBoyutları[j] != 0 && ((int)alan[i]) == çukurBoyutları[j])
                            {
                                Console.WriteLine("\n{0} boyutlu Çukur", çukurBoyutları[j]);
                                ÇukurDüş(karıncaListe, çukurBoyutları[j]);
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
                                TuzakDüş(karıncaListe,true);
                                tListe.RemoveAt(z);
                            }
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
        static List<Canlı> ListeArıOluştur()
        {
            List<Canlı> arıListe = new List<Canlı>();

            string ariIsim = "WXYZ";
            for (int i = 0; i < ariIsim.Length; i++)
            {
                Canlı k = new Arı();
                k.Ad = ariIsim[i].ToString();
                k.Can = 7;
                arıListe.Add(k);
            }
            return arıListe;
        }

        private static int IslemSay() 
        {
            int sayac = 0;
            int start, end;

            start = DateTime.Now.Second;
            end = (start + 3) % 60;

            do
            {
                
                for (int i = 0; i < 27; i++)
                {
                    IlkProblem(5,false);
                }

                start = DateTime.Now.Second;
                sayac++;
            } while (start != end);
            return sayac;
        }

        private static int IslemSay2() 
        {
            int sayac = 0;
            int start, end;

            start = DateTime.Now.Second;
            end = (start + 3) % 60;

            do
            {

                for (int i = 0; i < 27; i++)
                {
                    IlkProblem2(5, false);
                }

                start = DateTime.Now.Second;
                sayac++;
            } while (start != end);
            return sayac;
        }
        static int ÇukurDüş(List<Canlı> karıncaListe, int çukurBoyut)
        {
            Stack çukur = new Stack();

            if (karıncaListe.Count < çukurBoyut)
            {
                Console.WriteLine("Canlılar Sıkıştı.");
                return 0;
            }

            for (int i = 0; i < çukurBoyut; i++)
            {

                çukur.Push(karıncaListe[0]);
                karıncaListe.RemoveAt(0);
            }

            for (int i = 0; i < çukurBoyut; i++)
            {
                Canlı k = (Canlı)çukur.Pop();
                karıncaListe.Add(k);
            }
            
            return 0;

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

            
        }

        static void TuzakDüş(List<Canlı> karıncaListe,bool islem) //Can değerleri eklendiğinde bool kontrolü parametre olarak verilebilir.
        {
            
            if (karıncaListe[0].Can == 1)
            {
                if (islem)
                {
                    Console.WriteLine("\nTuzak Giriş, {0} Karıncası Öldü.", karıncaListe[0].Ad);
                }
                karıncaListe.RemoveAt(0);
                
            }
            else
            {
                if (islem)
                {
                    Console.WriteLine("\nTuzağa Girildi.");
                }
                karıncaListe[0].Can--;
                ÇukurDüş(karıncaListe, 1);
            }

        }
        static Karınca[] TuzakDüş(Karınca[] karıncaDizi,bool islem)
        {

            Karınca[] karıncaDizi1 = new Karınca[karıncaDizi.Length - 1];
            karıncaDizi[0].Can--;

            if (karıncaDizi[0].Can ==0)
            {
                int a = 0;
                if (islem)
                {
                    Console.WriteLine("\nTuzak Giriş, {0} Karıncası Öldü.", karıncaDizi[0].Ad);
                }
                karıncaDizi[0] = null;
                foreach (var item in karıncaDizi)
                {
                    if (item != null)
                    {
                        karıncaDizi1[a] = item;
                        a++;
                    }

                }
                return karıncaDizi1;
                
            }
            else
            {
                if (islem)
                {
                    Console.WriteLine("Tuzağa Girildi.");
                }
                ÇukurDüş(karıncaDizi, 1);
                
                return karıncaDizi;

            }
            

           
            
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
            var chars = "ABCDEFGHIJKLMNOPQRSTUVabcdefghijklmnopqrstuv";
            var stringChars = new char[2];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[rand.Next(chars.Length)];
            }
            String name = new String(stringChars);
            return name;
        }
    }
}