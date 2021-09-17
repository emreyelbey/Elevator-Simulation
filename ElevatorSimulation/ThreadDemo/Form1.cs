using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadDemo
{
    public partial class Form1 : Form
    {
        Floor floor0;
        Floor floor1;
        Floor floor2;
        Floor floor3;
        Floor floor4;
        Floor[] floors;

        Login login;

        Elevator elevator1;
        Elevator elevator2;
        Elevator elevator3;
        Elevator elevator4;
        Elevator elevator5;
        Elevator[] elevators;

        Exit exit;

        Kontrol kontrol;

        static object kilit = new object();

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            Control.CheckForIllegalCrossThreadCalls = false;

            floor0 = new Floor(0, labelFloor0, kilit);
            floor1 = new Floor(1, labelFloor1, kilit);
            floor2 = new Floor(2, labelFloor2, kilit);
            floor3 = new Floor(3, labelFloor3, kilit);
            floor4 = new Floor(4, labelFloor4, kilit);
            floors = new Floor[5];
            floors[0] = floor0;
            floors[1] = floor1;
            floors[2] = floor2;
            floors[3] = floor3;
            floors[4] = floor4;

            login = new Login(floor0);

            elevator1 = new Elevator(this.labelEleActive0, this.labelEleMode0, this.labelEleFloor0, this.labelEleDestination0, this.labelEleDirection0,
                                     this.labelEleCapacity0, this.labelEleCount0, this.labelEleInside0, floors, kilit);
            elevator2 = new Elevator(this.labelEleActive1, this.labelEleMode1, this.labelEleFloor1, this.labelEleDestination1, this.labelEleDirection1,
                                     this.labelEleCapacity1, this.labelEleCount1, this.labelEleInside1, floors, kilit);
            elevator3 = new Elevator(this.labelEleActive2, this.labelEleMode2, this.labelEleFloor2, this.labelEleDestination2, this.labelEleDirection2,
                                     this.labelEleCapacity2, this.labelEleCount2, this.labelEleInside2, floors, kilit);
            elevator4 = new Elevator(this.labelEleActive3, this.labelEleMode3, this.labelEleFloor3, this.labelEleDestination3, this.labelEleDirection3,
                                     this.labelEleCapacity3, this.labelEleCount3, this.labelEleInside3, floors, kilit);
            elevator5 = new Elevator(this.labelEleActive4, this.labelEleMode4, this.labelEleFloor4, this.labelEleDestination4, this.labelEleDirection4,
                                     this.labelEleCapacity4, this.labelEleCount4, this.labelEleInside4, floors, kilit);
            elevators = new Elevator[5];
            elevators[0] = elevator1;
            elevators[1] = elevator2;
            elevators[2] = elevator3;
            elevators[3] = elevator4;
            elevators[4] = elevator5;

            exit = new Exit(floors);

            //elevators[0].mode = "working";
            //elevators[1].mode = "idle";
            //elevators[2].mode = "idle";
            //elevators[3].mode = "idle";
            //elevators[4].mode = "idle";

            kontrol = new Kontrol(floors, elevators);
        }
    }
}
