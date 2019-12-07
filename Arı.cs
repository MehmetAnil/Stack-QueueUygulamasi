using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje2A2
{
    class Arı
    {
        private string ad;
        private int can;

        public Arı(string ad)
        {
            this.ad = ad;
        }
        public Arı(string ad, int can)
        {
            this.ad = ad;
            this.can = can;
        }

        public string Ad
        {
            get { return ad; }
            set { ad = value; }
        }
        public int Can
        {
            get { return can; }
            set { can = value; }
        }
    }
}
