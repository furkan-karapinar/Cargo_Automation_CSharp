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
    public partial class GonderiOlusturForm : Form
    {
        public GonderiOlusturForm()
        {
            InitializeComponent();
        }

        int alici_id = 0;
        int musteri_id = 0;

   

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (textBox15.Text != "" && textBox14.Text != "" && textBox13.Text != "" && textBox12.Text != "" && textBox11.Text != "")
            {
                decimal en = Convert.ToDecimal(textBox15.Text);
                decimal boy = Convert.ToDecimal(textBox14.Text);
                decimal yukseklik = Convert.ToDecimal(textBox13.Text);
                int km = Convert.ToInt32(textBox11.Text);

                decimal desi = (en * boy * yukseklik) / 3000;
                decimal km_ucreti = km * 0.25m;

                decimal ucret = desi + km_ucreti;

                label13.Text = ucret.ToString("0.00");
            }
        }

        private void alici_kayit()
        {
          bool alici_var_mi = false;

            string connectionString = "Data Source=database.db;Version=3;"; // Veritabanı dosyasının yolunu belirtin

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT alici_id FROM alici WHERE alici_tc = @tcNo";

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@tcNo", textBox8.Text);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        alici_var_mi=true;
                        alici_id = Convert.ToInt32(result);
                    }
                    else
                    {
                        alici_var_mi = false;
                    }
                }
            }

            if (!alici_var_mi)
            {
using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Veritabanına veri eklemek için SQL komutu
                string insertQuery = "INSERT INTO alici (alici_ad, alici_soyad, alici_tc, adres, gsm, email) VALUES (@ad, @soyad, @tc, @adres, @gsm, @email)";

                // SQLiteCommand oluşturma
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    // Parametrelerin atanması
                    command.Parameters.AddWithValue("@ad", textBox10.Text);
                    command.Parameters.AddWithValue("@soyad", textBox6.Text);
                    command.Parameters.AddWithValue("@tc", textBox8.Text);
                    command.Parameters.AddWithValue("@adres", textBox7.Text);
                    command.Parameters.AddWithValue("@gsm", maskedTextBox2.Text);
                    command.Parameters.AddWithValue("@email", textBox9.Text);

                    // Komutun çalıştırılması ve etkilenen satır sayısının alınması
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        // Eğer veri eklendiyse, son eklenen ID değerini almak için SQLiteCommand kullanımı
                        command.CommandText = "SELECT last_insert_rowid()";
                        int lastId = Convert.ToInt32(command.ExecuteScalar());

                        // LastID değerini kullanabilirsiniz
                        alici_id = lastId;
                    }
                }
            }
            }
            
            
        }

        private void gonderici_kayit()
        {
            bool gonderici_var_mi = false;

            string connectionString = "Data Source=database.db;Version=3;"; // Veritabanı dosyasının yolunu belirtin

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT musteri_id FROM musteriler WHERE musteri_tc = @tcNo";

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@tcNo", textBox4.Text);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        gonderici_var_mi = true;
                        musteri_id = Convert.ToInt32(result);
                    }
                    else
                    {
                        gonderici_var_mi = false;
                    }
                }
            }

            if (!gonderici_var_mi)
            {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Veritabanına veri eklemek için SQL komutu
                string insertQuery = "INSERT INTO musteriler (musteri_ad, musteri_soyad, musteri_tc, adres, gsm, email) VALUES (@ad, @soyad, @tc, @adres, @gsm, @email)";

                // SQLiteCommand oluşturma
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    // Parametrelerin atanması
                    command.Parameters.AddWithValue("@ad", textBox1.Text);
                    command.Parameters.AddWithValue("@soyad", textBox2.Text);
                    command.Parameters.AddWithValue("@tc", textBox4.Text);
                    command.Parameters.AddWithValue("@adres", textBox5.Text);
                    command.Parameters.AddWithValue("@gsm", maskedTextBox1.Text);
                    command.Parameters.AddWithValue("@email", textBox3.Text);

                    // Komutun çalıştırılması ve etkilenen satır sayısının alınması
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        // Eğer veri eklendiyse, son eklenen ID değerini almak için SQLiteCommand kullanımı
                        command.CommandText = "SELECT last_insert_rowid()";
                        int lastId = Convert.ToInt32(command.ExecuteScalar());

                        // LastID değerini kullanabilirsiniz
                        musteri_id = lastId;
                    }
                }
            }
            }

        }

        private void kayit()
        {
            string connectionString = "Data Source=database.db;Version=3;"; // Veritabanı dosyasının yolunu belirtin
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Veritabanına veri eklemek için SQL komutu
                string insertQuery = @"
                INSERT INTO kargo_bilgileri (
                    musteri_id, alici_id, durum, gonderim_tarihi, teslim_tarihi, 
                    agirlik, en, boy, yukseklik, ucret, alici_sube_id, gonderen_sube_id, km
                )
                VALUES (
                    @musteri_id, @alici_id, @durum, @gonderim_tarihi, @teslim_tarihi, 
                    @agirlik, @en, @boy, @yukseklik, @ucret, @alici_sube_id, @gonderen_sube_id, @km
                )";

                // SQLiteCommand oluşturma
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    // Parametrelerin atanması
                    command.Parameters.AddWithValue("@musteri_id", musteri_id); // Örnek olarak musteri_id değeri
                    command.Parameters.AddWithValue("@alici_id", alici_id); // Örnek olarak alici_id değeri
                    command.Parameters.AddWithValue("@durum", "Kargo Kabul Edildi");
                    command.Parameters.AddWithValue("@gonderim_tarihi", DateTime.Now.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@teslim_tarihi", DBNull.Value); // Örnek olarak teslim_tarihi değeri
                    command.Parameters.AddWithValue("@agirlik", textBox12.Text); // Örnek olarak agirlik değeri
                    command.Parameters.AddWithValue("@en", textBox15.Text); // Örnek olarak en değeri
                    command.Parameters.AddWithValue("@boy", textBox14.Text); // Örnek olarak boy değeri
                    command.Parameters.AddWithValue("@yukseklik", textBox13.Text); // Örnek olarak yukseklik değeri
                    command.Parameters.AddWithValue("@ucret", label13.Text); // Örnek olarak ucret değeri
                    command.Parameters.AddWithValue("@alici_sube_id", sube_id_ogren(comboBox4.Text)); // Örnek olarak alici_sube_id değeri
                    command.Parameters.AddWithValue("@gonderen_sube_id", sube_id_ogren(comboBox3.Text)); // Örnek olarak gonderen_sube_id değeri
                    command.Parameters.AddWithValue("@km", textBox11.Text); // Örnek olarak km değeri

                    // Komutun çalıştırılması ve etkilenen satır sayısının alınması
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        Console.WriteLine("Veri başarıyla eklendi.");

                        command.CommandText = "SELECT last_insert_rowid()";
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            MessageBox.Show("Gönderi Oluşturuldu. Takip No: " + result.ToString(),"Bildiri");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Veri eklenirken bir hata oluştu.");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" &&
                textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "" &&
                    textBox11.Text != "" && textBox12.Text != "" && textBox13.Text != "" && textBox14.Text != "" && textBox15.Text != ""
                    && maskedTextBox1.Text != "" && maskedTextBox2.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" &&
                    comboBox4.Text != "" && comboBox5.Text != "" && comboBox6.Text != "")
            {
                alici_kayit();
                gonderici_kayit();
                kayit();
                this.Close();
            }
        }

        private void GonderiOlusturForm_Load(object sender, EventArgs e)
        {
            illeri_listele();
        }

        private void illeri_listele()
        {
            comboBox1.Items.Clear();
            comboBox6.Items.Clear();

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
                            comboBox6.Items.Add(ilAdi);
                            comboBox1.Items.Add(ilAdi);
                        }
                    }
                }
            }

        }

        private void il_id_ogren(string il)
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
                    command.Parameters.AddWithValue("@secilenIlAdi", il);

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
            comboBox5.Items.Clear();

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
                            comboBox5.Items.Add(ilceAdi);
                        }
                    }
                }
            }
        }

        private void subeleri_listele(string secilenIl,string secilenIlce)
        {
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            string connectionString = "Data Source=database.db;Version=3;"; // Veritabanı dosyasının yolunu belirtin
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Il ve ilçeye göre şubeleri getiren sorgu
                string selectQuery = @"
                SELECT ad FROM sube
                WHERE il = @secilenIl AND ilce = @secilenIlce";

                // SQLiteCommand oluşturma
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@secilenIl", secilenIl);
                    command.Parameters.AddWithValue("@secilenIlce", secilenIlce);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string subeAdi = reader["ad"].ToString();
                            comboBox3.Items.Add(subeAdi);
                            comboBox4.Items.Add(subeAdi);
                            // Şube adını kullanabilirsiniz
                        }
                    }
                }
            }
        }

        private int sube_id_ogren(string sube_adi)
        {
            

            string connectionString = "Data Source=database.db;Version=3;"; // Veritabanı dosyasının yolunu belirtin
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Şube adından ilgili şube ID'sini alacak sorgu
                string selectQuery = "SELECT sube_id FROM sube WHERE ad = @secilenSubeAdi";

                // SQLiteCommand oluşturma
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@secilenSubeAdi", sube_adi);

                    // Şube ID'sini al
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                        
                        // subeId değerini kullanabilirsiniz
                    }
                    else
                    {
                        Console.WriteLine("Bu isimde bir şube bulunamadı.");
                        return -1;
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            il_id_ogren(comboBox1.Text);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            subeleri_listele(comboBox1.Text,comboBox2.Text);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            il_id_ogren(comboBox6.Text);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            subeleri_listele(comboBox6.Text, comboBox5.Text);
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
