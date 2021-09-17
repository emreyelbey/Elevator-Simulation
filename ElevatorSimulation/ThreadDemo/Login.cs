using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Login
    {
        Thread thread;
        Floor floor0;
        Grup grup;

        public Login(Floor floor0)
        {
            this.floor0 = floor0;
            thread = new Thread(()=>MusteriOlustur());
            thread.IsBackground = true;
            thread.Start();
        }

        public void MusteriOlustur()
        {
            while (true)
            {
                Thread.Sleep(500);
                grup = new Grup(0);
                floor0.kuyruk.Enqueue(grup);
                floor0.Update();
            }
        }
    }
}
