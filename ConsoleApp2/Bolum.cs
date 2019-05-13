using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdevIki
{
    class Bolum
    {
        private string ad; //bölümün adı
        Fakulte fa; //bölümün bağlı olduğu fakülte
        public Bolum(string ad, Fakulte fa) //bölüm oluşturulurken bölüm adı ve bağlı olduğu fakülte bilgisi girilmek zorunda.
        {
            this.ad = ad;
            this.fa = fa;
        }
        public string isim //bölümün adı istenirse sadece görüntülenebilsin. değiştirilemesin.
        {
            get { return ad; }

        }
        public Fakulte f //bağlı olduğu fakülte istenirse sadece görüntülenebilsin.
        {
            get { return fa; }
        }
        public List<Ders> derses = new List<Ders>(); //bölüme bağlı dersler
        public List<Ogrenci> ogrencis = new List<Ogrenci>(); //bölüme bağlı öğrenciler
        public List<OgretimElemanlari> ogretimElemanlaris = new List<OgretimElemanlari>(); //bölüme kayıtlı öğretim elemanları
    }
}
