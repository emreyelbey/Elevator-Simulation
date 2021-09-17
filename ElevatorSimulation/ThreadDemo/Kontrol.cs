using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Kontrol
    {
        Floor[] floors;
        Elevator[] elevators;
        int kuyrukta_bekleyen_sayisi;
        Thread thread;
        public Kontrol(Floor[] floors, Elevator[] elevators)
        {
            this.floors = floors;
            this.elevators = elevators;
            thread = new Thread(() => AsansorKontrol());
            thread.IsBackground = true;
            thread.Start();
        }

        public void AsansorKontrol()
        {
            while (true)
            {
                //Thread.Sleep(10);
                kuyrukta_bekleyen_sayisi = 0;
                for (int i = 0; i < floors.Length; i++)
                {
                    kuyrukta_bekleyen_sayisi += floors[i].kuyrukta_bekleyen_sayisi;
                }

                if (kuyrukta_bekleyen_sayisi < Elevator.s_capacity * 2 && kuyrukta_bekleyen_sayisi > 0)
                {
                    elevators[0].Working();
                    elevators[1].Waiting();
                    elevators[2].Waiting();
                    elevators[3].Waiting();
                    elevators[4].Waiting();
                }

                if (kuyrukta_bekleyen_sayisi < Elevator.s_capacity * 4 && kuyrukta_bekleyen_sayisi > Elevator.s_capacity * 2)
                {
                    elevators[0].Working();
                    elevators[1].Working();
                    elevators[2].Waiting();
                    elevators[3].Waiting();
                    elevators[4].Waiting();
                }

                if (kuyrukta_bekleyen_sayisi < Elevator.s_capacity * 6 && kuyrukta_bekleyen_sayisi > Elevator.s_capacity * 4)
                {
                    elevators[0].Working();
                    elevators[1].Working();
                    elevators[2].Working();
                    elevators[3].Waiting();
                    elevators[4].Waiting();
                }

                if (kuyrukta_bekleyen_sayisi < Elevator.s_capacity * 8 && kuyrukta_bekleyen_sayisi > Elevator.s_capacity * 6)
                {
                    elevators[0].Working();
                    elevators[1].Working();
                    elevators[2].Working();
                    elevators[3].Working();
                    elevators[4].Waiting();
                }

                if (kuyrukta_bekleyen_sayisi < Elevator.s_capacity * 10 && kuyrukta_bekleyen_sayisi > Elevator.s_capacity * 8)
                {
                    elevators[0].Working();
                    elevators[1].Working();
                    elevators[2].Working();
                    elevators[3].Working();
                    elevators[4].Working();
                }
            }
        }
    }
}
