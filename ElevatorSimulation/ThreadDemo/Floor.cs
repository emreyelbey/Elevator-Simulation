using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadDemo
{
    class Floor
    {
        public int numarasi;
        public int mevcut_kisi_sayisi;
        public int kuyrukta_bekleyen_sayisi;
        public Label label;
        public Queue<Grup> kuyruk;
        public string str_kuyruk;
        public List<Grup> list_inside;
        object kilit;
        public Floor(int numarasi,  Label label, object kilit)
        {
            this.kilit = kilit;
            this.numarasi = numarasi;
            kuyruk = new Queue<Grup>();
            this.mevcut_kisi_sayisi = 0;
            this.kuyrukta_bekleyen_sayisi = 0;
            this.label = label;
            list_inside = new List<Grup>();
            this.Print();
        }

        public void Print()
        {
            label.Text = numarasi + ". Floor > #Mevcut: " + mevcut_kisi_sayisi + "  #Kuyruk:  " + kuyrukta_bekleyen_sayisi
            + " > [ " + str_kuyruk + "]";
        }
        
        public void Update()
        {
            this.kuyrukta_bekleyen_sayisi = 0;
            str_kuyruk = "";

            lock (kilit)
            {
                foreach (Grup item in kuyruk)
                {
                    this.kuyrukta_bekleyen_sayisi += item.kisi_sayisi;
                    str_kuyruk += "[" + item.kisi_sayisi + " - " + item.hedef_kat + "] ";
                }
            }

            lock (kilit)
            {
                this.mevcut_kisi_sayisi = 0;
                foreach (Grup item in list_inside)
                {
                    mevcut_kisi_sayisi += item.kisi_sayisi;
                }
            }
            
            this.Print();
        }
    }
}
