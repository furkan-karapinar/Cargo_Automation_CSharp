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
    public partial class HesapGirisiForm : Form
    {
        private SQLiteConnection connection;
        private SQLiteCommand command;
        private SQLiteDataReader reader;

        // SQLite veritabanı dosyası yolunu burada belirtin
        private string dbFilePath = "database.db";

        public HesapGirisiForm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (giris_kontrol() == 1) // Admin ise
            {
                MessageBox.Show("Giriş Başarılı");
                this.Visible = false;
                AnaForm anaForm = new AnaForm();
                anaForm.admin_personel_kontrol(1);
                anaForm.ShowDialog();
                this.Close();
            }
            else if (giris_kontrol() == 2) // Personel ise
            {
                MessageBox.Show("Giriş Başarılı");
                this.Visible = false;
                AnaForm anaForm = new AnaForm();
                anaForm.admin_personel_kontrol(2);
                anaForm.ShowDialog();
                this.Close();
            }
        }

        private int giris_kontrol()
        {
            connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;");
            int dogrulama = 0;

            try
            {
                connection.Open();

                string query = "SELECT * FROM kullanicilar WHERE username=@username AND sifre=@sifre";
                command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@username", textBox1.Text);
                command.Parameters.AddWithValue("@sifre", textBox2.Text);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string uyeDurumu = reader["uye_statusu"].ToString();

                    // Üyelik durumu kontrolü burada yapılıyor
                    if (uyeDurumu == "admin")
                    {
                        // Admin
                        dogrulama = 1;
                    }
                    else
                    {
                        // Personel
                        dogrulama = 2;
                    }
                }
                else
                {
                    // Kullanıcı adı veya şifre hatalı
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection != null)
                    connection.Close();
            }

            return dogrulama;
        }
    }
}
