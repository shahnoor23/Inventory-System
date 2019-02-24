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
    public partial class purchase_master : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True");
        public purchase_master()
        {
            InitializeComponent();
        }
        //when load page
        private void purchase_master_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            fill_product_name();
            fill_dealer_name();
            dg();

        }
        //To Take Product Name from product table into Purchase Item  in combo box 1 
        public void fill_product_name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from product_name";
            DataTable dt= new DataTable();
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["product_name"].ToString());
            }

        }

        //fill dealer name in combo box 2
        public void fill_dealer_name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from dealer_info";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["dealer_name"].ToString());
            }

        }

        //to take automatic no of pieces of items
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from product_name where product_name='"+comboBox1.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
              // textBox1.Text=(dr["Per_Ps"].ToString());
                textBox1.Text = dr["Per_Ps"].ToString();
               
               

            }
            

        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox8.Text == "" ||
                textBox9.Text == "" || textBox13.Text == "" || textBox12.Text == "")
            {
                MessageBox.Show("Enter All The Fields");


            }
            else
            {
                int i;
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from stock where product_name='" + comboBox1.Text + "'";
                cmd1.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);
                i = Convert.ToInt32(dt1.Rows.Count.ToString());
                if (i == 0)
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into purchase_master values('" + comboBox1.Text + "','" + textBox9.Text +
                                      "','" + textBox3.Text + "','" + textBox3.Text + "','" +
                                      dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + comboBox2.Text + "','" +
                                      textBox4.Text + "','" + textBox5.Text + "','" + textBox2.Text + "','" +
                                      textBox8.Text + "','" + textBox12.Text + "','" + textBox13.Text + "')";
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "insert into stock values('" + comboBox1.Text + "','" + textBox9.Text + "','" +
                                       textBox8.Text + "','" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + comboBox2.Text + "')";
                    cmd3.ExecuteNonQuery();


                }
                else
                {
                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "insert into purchase_master values('" + comboBox1.Text + "','" + textBox9.Text +
                                       "','" + textBox3.Text + "','" + textBox3.Text + "','" +
                                       dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + comboBox2.Text + "','" +
                                       textBox4.Text + "','" + textBox5.Text + "','" + textBox2.Text + "','" +
                                       textBox8.Text + "','" + textBox12.Text + "','" + textBox13.Text + "')";
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd5 = con.CreateCommand();
                    cmd5.CommandType = CommandType.Text;
                    cmd5.CommandText = "update stock set product_in_qty=product_in_qty+ " + textBox9.Text +
                                       " where product_name='" + comboBox1.Text + "'";
                    cmd5.ExecuteNonQuery();

                    SqlCommand cmd6 = con.CreateCommand();
                    cmd6.CommandType = CommandType.Text;
                    cmd6.CommandText = "update stock set product_in_carton=product_in_carton+ " + textBox8.Text +
                                       " where product_name='" + comboBox1.Text + "'";
                    cmd6.ExecuteNonQuery();
                }

                //



                MessageBox.Show("record Inserted Successfully");

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into product_name values('"+textBox6.Text+"','"+textBox7.Text+"')";
            cmd.ExecuteNonQuery();

            textBox6.Text = "";
            textBox7.Text = "";
          
            MessageBox.Show("Record Inserted Successfully");
        }
        public void dg()
        {

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from product_name";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //panel2 which is invisible coz we make it imposible through propertise
            panel2.Visible = true;
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from product_name where id=" + id + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox11.Text = dr["product_name"].ToString();
                textBox10.Text = dr["Per_Ps"].ToString();
              


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update product_name set product_name='" + textBox11.Text + "',Per_Ps='" + textBox10.Text + "' where id=" + id + "";
            cmd.ExecuteNonQuery();
            panel2.Visible = false;
            dg();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        //psc qty
        private void textBox8_Leave(object sender, EventArgs e)
        {
            textBox9.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox8.Text));
        }
        //price
        private void textBox3_Leave(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox5.Text) * Convert.ToInt32(textBox9.Text) - Convert.ToInt32(textBox4.Text));
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
           // compnay price-discount
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToString(Convert.ToInt32(textBox5.Text)- Convert.ToInt32(textBox13.Text));

        }

        private void textBox16_Enter(object sender, EventArgs e)
        {
           // textBox16.Text = Convert.ToString(2/ Convert.ToInt32(textBox17.Text));
           
      
        }

        private void textBox15_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox14_Enter(object sender, EventArgs e)
        {
           
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        
    }
}
