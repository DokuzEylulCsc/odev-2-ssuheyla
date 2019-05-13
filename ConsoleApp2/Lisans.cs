using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdevIki
{
    class Lisans : Ogrenci
    {
        public Lisans(int numara, string adsoyad, Bolum bolum) : base(numara, adsoyad, bolum) //doktora öğrencisi oluşturulurken numara, ad ve bölüm bilgisi girilsin.
        {
            this.numara = numara;
            this.adsoyad = adsoyad;
            this.bolum = bolum;
        }
        public int telno //telefon numarası isteğe bağlı.
        {
            get
            {
                return telefon;
            }
            set
            {
                this.telefon = value;
            }
        }



    }
}
