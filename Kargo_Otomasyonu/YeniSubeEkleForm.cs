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
    public partial class YeniSubeEkleForm : Form
    {
        public YeniSubeEkleForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("İşlemi iptal etmek istiyor musunuz?", "İptal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox2.Text != ""  && maskedTextBox1.Text != "" && comboBox1.Text != "")
            {
                kaydet();
            }
            else
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz", "Hata");
            }
        }


        private void kaydet()
        {
            string dbFilePath = "database.db";
            string maskedTextValue = new string(maskedTextBox1.Text.Where(char.IsDigit).ToArray());

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();

                string insertQuery = "INSERT INTO sube (il, ilce, adres, telefon, ad) VALUES (@il, @ilce, @adres, @telefon, @ad)";

                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@il", comboBox1.Text);
                    command.Parameters.AddWithValue("@ilce", comboBox2.Text);
                    command.Parameters.AddWithValue("@adres", textBox2.Text);
                    command.Parameters.AddWithValue("@telefon", maskedTextValue);
                    command.Parameters.AddWithValue("@ad", textBox1.Text);

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

        private void illeri_listele()
        {
            comboBox1.Items.Clear();


            string connectionString = "Data Source=database.db;Version=3;"; // Veritabanı dosyasının yolunu belirtin
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT il_adi FROM iller";

                // SQLiteCommand oluşturma
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ilAdi = reader["il_adi"].ToString();
                            comboBox1.Items.Add(ilAdi);
                        }
                    }
                }
            }

        }

        private void il_id_ogren()
        {


            string connectionString = "Data Source=database.db;Version=3;"; // Veritabanı dosyasının yolunu belirtin
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // İl adından il_id değerini alacak sorgu
                string selectQuery = "SELECT il_id FROM iller WHERE il_adi = @secilenIlAdi";

                // SQLiteCommand oluşturma
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@secilenIlAdi", comboBox1.Text);

                    // il_id değerini al
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        int ilId = Convert.ToInt32(result);
                        ilceleri_listele(ilId);
                        // ilId değerini kullanabilirsiniz
                    }
                    else
                    {
                        Console.WriteLine("Bu isimde bir il bulunamadı.");
                    }
                }
            }
        }

        private void ilceleri_listele(int ilId)
        {
            comboBox2.Items.Clear();


            string connectionString = "Data Source=database.db;Version=3;"; // Veritabanı dosyasının yolunu belirtin
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT ilce_adi FROM ilceler WHERE il_id = @ilId";

                // SQLiteCommand oluşturma
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@ilId", ilId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ilceAdi = reader["ilce_adi"].ToString();
                            comboBox2.Items.Add(ilceAdi);
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            il_id_ogren();
        }

        private void YeniSubeEkleForm_Load(object sender, EventArgs e)
        {
            illeri_listele();
        }
    }
}
