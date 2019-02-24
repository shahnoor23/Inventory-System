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
    public partial class dealer_info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True");
        public dealer_info()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //for insert
        private void button1_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into dealer_info values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+textBox4.Text+"','"+textBox5.Text+"')";
            cmd.ExecuteNonQuery();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            dg();
            MessageBox.Show("record inserted successfully");


        }
        //page load
        private void dealer_info_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {

                con.Close();
            }
            con.Open();
            //when form load show the values in database , so call function here
            dg();
        }
        //to show data on grid
        public void dg()
        {

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from dealer_info";
            cmd.ExecuteNonQuery();
            DataTable dt=new DataTable();
            SqlDataAdapter da= new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        //for delete
        private void button3_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from dealer_info where id="+id+"";
            cmd.ExecuteNonQuery();
            dg();
        }


        //for Updation clcik on panel 1 which is visible
        private void button2_Click(object sender, EventArgs e)
        {
            //panel2 which is invisible coz we make it imposible through propertise
            panel2.Visible = true;
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from dealer_info where id="+id+"";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows )
            {
                textBox6.Text = dr["dealer_name"].ToString();
                textBox7.Text = dr["dealer_company_name"].ToString();
                textBox8.Text = dr["contact"].ToString();
                textBox9.Text = dr["address"].ToString();
                textBox10.Text = dr["city"].ToString();

            }

        }
        //panel2 
        //now to show update on panel 2 
        private void button4_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update dealer_info set dealer_name='"+textBox6.Text+"',dealer_company_name='"+textBox7.Text+"',contact='"+textBox8.Text+"',address='"+textBox9.Text+"',city='"+textBox10.Text+"' where id=" + id + "";
            cmd.ExecuteNonQuery();
            panel2.Visible = false;
            dg();

        }
    }
}
