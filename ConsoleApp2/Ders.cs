using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdevIki
{
    class Ders
    {
        int donem; //dersin dönemi
        OgretimElemanlari ogrt; //dersin öğretim elemanı
        private string ad; //dersin adı
        Bolum b; //dersin bağlı olduğu bölüm
        public Ders(string ad, Bolum b, int donem) //ders oluuşturulurken adı, bağlı olduğu bölüm ve dönemi bilgisi girilmek zorunda
        {
            this.ad = ad;
            this.b = b;
            this.donem = donem;
        }
        public string isim //dersin adı sorgulanabilsin değiştirilemesin.
        {
            get { return ad; }

        }
        public Bolum bol //dersin bölümü sorgulanabilsin, değiştirilemesin.
        {
            get { return b; }
        }
        public OgretimElemanlari eleman //dersin öğretim elemanının görüntülenmesi, değiştirilmesi veya silinmesi için.
        {
            get { return ogrt; }
            set { ogrt = value; }
        }
        public List<Sube> subesi = new List<Sube>(); //derse bağlı şubeler
        public List<Ogrenci> ogrencis = new List<Ogrenci>(); //derse kayıtlı öğrenciler

    }
}
