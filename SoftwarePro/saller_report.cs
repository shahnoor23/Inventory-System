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
    public partial class saller_report : Form
    {
        
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True");
        string query = "";
        public saller_report()
        {
            InitializeComponent();
        }

        private void saller_report_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void button2_Click(object sender, EventArgs e)
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
            query = "select * from order_item";
        }

        private void button1_Click(object sender, EventArgs e)
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
            query = "select * from order_item where purchase_date>='" + startdate.ToString() + "'AND purchase_date<='" + enddate.ToString() + "'";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from order_item where booker LIKE '%" + textBox1.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            query = "select * from order_item where booker LIKE '%" + textBox1.Text + "%'";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            generate_seller_report gbr = new generate_seller_report();
            gbr.get_value(query.ToString());
            gbr.Show();
        }
    }
}
