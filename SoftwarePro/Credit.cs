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

namespace SoftwarePro
{
    public partial class Credit : Form
    {
       
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True");
        DataTable dt = new DataTable();
        string query = "";
        public Credit()
        {
            InitializeComponent();
        }

        private void Credit_Load(object sender, EventArgs e)
        {
           
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
           
           
            fill_combox1();

        }

        public void dg()
        {

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from credit";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        //combobox for shop_name
      

        private void button1_Click(object sender, EventArgs e)
        {

        }
       
        //for insertion in database
        private void button1_Click_1(object sender, EventArgs e)
        {

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into credit values('" + comboBox1.Text + "','" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + textBox1.Text + "')";
            cmd.ExecuteNonQuery();

            comboBox1.Text = "";
            textBox1.Text = "";
           

          
            MessageBox.Show("record inserted successfully");

            dg();
            


        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
           
        }
        //search by date
        private void button2_Click(object sender, EventArgs e)
        {
            string startdate;
            string enddate;

            startdate = dateTimePicker2.Value.ToString("dd/mm/yyyy");
            enddate = dateTimePicker3.Value.ToString("dd/mm/yyyy");

            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from credit where date>='" + startdate.ToString() + "'AND date<='" + enddate.ToString() + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            query = "select * from credit where date>='" + startdate.ToString() + "'AND date<='" + enddate.ToString() + "'";
        }
        //delete selected row
        private void button4_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from credit where id=" + id + "";
            cmd.ExecuteNonQuery();
            dg();
        }
        
      //comboxfil
        public void fill_combox1()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from shop_reg";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["shop_name"].ToString());
            }

        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        //search by credit
        private void button6_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from credit";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            query = "select * from credit";
        }
        //search by name
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from credit where shop_name LIKE '%" + textBox2.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            query = "select * from credit where shop_name LIKE '%" + textBox2.Text + "%'";

        }

        //for print
        private void button3_Click(object sender, EventArgs e)
        {
            generate_credit gpr = new generate_credit();
            gpr.get_value(query.ToString());
            gpr.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
      
      
    }
}
