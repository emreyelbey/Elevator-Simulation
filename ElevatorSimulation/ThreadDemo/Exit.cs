using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Exit
    {
        Thread thread;
        Floor[] floors;
        Grup grup;
        Random random;
        int bulundugu_kat;
        int musteri_sayisi;

        public Exit(Floor[] floors)
        {
            random = new Random();
            this.floors = floors;
            thread = new Thread(() => CikisKuyruklariOlustur());
            thread.IsBackground = true;
            thread.Start();
        }

        public void CikisKuyruklariOlustur()
        {
            while (true)
            {
                Thread.Sleep(1000);
                do
                {
                    bulundugu_kat = random.Next(4) + 1;
                } while (floors[bulundugu_kat].list_inside.Count == 0);

                musteri_sayisi = random.Next(5) + 1;
                grup = new Grup(musteri_sayisi, 0);
                floors[bulundugu_kat].kuyruk.Enqueue(grup);
                for (int i = 0; i < floors[bulundugu_kat].list_inside.Count(); i++)
                {
                    if (floors[bulundugu_kat].list_inside.ElementAt<Grup>(i).bulundugu_kat == bulundugu_kat)
                    {
                        floors[bulundugu_kat].list_inside.ElementAt<Grup>(i).kisi_sayisi -= musteri_sayisi;
                    }
                }
                floors[bulundugu_kat].Update();
            }
        }
    }
}
