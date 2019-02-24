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
    public partial class purchase : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True");
         string query = "";
        public purchase()
        {
            InitializeComponent();
        }

         DataTable dt;
        private void purchase_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }
        //for showing all purchases from purchase master db
        private void button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from purchase_master";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            query = "select * from purchase_master";
            /*int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from purchase_master";
            cmd.ExecuteNonQuery();
            dt=new DataTable();
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //for total
            foreach (DataRow dr in dt.Rows)
            {
                i = i + Convert.ToInt32(dr["product_total"].ToString());


            }

            label3.Text = i.ToString();
            query = "select * from purchase_master";*/
        }
        //search button
        private void button1_Click(object sender, EventArgs e)
        {
            string startdate;
            string enddate;

            startdate = dateTimePicker1.Value.ToString("dd/mm/yyyy");
            enddate = dateTimePicker2.Value.ToString("dd/mm/yyyy");

            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from purchase_master where purchase_date>='"+startdate.ToString() +"'AND purchase_date<='"+enddate.ToString()+"'";
            cmd.ExecuteNonQuery();
            DataTable dt=new DataTable();
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //for total
          /*  foreach (DataRow dr in dt.Rows)
            {
                i = i + Convert.ToInt32(dr["product_total"].ToString());


            }

            label3.Text = i.ToString();*/
            query = "select * from purchase_master where purchase_date>='" + startdate.ToString() + "'AND purchase_date<='" + enddate.ToString() + "'";

        }
        //for search

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          /*  DataView DV = new DataView(dt);
            DV.RowFilter = string.Format("product_name LIKE '%{0}%'",textBox2.Text);
            dataGridView1.DataSource =  DV;*/
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from purchase_master where product_name LIKE '%" + textBox2.Text+ "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            query = "select * from purchase_master where product_name LIKE '%" + textBox2.Text + "%'";

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            generate_purchase_report gbr = new generate_purchase_report();
            gbr.get_value(query.ToString());
            gbr.Show();

        }
    }
}
