using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdevIki
{
    class Sube
    {
        private string ad; //sube adı
        Ders d; //hangi derse kayıtlı olduğu
        public Sube(string ad, Ders d) //oluşturulurken adı ve ders bilgisi girilsin.
        {
            this.ad = ad;
            this.d = d;
        }
        public string isim //sadece sorgulanabilsin.
        {
            get { return ad; }

        }
        public Ders dd //sadece sorgulanabilsin.
        {
            get { return d; }
        }
        List<Ogrenci> ogrenciss = new List<Ogrenci>(); //şubeye bağlı öğrenciler listesi

    }
}
