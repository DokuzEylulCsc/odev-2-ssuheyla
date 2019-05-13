using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdevIki
{
    class Universite
    {
        string ad; //üniversite adı
        public Universite(string ad) //üniversite oluşturulurken adı girilsin.
        {
            this.ad = ad;

        }
        public string isim //sadece sorgulanabilsin.
        {
            get { return ad; }
        }
    }
}
