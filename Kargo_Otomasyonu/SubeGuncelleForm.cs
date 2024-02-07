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
    public partial class SubeGuncelleForm : Form
    {
        public SubeGuncelleForm()
        {
            InitializeComponent();
        }
        int secilen_id = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox2.Text != "" && maskedTextBox1.Text != "" && comboBox1.Text != "")
            {
                DialogResult result = MessageBox.Show("Güncellemek istiyor musunuz?", "Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    guncelle();
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

        private void guncelle()
        {
            string dbFilePath = "database.db";

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();

                string updateQuery = "UPDATE sube SET il = @il, ilce = @ilce, adres = @adres, telefon = @telefon, ad = @ad WHERE sube_id = @subeID";

                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@il", comboBox1.Text);
                    command.Parameters.AddWithValue("@ilce", comboBox2.Text);
                    command.Parameters.AddWithValue("@adres", textBox2.Text);
                    command.Parameters.AddWithValue("@telefon", maskedTextBox1.Text);
                    command.Parameters.AddWithValue("@ad", textBox1.Text);
                    command.Parameters.AddWithValue("@subeID", secilen_id);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Veri başarıyla güncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("Veri güncellenirken bir hata oluştu veya güncellenecek veri bulunamadı.");
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

        private void SubeGuncelleForm_Load(object sender, EventArgs e)
        {
            yukle();
            illeri_listele();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            il_id_ogren();
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
    }
}
