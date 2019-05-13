using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdevIki
{
    class Fakulte
    {
        private string ad; //fakülte adı.
        Universite uni; //bağlı olduğu üniversite. 
        public Fakulte(string ad, Universite uni) //fakülte oluşturulurken adı ve bağlı olduğu üniversite bilgisi girilsin.
        {
            this.ad = ad;
            this.uni = uni;
        }
        public string isim //sadece sorgulanabilsin.
        {
            get { return ad; }

        }
        public Universite u //sadece sorgulanabilsin.
        {
            get { return uni; }
        }
        public List<Bolum> bolums = new List<Bolum>(); //fakülteye bağlı bölümler listesi
        public List<Ogrenci> ogrencis = new List<Ogrenci>(); //fakülteye bağlı öğrenciler listesi
        public List<OgretimElemanlari> ogretimElemanlaris = new List<OgretimElemanlari>(); //fakülteye bağlı öğretim elemanları listesi

    }
}
