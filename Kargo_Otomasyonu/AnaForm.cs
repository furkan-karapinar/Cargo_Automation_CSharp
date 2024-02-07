using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kargo_Otomasyonu
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KargoYonetimiForm kargoYonetimiFrm = new KargoYonetimiForm();
            kargoYonetimiFrm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GonderiUcretiHesaplaForm gonderiUcretiHesaplaForm = new GonderiUcretiHesaplaForm();
            gonderiUcretiHesaplaForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GonderiDurumSorgulamaForm gonderiDurumSorgulamaForm = new GonderiDurumSorgulamaForm();
            gonderiDurumSorgulamaForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GonderiDurumGuncellemeForm gonderiDurumGuncellemeForm = new GonderiDurumGuncellemeForm();
            gonderiDurumGuncellemeForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GonderiOlusturForm gonderiOlusturForm = new GonderiOlusturForm();
            gonderiOlusturForm.ShowDialog();
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {

        }

        public void admin_personel_kontrol(int deger)
        {
            if (deger == 1)
            {
                pictureBox2.Visible = true;
                button1.Visible = true;
            }
            else
            {
                pictureBox2.Visible = false;
                button1.Visible = false;
            }
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
