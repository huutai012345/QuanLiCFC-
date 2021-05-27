using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string conectionString ;
        private SqlConnection connection;

        public Form1()
        {
            InitializeComponent();
            conectionString = @"Data Source=NHT;Initial Catalog=QuanLiCF2;Integrated Security=True";
            connection = new SqlConnection(conectionString);
            // mở kết nối
            connection.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            //load dữ liệu vào bảng
            dataGridView1.DataSource = getFood().Tables[0];
        }

        // load dữ liệu từ database
        DataSet getFood()
        {
            String query = "SELECT * FROM Food";

            DataSet data = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(data);
            return data;
        }

        // kiểm tra thông tin
        private bool checkThongTin()
        {
            if(textBoxTen.Text == "" || textBoxDonGia.Text == "" || textBoxSoLg.Text == "")
            {
                return false;
            }
            return true;
        }

        // bắt sự kiện click vào bảng
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // lấy index của hàng đang chọn
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            //get dữ liệu của hàng vào textbox
            textBoxMa.Text = selectedRow.Cells[0].Value.ToString();
            textBoxTen.Text = selectedRow.Cells[1].Value.ToString();
            textBoxDonGia.Text = selectedRow.Cells[2].Value.ToString();
            textBoxSoLg.Text = selectedRow.Cells[3].Value.ToString();
        }

        //thêm dữ liệu
        private void buttonThem_Click(object sender, EventArgs e)
        {
            // Kiem tra đã đầy đủ thông tin chưa
            if (!checkThongTin())
            {
                MessageBox.Show("Vui Lòng Điền Đầy Đủ Thông Tin");
                return;
            }

            string query = "INSERT INTO Food VALUES(@name,@price,@amount)";

            SqlCommand command = new SqlCommand(query, connection);
            // truyền tham số vào query
            command.Parameters.AddWithValue("@name", textBoxTen.Text);
            command.Parameters.AddWithValue("@price", textBoxDonGia.Text);
            command.Parameters.AddWithValue("@amount", textBoxSoLg.Text);
            // thực hiện câu query
            command.ExecuteNonQuery();

            // load lại data vào bảng
            dataGridView1.DataSource = getFood().Tables[0];

            // cho cac text bằng rỗng
            textBoxMa.Text = "";
            textBoxTen.Text = "";
            textBoxSoLg.Text = "";
            textBoxDonGia.Text = "";
        }

        //xoá dữ liệu
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Kiem tra đã chọn thức ăn chưa
            if (!checkThongTin())
            {
                MessageBox.Show("Vui Lòng Chọn Thức Ăn Cần Xoá");
                return;
            }

            string query = "DELETE FROM Food WHERE id=@id;";

            SqlCommand command = new SqlCommand(query, connection);
            // truyền tham số vào query
            command.Parameters.AddWithValue("@id", textBoxMa.Text);
            // thực hiện câu query
            command.ExecuteNonQuery();

            // load lại data vào bảng
            dataGridView1.DataSource = getFood().Tables[0];

            // cho cac text bằng rỗng
            textBoxMa.Text = "";
            textBoxTen.Text = "";
            textBoxSoLg.Text = "";
            textBoxDonGia.Text = "";
        }

        //thoát
        private void buttonThoat_Click(object sender, EventArgs e)
        {
            // ngắt kết nối
            connection.Close();
            //đóng chương trình
            Application.Exit();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // Kiem tra đã đầy đủ thông tin chưa
            if (!checkThongTin())
            {
                MessageBox.Show("Vui Lòng Điền Đầy Đủ Thông Tin");
                return;
            }

            string query = "UPDATE Food SET name=@name,price=@price,amount=@amount WHERE id=@id;";

            SqlCommand command = new SqlCommand(query, connection);
            // truyền tham số vào query
            command.Parameters.AddWithValue("@id", textBoxMa.Text);
            command.Parameters.AddWithValue("@name", textBoxTen.Text);
            command.Parameters.AddWithValue("@price", textBoxDonGia.Text);
            command.Parameters.AddWithValue("@amount", textBoxSoLg.Text);
            // thực hiện câu query
            command.ExecuteNonQuery();

            // load lại data vào bảng
            dataGridView1.DataSource = getFood().Tables[0];

            // cho cac text bằng rỗng
            textBoxMa.Text = "";
            textBoxTen.Text = "";
            textBoxSoLg.Text = "";
            textBoxDonGia.Text = "";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // cho cac text bằng rỗng
            textBoxMa.Text = "";
            textBoxTen.Text = "";
            textBoxSoLg.Text = "";
            textBoxDonGia.Text = "";
        }
    }
}
