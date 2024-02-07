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
    public partial class KullaniciSilForm : Form
    {
        public KullaniciSilForm()
        {
            InitializeComponent();
        }
        int secilen_id = 0;

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

        private void KullaniciSilForm_Load(object sender, EventArgs e)
        {
            yukle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Silmek istiyor musunuz?", "Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                sil();
                yukle();
            }
        }

        private void sil()
        {
            string dbFilePath = "database.db"; // Veritabanı dosya yolu

            // Veritabanı bağlantısını oluştur
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();

                // Silme sorgusu
                string deleteQuery = "DELETE FROM kullanicilar WHERE kullanici_id = @id";

                using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                {
                    // Silinecek kullanıcının ID'sini parametre olarak belirt
                    command.Parameters.AddWithValue("@id", secilen_id); // Silinecek kullanıcının ID'si

                    // Sorguyu çalıştır
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Veri başarıyla silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Veri silinirken bir hata oluştu veya silinecek veri bulunamadı.");
                    }
                }

                connection.Close();
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
