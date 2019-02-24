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
using DataRow = System.Data.DataRow;
namespace SoftwarePro
{
    public partial class order_bill : Form

    {
        SqlConnection con =
            new SqlConnection(
                @"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True");

        string query = "";

        public order_bill()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from order_item";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            // dataGridView1.Columns(2).DefaultCellStyle.Format = "n3";
            query = "select * from order_item";
        }

        private void order_bill_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from order_item where code LIKE '%" + textBox1.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //  dataGridView1.Columns(3).DefaultCellStyle.Format = "#,###";
            query = "select * from order_item where code LIKE '%" + textBox1.Text + "%'";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string startdate;
            string enddate;

            startdate = dateTimePicker1.Value.ToString("dd/mm/yyyy");
            enddate = dateTimePicker2.Value.ToString("dd/mm/yyyy");

            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from order_item where purchase_date>='" + startdate.ToString() + "'AND purchase_date<='" + enddate.ToString() + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //for total
            /*  foreach (DataRow dr in dt.Rows)
              {
                  i = i + Convert.ToInt32(dr["product_total"].ToString());


              }

              label3.Text = i.ToString();*/
            query = "select * from order_item where purchase_date>='" + startdate.ToString() + "'AND purchase_date<='" + enddate.ToString() + "'";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            generate_order_bill gbr = new generate_order_bill();
           gbr.get_value(query.ToString());
            gbr.Show();
        }
    



}
}
