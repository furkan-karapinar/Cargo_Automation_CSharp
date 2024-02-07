using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kargo_Otomasyonu
{
    public partial class YeniKullaniciEkleForm : Form
    {
        public YeniKullaniciEkleForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.Text != "" && comboBox1.Text != "")
            {
                kaydet();
            }
            else
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz","Hata");
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

        private void kaydet()
        {
            string dbFilePath = "database.db"; // Veritabanı dosya yolu
            string maskedTextValue = new string(maskedTextBox1.Text.Where(char.IsDigit).ToArray());

            // Veritabanı bağlantısını oluştur
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();

                // Veri ekleme sorgusu
                string insertQuery = "INSERT INTO kullanicilar (kullanici_adi, kullanici_soyadi, gsm, username, sifre, uye_statusu) " +
                                     "VALUES (@adi, @soyadi, @gsm, @username, @sifre, @uyeStatusu)";

                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    // Parametrelerle sorguyu hazırla
                    command.Parameters.AddWithValue("@adi", textBox1.Text);
                    command.Parameters.AddWithValue("@soyadi", textBox2.Text);
                    command.Parameters.AddWithValue("@gsm", maskedTextValue);
                    command.Parameters.AddWithValue("@username", textBox3.Text);
                    command.Parameters.AddWithValue("@sifre", textBox4.Text);
                    command.Parameters.AddWithValue("@uyeStatusu", comboBox1.SelectedText);

                    // Sorguyu çalıştır
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Veri başarıyla eklendi.");
                    }
                    else
                    {
                        MessageBox.Show("Veri eklenirken bir hata oluştu.");
                    }
                }

                connection.Close();
            }
        }
    }
}
