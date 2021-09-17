using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Grup
    {
        public int kisi_sayisi;
        public int hedef_kat;
        public int bulundugu_kat;
        public Random random;
        public Grup(int bulundugu_kat)
        {
            this.bulundugu_kat = bulundugu_kat;
            Olustur();
        }

        public Grup(int kisi_sayisi, int hedef_kat)
        {
            this.kisi_sayisi = kisi_sayisi;
            this.hedef_kat = hedef_kat;
        }

        public void Olustur()
        {
            random = new Random();
            kisi_sayisi = random.Next(10) + 1;
            hedef_kat = random.Next(5);
            while(hedef_kat == bulundugu_kat)
            {
                hedef_kat = random.Next(5);
            }
        }
    }
}
