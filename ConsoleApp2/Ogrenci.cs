using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdevIki
{
    class Ogrenci //öğrenci ana sınıfı.
    {
        protected int numara; //sadece alt sınıflardan ulaşılabilsin.
        protected string adsoyad;
        protected Bolum bolum;
        protected int telefon = 000;

        public Ogrenci(int numara, string adsoyad, Bolum bolum) //öğrenci oluşturulurken numara, ad, soyad ve bölüm bilgileri girilsin.
        {
            this.numara = numara;
            this.adsoyad = adsoyad;
            this.bolum = bolum;
        }
        public string ogradi //sadece sorgulanabilsin
            {
                get
            {
                return adsoyad;
            }

            }
        public string bolumadi //sadece sorgulanabilsin
        {
            get
            {
                return bolum.isim;
            }
        }
        public int ogrno //sadece sorgulanabilsin.
        {
            get
            {
                return numara;
            }
        }
    }
}
