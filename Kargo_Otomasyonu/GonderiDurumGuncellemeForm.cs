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
    public partial class GonderiDurumGuncellemeForm : Form
    {
        public GonderiDurumGuncellemeForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Güncellemek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                string connectionString = "Data Source=database.db;Version=3;"; // Veritabanı dosyasının yolunu belirtin

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = "UPDATE kargo_bilgileri SET durum = @yeniDurum , teslim_tarihi = @teslim WHERE takip_no = @takipNo";

                    using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@yeniDurum", comboBox1.Text);
                        command.Parameters.AddWithValue("@takipNo", textBox1.Text);

                        if (comboBox1.Text == "Kargo Teslim Edildi")
                        {
                            command.Parameters.AddWithValue("@teslim", DateTime.Now.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@teslim", DBNull.Value);
                        }

                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Durum başarıyla güncellendi.","Bildiri");
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen takip numarasına sahip kayıt bulunamadı veya durum güncellenirken bir hata oluştu.","Hata");
                        }
                    }
                }
            }
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
                            comboBox1.Text = reader["durum"].ToString();
                            label9.Text = reader["gonderim_tarihi"].ToString();
                            label10.Text = reader["teslim_tarihi"].ToString();
                            label11.Text = reader["gonderen_sube_adi"].ToString();
                            label14.Text = reader["alici_sube_adi"].ToString();

                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("İşlemi iptal etmek istiyor musunuz?", "İptal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void GonderiDurumGuncellemeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
