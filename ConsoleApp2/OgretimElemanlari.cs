using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdevIki
{
    class OgretimElemanlari
    {
        protected string adsoyad;
        protected Bolum bolum;
        protected int telefon = 000;

        public OgretimElemanlari(string adsoyad, Bolum bolum) //öğretim elemanı oluşturulurken ad, soyad ve bağlı olduğu bölüm bilgisi girilsin.
        {
            this.adsoyad = adsoyad;
            this.bolum = bolum;
        }
        public string ogretimadi //sadece sorgulanabilsin.
        {
            get
            {
                return adsoyad;
            }

        }
        public string bolumadi //sadece sorgulanabilsin.
        {
            get
            {
                return bolum.isim;
            }
        }
    }
}
