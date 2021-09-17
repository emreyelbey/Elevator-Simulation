using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadDemo
{
    class Elevator
    {
        Label labActive;
        public string str_labActive;
        Label labMode;
        public string str_labMode;
        Label labFloor;
        public string str_labFloor;
        Label labDest;
        public string str_labDest;
        Label labDir;
        public string str_labDir;
        Label labCap;
        public string str_labCap;
        Label labCount;
        public string str_labCount;
        Label labIns;
        public string str_labIns;

        public bool isActive;
        public string mode;
        public int floor;
        public int destination;
        public string direction;
        public int capacity;
        public static int s_capacity = 10;
        public int count_inside;
        public string str_inside;
        public int parcalanmis_grup;
        public bool b_kontrol;

        Grup grup;
        List<Grup> inside;
        Floor[] floors;

        Thread thread;
        object kilit;
        public Elevator(Label labActive, Label labMode, Label labFloor, Label labDest, Label labDir, Label labCap, Label labCount, Label labIns, Floor[] floors, object kilit)
        {
            this.kilit = kilit;
            this.labActive = labActive;
            this.labMode = labMode;
            this.labFloor = labFloor;
            this.labDest = labDest;
            this.labDir = labDir;
            this.labCap = labCap;
            this.labCount = labCount;
            this.labIns = labIns;

            this.str_labActive = labActive.Text;
            this.str_labMode = labMode.Text;
            this.str_labFloor = labFloor.Text;
            this.str_labDest = labDest.Text;
            this.str_labDir = labDir.Text;
            this.str_labCap = labCap.Text;
            this.str_labCount = labCount.Text;
            this.str_labIns = labIns.Text;

            this.isActive = false;
            this.mode = "idle";
            this.floor = 0;
            this.destination = 0;
            this.direction = "up";
            this.capacity = 10;
            this.count_inside = 0;

            this.floors = floors;
            this.inside = new List<Grup>();

            this.thread = new Thread(() => AsansorAktif());
            this.thread.IsBackground = true;
            this.thread.Start();
            Thread.Sleep(500);

            this.Print();
        }

        public void Working()
        {   
            if (this.mode == "idle")
            {
                this.mode = "working";
                this.isActive = true;
                this.Update();
            }
        }

        public void Waiting()
        {
            if (this.mode == "working" && this.b_kontrol == true)
            {
                Console.WriteLine("PAUSE");
                this.mode = "idle";
                this.isActive = false;
                this.Update();
            }
        }

        public void Print()
        {
            labActive.Text = this.str_labActive + Convert.ToString(this.isActive);
            labMode.Text = this.str_labMode + this.mode;
            labFloor.Text = this.str_labFloor + Convert.ToString(this.floor);
            labDest.Text = this.str_labDest + Convert.ToString(this.destination);
            labDir.Text = this.str_labDir + this.direction;
            labCap.Text = this.str_labCap + Convert.ToString(this.capacity);
            labCount.Text = this.str_labCount + Convert.ToString(this.count_inside);
            labIns.Text = this.str_labIns + this.str_inside;
        }

        public void Update()
        {
            this.str_inside = "";
            foreach (Grup item in this.inside)
            {
                this.str_inside += "[" + item.kisi_sayisi + " - " + item.hedef_kat + "] ";
            }
            this.Print();
        }

        public void AsansorAktif()
        {
            while (true)
            {
                this.b_kontrol = false;
                if (this.mode == "working")
                {
                    MusteriAl();
                    HedefBelirleveHareketEt();
                    MusteriBirak();
                    this.b_kontrol = true;
                }
            }
        }

        public void MusteriAl()
        {
            lock (kilit)
            {
                while (this.count_inside < this.capacity && floors[floor].kuyruk.Count > 0)
                {
                    if (floors[floor].kuyruk.Peek().kisi_sayisi + this.count_inside <= this.capacity)
                    {
                        this.count_inside += floors[floor].kuyruk.Peek().kisi_sayisi;
                        this.inside.Add(floors[floor].kuyruk.Dequeue());
                        this.Update();
                        floors[floor].Update();
                    }
                    else
                    {
                        parcalanmis_grup = this.capacity - this.count_inside;
                        this.count_inside += parcalanmis_grup;
                        floors[floor].kuyruk.Peek().kisi_sayisi -= parcalanmis_grup;
                        grup = new Grup(parcalanmis_grup, floors[floor].kuyruk.Peek().hedef_kat);
                        this.inside.Add(grup);
                        this.Update();
                        floors[floor].Update();
                    }
                }
            }
        }

        public void MusteriBirak()
        {
            lock (kilit)
            {
                if (this.inside.Count > 0)
                {
                    for (int i = 0; i < inside.Count; i++)
                    {
                        if (this.inside[i].hedef_kat == this.floor)
                        {
                            this.floors[this.floor].list_inside.Add(this.inside[i]);
                            this.count_inside -= this.inside[i].kisi_sayisi;
                            this.inside.RemoveAt(i);
                            i--;
                            this.Update();
                            this.floors[this.floor].Update();
                        }
                    }
                }
            }
        }

        public void YukariHareket()
        {
            MusteriBirak();
            this.Update();
            while (this.floor != this.destination)
            {
                floor++;
                MusteriBirak();
                this.Update();
                Thread.Sleep(200);
            }
            MusteriBirak();
            this.Update();
        }

        public void AsagiHareket()
        {
            MusteriAl();
            this.Update();
            while (this.floor != this.destination)
            {
                floor--;
                MusteriAl();
                this.Update();
                Thread.Sleep(200);
            }
            MusteriAl();
            this.Update();
        }

        public void HedefBelirleveHareketEt()
        {
            if (this.direction == "up")
            {
                this.destination = 0;
                for (int i = 0; i < this.inside.Count; i++)
                {
                    if (this.inside[i].hedef_kat > this.destination)
                    {
                        this.destination = this.inside[i].hedef_kat;
                    }
                }
                YukariHareket();
                CikisKuyrukKontrolu();
                this.direction = "down";
            }
            if (this.direction == "down")
            {
                this.destination = 0;
                AsagiHareket();
                this.direction = "up";
            }
        }

        public void CikisKuyrukKontrolu()
        {
            for (int i = this.floor + 1; i <= 4; i++)
            {
                if (floors[i].kuyruk.Count > 0)
                {
                    this.destination = i;
                }
            }
            YukariHareket();
        }
    }
}
