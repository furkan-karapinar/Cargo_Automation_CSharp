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
    public partial class GonderiDurumSorgulamaForm : Form
    {
        public GonderiDurumSorgulamaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=database.db;Version=3;"; // Veritabanı dosyasının yolunu belirtin

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = @"
SELECT 
    m.musteri_ad, m.musteri_soyad,
    a.alici_ad, a.alici_soyad,
    k.durum, k.gonderim_tarihi, k.teslim_tarihi, s2.ad as gonderen_sube_adi , s.ad as alici_sube_adi
FROM kargo_bilgileri k
INNER JOIN musteriler m ON k.musteri_id = m.musteri_id
INNER JOIN alici a ON k.alici_id = a.alici_id
LEFT JOIN sube s ON k.alici_sube_id = s.sube_id
LEFT JOIN sube s2 ON k.gonderen_sube_id = s2.sube_id
                WHERE k.takip_no = @takipNo";

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@takipNo", textBox1.Text);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            label7.Text = reader["musteri_ad"].ToString() + " " + reader["musteri_soyad"].ToString();
                            label8.Text = reader["alici_ad"].ToString() + " " + reader["alici_soyad"].ToString();
                            label11.Text = reader["durum"].ToString();
                            label9.Text = reader["gonderim_tarihi"].ToString();
                            label10.Text = reader["teslim_tarihi"].ToString();
                            label12.Text = reader["gonderen_sube_adi"].ToString();
                            label14.Text = reader["alici_sube_adi"].ToString();

                        }
                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Sadece rakam veya kontrol tuşlarına izin ver
            }
        }
    }
}
