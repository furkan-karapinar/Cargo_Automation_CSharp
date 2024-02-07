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
    public partial class KullaniciGuncelleForm : Form
    {
        public KullaniciGuncelleForm()
        {
            InitializeComponent();
        }

        int secilen_id = 0; 
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && maskedTextBox1.Text != "" && comboBox1.Text != "")
            {
                DialogResult result = MessageBox.Show("Güncellemek istiyor musunuz?", "Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    guncelle();
                }
            }
            else
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz", "Hata");
                yukle();
            }
        }

        private void guncelle()
        {
            string dbFilePath = "database.db"; // Veritabanı dosya yolu

            // Veritabanı bağlantısını oluştur
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();

                // Güncelleme sorgusu
                string updateQuery = "UPDATE kullanicilar SET kullanici_adi = @adi, kullanici_soyadi = @soyadi, gsm = @gsm , username = @username , sifre = @sifre , uye_statusu = @uye_statusu WHERE kullanici_id = @id";

                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    // Parametrelerle sorguyu hazırla
                    command.Parameters.AddWithValue("@adi", textBox1.Text);
                    command.Parameters.AddWithValue("@soyadi", textBox2.Text);
                    command.Parameters.AddWithValue("@gsm", maskedTextBox1.Text);
                    command.Parameters.AddWithValue("@id", secilen_id); // Güncellenecek kullanıcının ID'si
                    command.Parameters.AddWithValue("@username", textBox3.Text);
                    command.Parameters.AddWithValue("@sifre", textBox4.Text);
                    command.Parameters.AddWithValue("@uye_statusu", comboBox1.Text);

                    // Sorguyu çalıştır
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Veri başarıyla güncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("Veri güncellenirken bir hata oluştu.");
                    }
                }

                connection.Close();
            }
        }

        private void yukle()
        {
            string query = "SELECT * FROM kullanicilar";
            string dbFilePath = "database.db"; // Veritabanı dosya yolu

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }

                connection.Close();
            }
        }

        private void KullaniciGuncelleForm_Load(object sender, EventArgs e)
        {
            yukle();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];
                secilen_id = Convert.ToInt32(selectedRow.Cells["kullanici_id"].Value);
                // Verileri TextBox'lara aktarma
                textBox1.Text = Convert.ToString(selectedRow.Cells["kullanici_adi"].Value);
                textBox2.Text = Convert.ToString(selectedRow.Cells["kullanici_soyadi"].Value);
                maskedTextBox1.Text = Convert.ToString(selectedRow.Cells["gsm"].Value);
                textBox3.Text = Convert.ToString(selectedRow.Cells["username"].Value);
                textBox4.Text = Convert.ToString(selectedRow.Cells["sifre"].Value);
                comboBox1.Text = Convert.ToString(selectedRow.Cells["uye_statusu"].Value);
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
