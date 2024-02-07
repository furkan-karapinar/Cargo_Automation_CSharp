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
    public partial class GonderiUcretiHesaplaForm : Form
    {
        public GonderiUcretiHesaplaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text!="" && textBox4.Text != "" && textBox5.Text !="")
            {
                decimal en = Convert.ToDecimal(textBox1.Text);
                decimal boy = Convert.ToDecimal(textBox2.Text);
                decimal yukseklik = Convert.ToDecimal(textBox3.Text);
                int km = Convert.ToInt32(textBox5.Text);

                decimal desi = (en * boy * yukseklik) / 3000;
                decimal km_ucreti = km * 0.25m;

                decimal ucret = desi + km_ucreti;

                label6.Text = ucret.ToString("0.00");
            }
            else
            {
                MessageBox.Show("Lüften boş alanları doldurunuz", "Uyarı");
            }




           

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kullanıcının girdiği karakterin sayı, backspace veya virgül olup olmadığını kontrol eder
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true; // Geçersiz karakterleri engeller
            }

            // Virgülün birden fazla kullanılmasını engeller
            if (e.KeyChar == ',' && (sender as TextBox).Text.Contains(','))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("İşlemi iptal etmek istiyor musunuz?", "İptal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
