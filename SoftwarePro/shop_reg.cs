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
    public partial class shop_reg : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True
");
        public shop_reg()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void shop_reg_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            con.Open();
            dg();
        }
        //function to show shops on data grid
        public void dg()
        {

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from shop_reg";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        //add button
        private void button1_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into shop_reg values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
            cmd.ExecuteNonQuery();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
          
            dg();
            MessageBox.Show("record inserted successfully");
        }
        //delete button
        private void button3_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from shop_reg where id=" + id + "";
            cmd.ExecuteNonQuery();
            dg();
        }
        //edited button
        private void button4_Click(object sender, EventArgs e)
        {

            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update shop_reg set name='" + textBox7.Text + "',shop_name='" + textBox8.Text + "',address='" + textBox9.Text + "',contact_no='" + textBox10.Text + "' where id=" + id + "";
            cmd.ExecuteNonQuery();
            panel1.Visible = false;
            dg();
        }
        //update button
        private void button2_Click(object sender, EventArgs e)
        {
            //panel2 which is invisible coz we make it imposible through propertise
            panel1.Visible = true;
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from shop_reg where id=" + id + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox7.Text = dr["name"].ToString();
                textBox8.Text = dr["shop_name"].ToString();
                textBox9.Text = dr["address"].ToString();
                textBox10.Text = dr["contact_no"].ToString();
                

            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
