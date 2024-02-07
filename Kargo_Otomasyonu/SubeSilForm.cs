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
    public partial class SubeSilForm : Form
    {
        public SubeSilForm()
        {
            InitializeComponent();
        }

        int secilen_id = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox2.Text != "" && maskedTextBox1.Text != "" && comboBox1.Text != "")
            {
                DialogResult result = MessageBox.Show("Silmek istiyor musunuz?", "Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    sil();
                    yukle();
                }
            }
            else
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz", "Hata");
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

        private void sil()
        {
            string dbFilePath = "database.db";

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM sube WHERE sube_id = @subeID";

                using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@subeID", secilen_id);

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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];
                secilen_id = Convert.ToInt32(selectedRow.Cells["sube_id"].Value);
                // Verileri TextBox'lara aktarma
                textBox1.Text = Convert.ToString(selectedRow.Cells["ad"].Value);
                textBox2.Text = Convert.ToString(selectedRow.Cells["adres"].Value);
                maskedTextBox1.Text = Convert.ToString(selectedRow.Cells["telefon"].Value);
                comboBox2.Text = Convert.ToString(selectedRow.Cells["ilce"].Value);
                comboBox1.Text = Convert.ToString(selectedRow.Cells["il"].Value);
            }
        }

        private void yukle()
        {
            string query = "SELECT * FROM sube";
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

        private void SubeSilForm_Load(object sender, EventArgs e)
        {
            yukle();
        }
    }
}
