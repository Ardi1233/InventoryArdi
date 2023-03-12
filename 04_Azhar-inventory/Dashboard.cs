using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace Daftar_Pepustakaan
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection("server=localhost;user=root;password=;database=dmarket");

        public void Tampil(string valueToSearch)
        {
            MySqlCommand command = new MySqlCommand("select * from market where concat(id,nama_produk, harga, satuan, stok) like '%" + valueToSearch + "%'", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.RowTemplate.Height = 60;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = table;
            DataGridViewImageColumn imgcol = new DataGridViewImageColumn();
            //imgcol = (DataGridViewImageColumn)dataGridView1.Columns[5];
            imgcol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Nama Produk";
            dataGridView1.Columns[2].HeaderText = "Harga";
            dataGridView1.Columns[3].HeaderText = "Satuan";
            dataGridView1.Columns[4].HeaderText = "Stok Produk";
            dataGridView1.Columns[5].HeaderText = "Foto";
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void ExecMyQuery(MySqlCommand mcomd, string myMsg)
        {
            connection.Open();
            if (mcomd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show(myMsg);
            }
            else
            {
                MessageBox.Show("Error");
            }
            connection.Close();
            Tampil("");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.JPG;*.PNG;*.JPEG;*.GIF;)|*.jpg;*.png;*.jpeg;*.gif;";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();
            MySqlCommand command = new MySqlCommand("INSERT INTO market(nama_produk, harga, satuan, stok, foto) VALUES (@nama_produk, @harga, @satuan, @stok, @foto)", connection);
            command.Parameters.Add("@nama_produk", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@harga", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@satuan", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@stok", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@foto", MySqlDbType.Blob).Value = img;

            ExecMyQuery(command, "Data Berhasil Ditambahkan");
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            Tampil("");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Byte[] img = (Byte[])dataGridView1.CurrentRow.Cells[5].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);

            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM market WHERE id=@id", connection);
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = textBox8.Text;

            ExecMyQuery(command, "Data Berhasil Dihapus");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();
            MySqlCommand command = new MySqlCommand("UPDATE market SET nama_produk = @nama_produk, harga = @harga, satuan = @satuan, stok = @stok, foto = @foto WHERE id = @id", connection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = textBox8.Text;
            command.Parameters.Add("@nama_produk", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@harga", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@satuan", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@stok", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@foto", MySqlDbType.Blob).Value = img;

            ExecMyQuery(command, "Data Berhasil Diubah");
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            Tampil(textBox7.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
