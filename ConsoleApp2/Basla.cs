using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OdevIki
{
    class Basla
    {

        Universite itu = new Universite("İstanbul Teknik Üniversitesi"); //Üniverite tek olacağı için başlangıçta belirledik.
        List<Fakulte> fakultes = new List<Fakulte>(); //üniversiteye bağlı fakülteler listesi
        List<OgretimElemanlari> ogretimElemanlaris = new List<OgretimElemanlari>(); //üniversiteye bağlı öğretim elemanları listesi. Bu listeye kaydolabilmesi için öncelikle bir bölüme dolayısıyla fakülteye bağlı olması gerekli.
        List<Ogrenci> ogrencis = new List<Ogrenci>(); //üniversiteye kayıtlı öğrenciler listesi. Bu listeye eklenebilmesi için öncelikle bir bölüme doalyısıyla bir fakülteye kayıt olması gerekmekte.


        public void Baslat()
        {
            while (true)
            {
                
                int key;
            AnaMenu:  //Ana menünün görüntüleme işlemleri. Buradan yapılan seçime göre alt menülere yönlendiriyor.
                Console.Clear();
                Console.WriteLine("Görüntüleme İşlemleri(1)");
                Console.WriteLine("Ekle/Çıkar işlemleri(2)");
                Console.WriteLine("Dosyaya Yazdırma İşlemleri(3)");
                Console.WriteLine("Cikis(0)");
                try //girilen değeri sayı olarak beklediğimiz için, eğer sayı dışında başka bir şey girerse diye.
                {
                    key = int.Parse(Console.ReadLine()); //dışarıdan okuyoruz.

                    switch (key)
                    {
                        case 1:
                            goto Goruntule;
                        case 2:
                            goto EkleCikar;
                        case 3:
                            goto Yazdirma;
                        case 0:
                            goto bitir;
                        default:
                            Console.WriteLine("Yanlış giriş yaptınız. Yeniden deneyin.");
                            Console.ReadKey();
                            goto AnaMenu; //eğer yanlış giriş yaparsa ana menüyü yenişden ekrana yazdırıyoruz.
                    }
                }
                catch (FormatException e)
                {
                    // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                    Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                }

                catch (Exception exception)
                {
                    // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                    Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                }
            Yazdirma:
                {
                    try
                    {
                        FileStream ds = new FileStream(@"dosya.txt", FileMode.OpenOrCreate,FileAccess.Write); //filestream nesnesi oluşturuyoruz. bin klasörüne yazılacak. varsa açılıp üzerine yazılacak yoksa oluşturulacak.
                        StreamWriter st = new StreamWriter(ds);
                        if (fakultes.Count > 0) //herhangi bir fakülte varsa
                        {
                            for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                            {
                                Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                            }
                            Console.WriteLine("Sectiginiz fakulte numarasini girin");
                            int secim = int.Parse(Console.ReadLine()); //seçilen fakültenin numarasını kullanıcıdan alıyoruz.
                            while (secim <= 0 && secim > fakultes.Count) //eğer listede olmayan bir indis girilmişse.
                            {
                                Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                secim = int.Parse(Console.ReadLine());
                            }

                            if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                            {
                                for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                {
                                    Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim); 
                                }
                                Console.WriteLine("Sectiginiz bolum numarasini girin"); 
                                int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                {
                                    Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                    secim2 = int.Parse(Console.ReadLine());
                                }
                                if (fakultes[secim - 1].bolums[secim2 - 1].derses.Count > 0) //seçilen bölüme kayıtlı ders varsa
                                {
                                    for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses.Count; i++) //bölümdeki dersleri ekrana yazdırıyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].derses[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz ders numarasini girin");
                                    int secim3 = int.Parse(Console.ReadLine()); //kullanıcıdan ders seçmesini istiyoruz.
                                    while (secim3 <= 0 && secim3 > fakultes[secim - 1].bolums[secim2 - 1].derses.Count) //listede olmayan indis girildiyse tekrar alıyoruz.
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim3 = int.Parse(Console.ReadLine());
                                    }
                                    if (fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].eleman != null)  //seçilen dersin öğretim elemanı varsa adını dosyaya yazıyoruz
                                    {
                                        st.WriteLine("Dersin ogretim elemani :" + fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].eleman.ogretimadi);
                                    }
                                    else
                                    {
                                        st.WriteLine("Dersin ogretim elemanı yok"); //öğretim elemanı yoksa dersin öğretim elemanı yok diyoruz 
                                    }
                                    if (fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].subesi.Count > 0) //eğer dersin şubeleri varsa hepsini ekrana yazdırıyoruz.
                                    {
                                        st.Write("Dersin şubeleri : ");
                                        for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].subesi.Count; i++)
                                        {
                                            st.Write(fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].subesi[i].isim + ", ");
                                        }
                                        st.WriteLine();
                                    }
                                    else st.WriteLine("dersin şubesi yok"); //dersin şubesi yoksa dersin şubesi yok yazdırıyoruz.
                                    if (fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis.Count > 0) //derse kayıtlı öğrenci varsa öğrencilerin isimlerini dosyaya yazdırıyoruz.
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis.Count; i++)
                                        {
                                            st.WriteLine(fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis[i].ogradi);
                                        }
                                    }
                                    else st.WriteLine("Ogrenci yok"); //derse kayıtlı öğrenci yoksa öğrenci yok yazdırıyoruz.
                                    st.Flush();
                                    st.Close();
                                    ds.Close();
                                }
                                else 
                                {
                                    Console.WriteLine("Ders eklemeyi unuttunuz ;)"); //bölüme kayıtlı ders yoksa
                                }
                            }
                            else 
                            {
                                Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                            }
                        }
                        else 
                        {
                            Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                        }
                    }
                    catch (FormatException e)
                    {
                        // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                        Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                    }

                    catch (Exception exception)
                    {
                        // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                        Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                    }
                    Console.ReadKey();
                    goto AnaMenu;
                }
            Goruntule: //ana menüden görüntüle seçeneği seçildiyse
                Console.WriteLine("Tüm Öğrencileri Görüntüle(1)");
                Console.WriteLine("Fakülteye Bağlı Öğrencileri Görüntüle(2)");
                Console.WriteLine("Bölüme Bağlı Öğrencileri Görüntüle(3)");
                Console.WriteLine("Derse Bağlı Öğrencileri Görüntüle(4)");
                Console.WriteLine("Tüm Öğretim Elemanlarını Görüntüle(5)");
                Console.WriteLine("Fakülteye Bağlı Öğretim Elemanlarını Görüntüle(6)");
                Console.WriteLine("Bölüme Bağlı Öğretim Elemanlarını Görüntüle(7)");
                Console.WriteLine("Derse Bağlı Öğretim Elemanını Görüntüle(8)");
                Console.WriteLine("Ana Menü(0)");
                try
                {
                    key = int.Parse(Console.ReadLine()); //seçimi dışarıdan alıyoruz.
                    switch (key)
                    {
                        case 1:
                            if (ogrencis.Count < 1) //eğer üniversite kayıtlı hiç öğrenci yoksa.
                            {
                                Console.WriteLine("Hiç öğrenci yok.");
                            }
                            else //üniversiteye kayıtlı öğrencilerin bilgilerini ekrana yazdırıyoruz. 
                                for (int i = 0; i < ogrencis.Count; i++)
                                    Console.WriteLine(i + 1 + ". " + ogrencis[i].ogradi + "\tBolumu: " + ogrencis[i].bolumadi);
                            Console.ReadKey();
                            goto AnaMenu;
                        case 2:
                            if (fakultes.Count < 1) //üniversiteye kayıtlı fakülte var mı kontrolü
                            {
                                Console.WriteLine("Hiç Fakülte Yok!");
                            }
                            else
                            {
                                for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz. 
                                {
                                    Console.WriteLine(i + 1 + ". " + fakultes[i].isim); //kayıtlı fakülteleri ekrana yazdırıyoruz.
                                }
                                Console.WriteLine("Secilecek fakulte numarasini girin");
                                try
                                {
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("yanlis giris yaptiniz yeniden deneyin"); 
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                    if (fakultes[secim - 1].ogrencis.Count < 0) //fakülteye kayıtlı öğrenci var mı diye bakıyoruz.
                                        Console.WriteLine("Seçilen Fakültede Öğrenci Yok!");
                                    else
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].ogrencis.Count; i++) //kayıtlı olan öğrenci varsa bilgilerini listeliyoruz.
                                            Console.WriteLine(i + 1 + ". " + fakultes[secim - 1].ogrencis[i].ogradi + "\tBolumu: " + fakultes[secim - 1].ogrencis[i].bolumadi);
                                    }
                                }
                                catch (FormatException e)
                                {
                                    // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                    Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                                }

                                catch (Exception exception)
                                {
                                    // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                    Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                                }
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 3: //bölüme bağlı öğrencileri listele
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz. 
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim); //fakülteleri listele
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Count > 0) //seçilen bölüme kayıtlı öğrencileri ekrana yazdırıyoruz.
                                        {
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Count; i++)
                                            {
                                                Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].ogrencis[i].ogradi + "\t Bölümü : " + fakultes[secim - 1].bolums[secim2 - 1].ogrencis[i].bolumadi);
                                            }
                                            
                                        }
                                        else
                                        {
                                            Console.WriteLine("Bolume kayitli ogrenci yok."); //bölüme kayıtlı öğrenci yoksa
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 4:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }

                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");

                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim2 - 1].derses.Count > 0) //seçilen bölüme kayıtlı ders varsa
                                        {
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses.Count; i++) //bölümdeki dersleri ekrana yazdırıyoruz.
                                            {
                                                Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].derses[i].isim);
                                            }
                                            Console.WriteLine("Sectiginiz ders numarasini girin");
                                            int secim3 = int.Parse(Console.ReadLine()); //kullanıcıdan ders seçmesini istiyoruz.
                                            while (secim3 <= 0 && secim3 > fakultes[secim - 1].bolums[secim2 - 1].derses.Count) //listede olmayan indis girildiyse tekrar alıyoruz.
                                            {
                                                Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                                secim3 = int.Parse(Console.ReadLine());
                                            }
                                            if (fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis.Count < 1) //seçilen derse kayıtlı öğrenci yoksa
                                                Console.WriteLine("Seçilen derse kayıtlı öğrenci yok!");
                                            else
                                                for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis.Count; i++)
                                                    Console.WriteLine(i + 1 + ". " + fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis[i].ogradi + "\tBolumu: " + fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis[i].bolumadi);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ders eklemeyi unuttunuz ;)"); //bölüme kayıtlı ders yoksa
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 5:
                            if (ogretimElemanlaris.Count < 1) //kayıtlı öğretim elemanı yoksa
                            {
                                Console.WriteLine("Hiç öğretim elemanı yok.");
                            }
                            else
                                for (int i = 0; i < ogretimElemanlaris.Count; i++) //var olan öğretim elemanlarınin bilgilerini ekrana yazdırıyoruz.
                                    Console.WriteLine(i + 1 + ". " + ogretimElemanlaris[i].ogretimadi + "\tBolumu: " + ogretimElemanlaris[i].bolumadi);

                            Console.ReadKey();
                            goto AnaMenu;
                        case 6:
                            try
                            {
                                if (fakultes.Count < 1) //üniversiteye kayıtlı fakülte var mı kontrolü
                                {
                                    Console.WriteLine("Hiç Fakülte Yok!");
                                }
                                else
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + ". " + fakultes[i].isim); //kayıtlı fakülteleri ekrana yazdırıyoruz.
                                    }
                                    Console.WriteLine("Secilecek fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.  
                                    while (secim >= fakultes.Count && secim < 0)
                                    {
                                        Console.WriteLine("yanlis giris yaptiniz yeniden deneyin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                    if (fakultes[secim - 1].ogretimElemanlaris.Count < 0)  //fakülteye kayıtlı öğretim elemanı yoksa.
                                        Console.WriteLine("Seçilen Fakültede Öğretim Elemanı Yok!");
                                    else
                                        for (int i = 0; i < fakultes[secim - 1].ogretimElemanlaris.Count; i++) //seçilen fakülteye ait öğretim elemanlarının bilgilerini yazdırıyoruz.
                                            Console.WriteLine(i + 1 + ". " + fakultes[secim - 1].ogretimElemanlaris[i].ogretimadi + "\tBolumu: " + fakultes[secim - 1].ogretimElemanlaris[i].bolumadi);
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 7:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + ". " + fakultes[i].isim); //kayıtlı fakülteleri ekrana yazdırıyoruz.
                                    }

                                    Console.WriteLine("Secilecek fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.  
                                    while (secim > fakultes.Count && secim <= 0)
                                    {
                                        Console.WriteLine("yanlis giris yaptiniz yeniden deneyin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + ". " + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Secilen bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 >= fakultes[secim - 1].bolums.Count) //girilen indis yanlışsa  
                                        {
                                            Console.WriteLine("yanlis giris yaptiniz yeniden deneyin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim - 2].ogretimElemanlaris.Count < 1) //seçilen bölüme kayıtlı öğretim elemanı yoksa.
                                            Console.WriteLine("Seçilen bölümde öğretim elemanı yok!");
                                        else
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim - 2].ogretimElemanlaris.Count; i++) //var olan öğretim elemanlarını listele
                                                Console.WriteLine(i + 1 + ". " + fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris[i].ogretimadi + "\tBolumu: " + fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris[i].bolumadi);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Herhangi bir bölüm yok, lütfen önce bölüm ekleyiniz"); //fakülteye bağlı bölüm yoksa.
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Herhangi bir fakülte yok, lütfen önce fakülte ekleyiniz"); //üniversiteye bağlı fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 8:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }

                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim2 - 1].derses.Count > 0) //seçilen bölüme kayıtlı ders varsa
                                        {
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses.Count; i++) //bölümdeki dersleri ekrana yazdırıyoruz.
                                            {
                                                Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].derses[i].isim);
                                            }
                                            Console.WriteLine("Sectiginiz ders numarasini girin");
                                            int secim3 = int.Parse(Console.ReadLine()); //kullanıcıdan ders seçmesini istiyoruz.
                                            while (secim3 <= 0 && secim3 > fakultes[secim - 1].bolums[secim2 - 1].derses.Count) //listede olmayan indis girildiyse tekrar alıyoruz.
                                            {
                                                Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                                secim3 = int.Parse(Console.ReadLine());
                                            }
                                            if (fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].eleman == null)
                                                Console.WriteLine("Seçilen derse kayıtlı öğretim elemanı yok!");
                                            else
                                                Console.WriteLine("Öğretim elemanı: " + fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].eleman.ogretimadi);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ders eklemeyi unuttunuz ;)"); //bölüme kayıtlı ders yoksa
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 0:
                            goto AnaMenu;
                        default:
                            Console.WriteLine("Yanlış giriş yaptınız. Lütfen tekrar girin: ");
                            goto Goruntule;
                    }
                }
                catch (FormatException e)
                {
                    // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                    Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                }

                catch (Exception exception)
                {
                    // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                    Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                }


            EkleCikar:
                Console.WriteLine("Fakulte Ekle(1)");
                Console.WriteLine("Bolum Ekle(2)");
                Console.WriteLine("Ders Ekle(3)");
                Console.WriteLine("Şube Ekle(4)");
                Console.WriteLine("Öğrenci Ekle(5)");
                Console.WriteLine("Ogrenciyi derse kaydet(6)");
                Console.WriteLine("Öğretim Elemanı Ekle(7)");
                Console.WriteLine("Öğretim Elemanını derse ata/değiştir(8)");
                Console.WriteLine("Bölümden öğrenci sil(9)");
                Console.WriteLine("Dersten öğrenci sil(10)");
                Console.WriteLine("Bölümden öğretim elemanı silme(11)");
                Console.WriteLine("Ders kapatma(12)");
                
                Console.WriteLine("Ana Menü(0)");

                try
                {
                    key = int.Parse(Console.ReadLine());
                    switch (key)
                    {

                        case 1:
                            Console.WriteLine("Eklenecek fakülte adini girin");
                            string fadi = Console.ReadLine();
                            fakultes.Add(new Fakulte(fadi, itu));
                            goto AnaMenu;
                        case 2:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + ". " + fakultes[i].isim); //kayıtlı fakülteleri ekrana yazdırıyoruz.
                                    }
                                    Console.WriteLine("Secilecek fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz. 

                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("yanlis giris yaptiniz yeniden deneyin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                    Console.WriteLine("Eklenecek bölüm adini girin");
                                    string bolumadi = Console.ReadLine();
                                    fakultes[secim - 1].bolums.Add(new Bolum(bolumadi, fakultes[secim - 1]));
                                }
                                else
                                {
                                    Console.WriteLine("Herhangi bir fakülte yok, lütfen önce fakülte ekleyiniz");
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 3:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + ". " + fakultes[i].isim); //kayıtlı fakülteleri ekrana yazdırıyoruz.
                                    }
                                    Console.WriteLine("Secilecek fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz. 
                                    while (secim > fakultes.Count && secim <= 0)
                                    {
                                        Console.WriteLine("yanlis giris yaptiniz yeniden deneyin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + ". " + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Secilen bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 >= fakultes[secim - 1].bolums.Count) //girilen indis yanlışsa
                                        {
                                            Console.WriteLine("yanlis giris yaptiniz yeniden deneyin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        Console.WriteLine("Eklenecek ders adini girin");
                                        string dersadi = Console.ReadLine();
                                        Console.WriteLine("Eklenecek dersin donemini giriniz");
                                        int donemno = int.Parse(Console.ReadLine());
                                        fakultes[secim - 1].bolums[secim2 - 1].derses.Add(new Ders(dersadi, fakultes[secim - 1].bolums[secim2 - 1], donemno));
                                    }
                                    else
                                    {
                                        Console.WriteLine("Herhangi bir bölüm yok, lütfen önce bölüm ekleyiniz");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Herhangi bir fakülte yok, lütfen önce fakülte ekleyiniz");
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 4:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }

                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim2 - 1].derses.Count > 0) //seçilen bölüme kayıtlı ders varsa
                                        {
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses.Count; i++) //bölümdeki dersleri ekrana yazdırıyoruz.
                                            {
                                                Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].derses[i].isim);
                                            }
                                            Console.WriteLine("Sectiginiz ders numarasini girin");
                                            int secim3 = int.Parse(Console.ReadLine()); //kullanıcıdan ders seçmesini istiyoruz.
                                            while (secim3 <= 0 && secim3 > fakultes[secim - 1].bolums[secim2 - 1].derses.Count) //listede olmayan indis girildiyse tekrar alıyoruz.
                                            {
                                                Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                                secim3 = int.Parse(Console.ReadLine());
                                            }
                                            Console.WriteLine("Sube numarasini giriniz");
                                            string subeno = Console.ReadLine();
                                            fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].subesi.Add(new Sube(subeno, fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1]));
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ders eklemeyi unuttunuz ;)"); //bölüme kayıtlı ders yoksa
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 5:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }

                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        Console.WriteLine("Lisans (1), Yuksek Lisans(2), Doktora(3)");
                                        int tercih = int.Parse(Console.ReadLine());
                                        while (tercih < 1 && tercih > 3)
                                        {
                                            Console.WriteLine("Hatali islem yaptiniz, tekrar deneyin");
                                            tercih = int.Parse(Console.ReadLine());
                                        }
                                        if (tercih == 1)
                                        {

                                            Console.WriteLine("Ogrenci numarasini girin");
                                            int ogrno = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Ogrenci adini girin");
                                            string ograd = Console.ReadLine();
                                            ogrencis.Add(new Lisans(ogrno, ograd, fakultes[secim - 1].bolums[secim2 - 1])); //genele kaydetme
                                            fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Add(new Lisans(ogrno, ograd, fakultes[secim - 1].bolums[secim2 - 1]));// bolume kaydetme
                                            fakultes[secim - 1].ogrencis.Add(new Lisans(ogrno, ograd, fakultes[secim - 1].bolums[secim2 - 1]));//fakulteye kaydetme
                                        }
                                        else if (tercih == 2)
                                        {
                                            Console.WriteLine("Ogrenci numarasini girin");
                                            int ogrno = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Ogrenci adini girin{");
                                            string ograd = Console.ReadLine();
                                            ogrencis.Add(new YuksekLisans(ogrno, ograd, fakultes[secim - 1].bolums[secim2 - 1])); //genele kaydetme
                                            fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Add(new YuksekLisans(ogrno, ograd, fakultes[secim - 1].bolums[secim2 - 1]));// bolume kaydetme
                                            fakultes[secim - 1].ogrencis.Add(new YuksekLisans(ogrno, ograd, fakultes[secim - 1].bolums[secim2 - 1]));//fakulteye kaydetme
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ogrenci numarasini girin");
                                            int ogrno = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Ogrenci adini girin{");
                                            string ograd = Console.ReadLine();
                                            ogrencis.Add(new Doktora(ogrno, ograd, fakultes[secim - 1].bolums[secim2 - 1])); //genele kaydetme
                                            fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Add(new Doktora(ogrno, ograd, fakultes[secim - 1].bolums[secim2 - 1]));// bolume kaydetme
                                            fakultes[secim - 1].ogrencis.Add(new Doktora(ogrno, ograd, fakultes[secim - 1].bolums[secim2 - 1]));//fakulteye kaydetme
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 6:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Count > 0) //seçilen bölüme kayıtlı öğrencileri ekrana yazdırıyoruz.
                                        {
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Count; i++)
                                            {
                                                Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].ogrencis[i].ogradi + "\t Bölümü : " + fakultes[secim - 1].bolums[secim2 - 1].ogrencis[i].bolumadi);
                                            }
                                            Console.WriteLine("Sectiginiz ogrencinin indisi");
                                            int tercih2 = int.Parse(Console.ReadLine());
                                            while (tercih2 <= 0 && tercih2 > fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Count)
                                            {
                                                Console.WriteLine("Hatali islem yaptiniz, tekrar girin");
                                                tercih2 = int.Parse(Console.ReadLine());
                                            }
                                            if (fakultes[secim - 1].bolums[secim2 - 1].derses.Count > 0) //seçilen bölüme kayıtlı ders varsa
                                            {
                                                for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses.Count; i++) //bölümdeki dersleri ekrana yazdırıyoruz.
                                                {
                                                    Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].derses[i].isim);
                                                }
                                                Console.WriteLine("Sectiginiz ders numarasini girin");
                                                int secim3 = int.Parse(Console.ReadLine()); //kullanıcıdan ders seçmesini istiyoruz.
                                                while (secim3 <= 0 && secim3 > fakultes[secim - 1].bolums[secim2 - 1].derses.Count) //listede olmayan indis girildiyse tekrar alıyoruz.
                                                {
                                                    Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                                    secim3 = int.Parse(Console.ReadLine());
                                                }
                                                fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis.Add(fakultes[secim - 1].bolums[secim2 - 1].ogrencis[tercih2 - 1]);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ders eklemeyi unuttunuz ;)"); //bölüme kayıtlı ders yoksa
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Bolume kayitli ogrenci yok."); //bölüme kayıtlı öğrenci yoksa
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 7:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        Console.WriteLine("Ogretim elemani adini girin");
                                        string ogretimadi = Console.ReadLine();
                                        ogretimElemanlaris.Add(new OgretimElemanlari(ogretimadi, fakultes[secim - 1].bolums[secim2 - 1])); //genele kaydetme
                                        fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris.Add(new OgretimElemanlari(ogretimadi, fakultes[secim - 1].bolums[secim2 - 1]));// bolume kaydetme
                                        fakultes[secim - 1].ogretimElemanlaris.Add(new OgretimElemanlari(ogretimadi, fakultes[secim - 1].bolums[secim2 - 1]));//fakulteye kaydetme
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 8:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris.Count > 0)
                                        {
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris.Count; i++)
                                            {
                                                Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris[i].ogretimadi + "\t Bölümü : " + fakultes[secim - 1].bolums[secim2 - 1].ogrencis[i].bolumadi);
                                            }
                                            Console.WriteLine("Sectiginiz ogretim elemanının indisi");
                                            int tercih2 = int.Parse(Console.ReadLine());
                                            while (tercih2 <= 0 && tercih2 > fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris.Count)
                                            {
                                                Console.WriteLine("Hatali islem yaptiniz, tekrar girin");
                                                tercih2 = int.Parse(Console.ReadLine());
                                            }
                                            if (fakultes[secim - 1].bolums[secim2 - 1].derses.Count > 0) //seçilen bölüme kayıtlı ders varsa
                                            {
                                                for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses.Count; i++) //bölümdeki dersleri ekrana yazdırıyoruz.
                                                {
                                                    Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].derses[i].isim);
                                                }
                                                Console.WriteLine("Sectiginiz ders numarasini girin");
                                                int secim3 = int.Parse(Console.ReadLine()); //kullanıcıdan ders seçmesini istiyoruz.
                                                while (secim3 <= 0 && secim3 > fakultes[secim - 1].bolums[secim2 - 1].derses.Count) //listede olmayan indis girildiyse tekrar alıyoruz.
                                                {
                                                    Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                                    secim3 = int.Parse(Console.ReadLine());
                                                }
                                                fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].eleman = fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris[tercih2 - 1];
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ders eklemeyi unuttunuz ;)"); //bölüme kayıtlı ders yoksa
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Bolume kayitli ogrenci yok."); //bölüme kayıtlı öğrenci yoksa
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 9:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Count > 0) //seçilen bölüme kayıtlı öğrencileri ekrana yazdırıyoruz.
                                        {
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Count; i++)
                                            {
                                                Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].ogrencis[i].ogradi + "\t Bölümü : " + fakultes[secim - 1].bolums[secim2 - 1].ogrencis[i].bolumadi);
                                            }
                                            Console.WriteLine("Sectiginiz ogrencinin indisi");
                                            int tercih2 = int.Parse(Console.ReadLine());
                                            while (tercih2 <= 0 && tercih2 > fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Count)
                                            {
                                                Console.WriteLine("Hatali islem yaptiniz, tekrar girin");
                                                tercih2 = int.Parse(Console.ReadLine());
                                            }
                                            for (int i = 0; i < ogrencis.Count; i++)
                                            {
                                                if (fakultes[secim - 1].bolums[secim2 - 1].ogrencis[tercih2 - 1] == ogrencis[i])
                                                {
                                                    ogrencis.Remove(ogrencis[i]);
                                                    break;
                                                }
                                            }
                                            for (int i = 0; i < fakultes[secim - 1].ogrencis.Count; i++)
                                            {
                                                if (fakultes[secim - 1].bolums[secim2 - 1].ogrencis[tercih2 - 1] == fakultes[secim - 1].ogrencis[i])
                                                {
                                                    fakultes[secim - 1].ogrencis.Remove(fakultes[secim - 1].ogrencis[i]);
                                                    break;
                                                }
                                            }
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses.Count; i++) //bölümdeki dersleri ekrana yazdırıyoruz.
                                            {
                                                for (int j = 0; j < fakultes[secim - 1].bolums[secim2 - 1].derses[i].ogrencis.Count; j++)
                                                {
                                                    if (fakultes[secim - 1].bolums[secim2 - 1].derses[i].ogrencis[j] == fakultes[secim - 1].bolums[secim2 - 1].ogrencis[tercih2 - 1])
                                                    {
                                                        fakultes[secim - 1].bolums[secim2 - 1].derses[i].ogrencis.Remove(fakultes[secim - 1].bolums[secim2 - 1].derses[i].ogrencis[j]);
                                                        break;
                                                    }
                                                }
                                            }
                                            fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Remove(fakultes[secim - 1].bolums[secim2 - 1].ogrencis[tercih2 - 1]);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Bolume kayitli ogrenci yok."); //bölüme kayıtlı öğrenci yoksa
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 10:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim2 - 1].derses.Count > 0) //seçilen bölüme kayıtlı ders varsa
                                        {
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses.Count; i++) //bölümdeki dersleri ekrana yazdırıyoruz.
                                            {
                                                Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].derses[i].isim);
                                            }
                                            Console.WriteLine("Sectiginiz ders numarasini girin");
                                            int secim3 = int.Parse(Console.ReadLine()); //kullanıcıdan ders seçmesini istiyoruz.
                                            while (secim3 <= 0 && secim3 > fakultes[secim - 1].bolums[secim2 - 1].derses.Count) //listede olmayan indis girildiyse tekrar alıyoruz.
                                            {
                                                Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                                secim3 = int.Parse(Console.ReadLine());
                                            }
                                            if (fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis.Count > 0)
                                            {
                                                for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Count; i++)
                                                {
                                                    Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].ogrencis[i].ogradi + "\t Bölümü : " + fakultes[secim - 1].bolums[secim2 - 1].ogrencis[i].bolumadi);
                                                }
                                                Console.WriteLine("Sectiginiz ogrencinin indisi");
                                                int tercih2 = int.Parse(Console.ReadLine());
                                                while (tercih2 <= 0 && tercih2 > fakultes[secim - 1].bolums[secim2 - 1].ogrencis.Count)
                                                {
                                                    Console.WriteLine("Hatali islem yaptiniz, tekrar girin");
                                                    tercih2 = int.Parse(Console.ReadLine());
                                                }
                                                fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis.Remove(fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1].ogrencis[tercih2 - 1]);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Bolume kayitli ogrenci yok."); //bölüme kayıtlı öğrenci yoksa
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ders eklemeyi unuttunuz ;)"); //bölüme kayıtlı ders yoksa
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 11:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris.Count > 0)
                                        {
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris.Count; i++)
                                            {
                                                Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris[i].ogretimadi + "\t Bölümü : " + fakultes[secim - 1].bolums[secim2 - 1].ogrencis[i].bolumadi);
                                            }
                                            Console.WriteLine("Sectiginiz ogretim elemanının indisi");
                                            int tercih2 = int.Parse(Console.ReadLine());
                                            while (tercih2 <= 0 && tercih2 > fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris.Count)
                                            {
                                                Console.WriteLine("Hatali islem yaptiniz, tekrar girin");
                                                tercih2 = int.Parse(Console.ReadLine());
                                            }
                                            if (fakultes[secim - 1].bolums[secim2 - 1].derses.Count > 0) //seçilen bölüme kayıtlı ders varsa
                                            {
                                                for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses.Count; i++) //bölümdeki dersleri ekrana yazdırıyoruz.
                                                {
                                                    if (fakultes[secim - 1].bolums[secim2 - 1].derses[i].eleman == fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris[tercih2 - 1])
                                                    {
                                                        fakultes[secim - 1].bolums[secim2 - 1].derses[i].eleman = null;
                                                    }
                                                }
                                            }
                                            for (int i = 0; i < ogretimElemanlaris.Count; i++)
                                            {
                                                if (ogretimElemanlaris[i] == fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris[tercih2 - 1])
                                                {
                                                    ogretimElemanlaris.Remove(ogretimElemanlaris[i]);
                                                    break;
                                                }
                                            }
                                            for (int i = 0; i < fakultes[secim - 1].ogretimElemanlaris.Count; i++)
                                            {
                                                if (fakultes[secim - 1].ogretimElemanlaris[i] == fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris[tercih2 - 1])
                                                {
                                                    fakultes[secim - 1].ogretimElemanlaris.Remove(fakultes[secim - 1].ogretimElemanlaris[i]);
                                                    break;
                                                }
                                            }
                                            fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris.Remove(fakultes[secim - 1].bolums[secim2 - 1].ogretimElemanlaris[tercih2 - 1]);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Bolume kayitli ogrenci yok."); //bölüme kayıtlı öğrenci yoksa
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 12:
                            try
                            {
                                if (fakultes.Count > 0) //ekli fakülte varsa
                                {
                                    for (int i = 0; i < fakultes.Count; i++) //mevcut fakülteleri listeliyoruz.
                                    {
                                        Console.WriteLine(i + 1 + "." + fakultes[i].isim);
                                    }
                                    Console.WriteLine("Sectiginiz fakulte numarasini girin");
                                    int secim = int.Parse(Console.ReadLine());  //hangi fakülteyi seçeceğini kullanıcıdan alıyoruz.
                                    while (secim >= fakultes.Count && secim < 0) //listede olmayan değer girilirse yeniden alalım
                                    {
                                        Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                        secim = int.Parse(Console.ReadLine());
                                    }
                                   if (fakultes[secim - 1].bolums.Count > 0) //seçilen fakülteye bağlı herhangi bir bölüm varsa
                                    {
                                        for (int i = 0; i < fakultes[secim - 1].bolums.Count; i++) //bölümlerin isimlerini ekrana yazdırıyoruz.
                                        {
                                            Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[i].isim);
                                        }
                                        Console.WriteLine("Sectiginiz bolum numarasini girin");
                                        int secim2 = int.Parse(Console.ReadLine()); //seçilen bölümün numarasını kullanıcıdan alıyoruz.
                                        while (secim2 <= 0 && secim2 > fakultes[secim - 1].bolums.Count) //listede olmayan bir indis girilirse tekrar alıyoruz.
                                        {
                                            Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                            secim2 = int.Parse(Console.ReadLine());
                                        }
                                        if (fakultes[secim - 1].bolums[secim2 - 1].derses.Count > 0) //seçilen bölüme kayıtlı ders varsa
                                        {
                                            for (int i = 0; i < fakultes[secim - 1].bolums[secim2 - 1].derses.Count; i++) //bölümdeki dersleri ekrana yazdırıyoruz.
                                            {
                                                Console.WriteLine(i + 1 + "." + fakultes[secim - 1].bolums[secim2 - 1].derses[i].isim);
                                            }
                                            Console.WriteLine("Sectiginiz ders numarasini girin");
                                            int secim3 = int.Parse(Console.ReadLine()); //kullanıcıdan ders seçmesini istiyoruz.
                                            while (secim3 <= 0 && secim3 > fakultes[secim - 1].bolums[secim2 - 1].derses.Count) //listede olmayan indis girildiyse tekrar alıyoruz.
                                            {
                                                Console.WriteLine("Hatali giris yaptiniz, tekrar girin");
                                                secim3 = int.Parse(Console.ReadLine());
                                            }

                                            fakultes[secim - 1].bolums[secim2 - 1].derses.Remove(fakultes[secim - 1].bolums[secim2 - 1].derses[secim3 - 1]);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ders eklemeyi unuttunuz ;)"); //bölüme kayıtlı ders yoksa
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bolum eklemeyi unuttunuz ;)"); //fakülteye kayıtlı bölüm yoksa
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fakulte eklemeyi unuttunuz ;)"); //herhangi bir fakülte yoksa
                                }
                            }
                            catch (FormatException e)
                            {
                                // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                                Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                            }

                            catch (Exception exception)
                            {
                                // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                                Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                            }
                            Console.ReadKey();
                            goto AnaMenu;
                        case 0:
                            goto AnaMenu;
                        default:
                            Console.WriteLine("Yanlış giriş yaptınız!");
                            goto EkleCikar;
                    }
                }
                catch (FormatException e)
                {
                    // Klavyeden sayı yerine harf veya karakter girildiğinde bu catch bloğu çalışacak.
                    Console.WriteLine("Hata! Sadece sayı girilebilir. Hata Mesajı:{0}", e.Message);
                }

                catch (Exception exception)
                {
                    // Öngördüğümüz hatalar haricinde bir hata oluştuğunda bu catch bloğu çalışacak.
                    Console.WriteLine("Beklenmedik bir hata oluştu. Hata Mesajı:{0}", exception.Message);
                }
            }
        bitir:
            Console.WriteLine("Bitti");
        }
    }
}