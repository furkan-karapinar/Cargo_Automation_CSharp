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
    public partial class KargoYonetimiForm : Form
    {
        public KargoYonetimiForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YeniKullaniciEkleForm yeniKullaniciEkleForm = new YeniKullaniciEkleForm();
            yeniKullaniciEkleForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KullaniciGuncelleForm kullaniciGuncelleForm = new KullaniciGuncelleForm();
            kullaniciGuncelleForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KullaniciSilForm kullaniciSilForm = new KullaniciSilForm();
            kullaniciSilForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            YeniSubeEkleForm yeniSubeEkleForm = new YeniSubeEkleForm();
            yeniSubeEkleForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SubeGuncelleForm subeGuncelleForm = new SubeGuncelleForm();
            subeGuncelleForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SubeSilForm subeSilForm = new SubeSilForm();
            subeSilForm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            XtraReport1 rapor = new XtraReport1();

            // Rapor görüntüleyici kontrolü oluşturun ve raporu atayın
            ReportPrintTool printTool = new ReportPrintTool(rapor);
            printTool.ShowPreviewDialog();
        }
    }
}
