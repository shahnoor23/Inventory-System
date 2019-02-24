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
using System.Drawing.Text;

namespace SoftwarePro
{
    public partial class sales : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True");
        DataTable dt=new DataTable();
         int tot = 0;
        string errorProvider;
        public sales()
        {
            InitializeComponent();
        }

       
        private void sales_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dt.Clear();
            fill_shop_name();
            dt.Columns.Add("product");
            dt.Columns.Add("price");
            dt.Columns.Add("qty");
            dt.Columns.Add("total");
            dt.Columns.Add("C_Price");
            dt.Columns.Add("TO");
            dt.Columns.Add("Discount");
            dt.Columns.Add("booker");
            dt.Columns.Add("purchase_date");
            dt.Columns.Add("qty_Boxses");
            dt.Columns.Add("code");
          




        }
        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            listBox1.Visible = true;
            listBox1.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from stock where product_name like('" + textBox3.Text + "%')";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr["product_name"].ToString());
            }

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;
            }

        }

        private void listBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    this.listBox1.SelectedIndex = this.listBox1.SelectedIndex + 1;

                }

                if (e.KeyCode == Keys.Up)
                {
                    this.listBox1.SelectedIndex = this.listBox1.SelectedIndex -1;

                }

                if (e.KeyCode == Keys.Enter)
                {
                    textBox3.Text = listBox1.SelectedItem.ToString();
                    listBox1.Visible = false;
                    textBox4.Focus();
                }

            }
            catch (Exception ex)
            {

            }

        }
        //price
        private void textBox4_Enter(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 1 * from purchase_master where product_name='"+ textBox3.Text+"'order by id desc";
            cmd.ExecuteNonQuery();
            DataTable dt=new DataTable();
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox4.Text = dr["wholesale_price"].ToString();
            }

        }
        private void textBox8_Enter(object sender, EventArgs e)
        {
            SqlCommand cmd8 = con.CreateCommand();
            cmd8.CommandType = CommandType.Text;
            cmd8.CommandText = "select top 1 * from purchase_master where product_name='" + textBox3.Text + "'order by id desc";
            cmd8.ExecuteNonQuery();
            DataTable dt8 = new DataTable();
            SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
            da8.Fill(dt8);
            foreach (DataRow dr8 in dt8.Rows)
            {
                textBox8.Text = dr8["company_price"].ToString();
            }
        }
        private void textBox10_Enter(object sender, EventArgs e)
        {
           

        }
        private void textBox16_Enter(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from purchase_master where product_name='" + comboBox1.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox1.Text = (dr["Per_Ps"].ToString());

            }
        }
        private void textBox11_Enter(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from product_name where product_name='" + listBox1.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox11.Text = (dr["Per_Ps"].ToString());

            }
        }

        //qunatity
        private void textBox5_Leave(object sender, EventArgs e)
        {

            try
            {
                textBox7.Text = Convert.ToString(Convert.ToInt32(textBox4.Text)*Convert.ToInt32(textBox5.Text));

                
           }
            catch (Exception ex)
            {


            }

       }
        private void textBox6_Leave(object sender, EventArgs e)
        {
            textBox6.Text = Convert.ToString(Convert.ToInt32(textBox7.Text) - Convert.ToInt32(textBox2.Text));
        }
        private void listBox1_VisibleChanged(object sender, EventArgs e)
        {

        }
        //save button
        private void button1_Click(object sender, EventArgs e)
        {
            int stock = 0;
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from stock where product_name='" + textBox3.Text + "'";
            cmd1.ExecuteNonQuery();

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                stock = Convert.ToInt32(dr1["product_in_qty"].ToString());
            }

            if (Convert.ToInt32(textBox5.Text) > stock)
            {
                MessageBox.Show("NOT AVAILABLE QTY");
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["product"] = textBox3.Text;
                dr["price"] = textBox4.Text;
                dr["qty"] = textBox5.Text;
                dr["total"] = textBox6.Text;
                dr["C_Price"] = textBox8.Text;
                dr["TO"] = textBox2.Text;
                dr["Discount"] = textBox10.Text;
                dr["booker"] = textBox1.Text;
                dr["purchase_date"]=dateTimePicker1.Value;
                dr["qty_Boxses"] = textBox12.Text;
                dr["code"] = textBox13.Text;

               
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                tot = tot + Convert.ToInt32(dr["total"].ToString());
                label10.Text = tot.ToString();

            }

            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";


        }
        //delete button working
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tot = 0;
                dt.Rows.RemoveAt(Convert.ToInt32(dataGridView1.CurrentCell.RowIndex.ToString()));
                foreach (DataRow dr1 in dt.Rows )
                {
                    tot = tot + Convert.ToInt32(dr1["total"].ToString());
                    label10.Text = tot.ToString();
                }

            }
            catch (Exception ex)
            {
                
            }
        }
        //save and print bill
        //inserting firstname,lastname,billtype and date in table order_user
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox13.Text == "" || textBox2.Text == "" )
          
            {
                MessageBox.Show("Enter All The Fields");


            }
            else
            {
                string orderid = "";
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into order_user values ('" + textBox1.Text + "','" + comboBox1.Text + "','" +
                                   dateTimePicker1.Value.ToString("dd/mm/yyyy") + "','" + textBox9.Text + "')";
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select top 1 * from order_user order by id desc";
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                foreach (DataRow dr2 in dt2.Rows)
                {
                    orderid = dr2["id"].ToString();

                }

                foreach (DataRow dr in dt.Rows)

                {
                    int qty = 0;
                    double qty_Boxses = 0;
                    string pname = "";
                    SqlCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "insert into order_item values ('" + orderid.ToString() + "','" +
                                       dr["product"].ToString() + "','" + dr["price"].ToString() + "','" +
                                       dr["qty"].ToString() + "','" + dr["total"].ToString() + "','" +
                                       dr["C_Price"].ToString() + "','" + dr["TO"].ToString() + "','" +
                                       dr["Discount"].ToString() + "','" + dr["booker"].ToString() + "','" +
                                       dateTimePicker1.Value.ToString("dd/mm/yyyy") + "','" +
                                       dr["qty_Boxses"].ToString() + "','" + dr["code"].ToString() + "')";
                    cmd3.ExecuteNonQuery();

                    //to reduce value in stock

                    qty = Convert.ToInt32(dr["qty"].ToString());

                    pname = dr["product"].ToString();
                    SqlCommand cmd6 = con.CreateCommand();
                    cmd6.CommandType = CommandType.Text;
                    cmd6.CommandText = "update stock set product_in_qty=product_in_qty-" + qty + " where product_name='" +
                                       pname.ToString() + "'";
                    cmd6.ExecuteNonQuery();



                    /*   SqlCommand cmd11 = con.CreateCommand();
                       cmd11.CommandType = CommandType.Text;
                       cmd11.CommandText = "update stock set product_box=product_box- " + textBox12.Text + " where product_name='" + textBox3.Text + "'";
                       cmd11.ExecuteNonQuery(); */


                    // qty_Boxses = Convert.ToFloat32(dr["qty_Boxses"].ToString());
                    qty_Boxses = float.Parse(dr["qty_Boxses"].ToString());
                    pname = dr["product"].ToString();
                    SqlCommand cmd9 = con.CreateCommand();
                    cmd9.CommandType = CommandType.Text;
                    cmd9.CommandText = "update stock set product_in_carton=product_in_carton-" + qty_Boxses +
                                       " where product_name='" + pname.ToString() + "'";
                    cmd9.ExecuteNonQuery();

                   


                    /*   Decimal stockValue = Convert.ToDecimal(uiStock1000TextBox.Text);
                       Decimal basketValue = Convert.ToDecimal(uiBasket1000TextBox.Text);
                       uiStock1000TextBox.Text = (stockValue - basketValue).ToString();*/




                }

                //toclear box
                textBox1.Text = "";
                comboBox1.Text = "";
                textBox3.Text = "";
                textBox8.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                label10.Text = "";
                dt.Clear();
                dataGridView1.DataSource = dt;
                MessageBox.Show("Record Inserted Successfully");

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       /* private string GenerateOrderNumber()
        {
            string orderNumber;

            Random rnd=new Random();
            int orderpart1 = rnd.Next(1000, 9999);
            int orderpart2 = rnd.Next(1000, 9999);
            orderNumber = "ji-" + orderpart1 + "-" + orderpart2;
            return orderNumber;
        }*/
        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox13.Text == "" || textBox2.Text == "")

            {
                MessageBox.Show("Enter All The Fields");


            }
            else
            {

                string orderid = "";
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into order_user values ('" + textBox1.Text + "','" + comboBox1.Text + "','" +
                                   dateTimePicker1.Value.ToString("dd/mm/yyyy") + "','" + textBox9.Text + "')";
                cmd1.ExecuteNonQuery();


                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select top 1 * from order_user order by id desc";
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                foreach (DataRow dr2 in dt2.Rows)
                {
                    orderid = dr2["id"].ToString();

                }

                foreach (DataRow dr in dt.Rows)
                {
                    int qty = 0;
                    double qty_Boxses = 0;
                    string pname = "";
                    SqlCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "insert into order_item values ('" + orderid.ToString() + "','" +
                                       dr["product"].ToString() + "','" + dr["price"].ToString() + "','" +
                                       dr["qty"].ToString() + "','" + dr["total"].ToString() + "','" +
                                       dr["C_Price"].ToString() + "','" + dr["TO"].ToString() + "','" +
                                       dr["Discount"].ToString() + "','" + dr["booker"].ToString() + "','" +
                                       dateTimePicker1.Value.ToString("dd/mm/yyyy") + "','" +
                                       dr["qty_Boxses"].ToString() + "','" + dr["code"].ToString() + "')";
                    cmd3.ExecuteNonQuery();


                    //to reduce item value in stock

                    qty = Convert.ToInt32(dr["qty"].ToString());
                    pname = dr["product"].ToString();
                    SqlCommand cmd6 = con.CreateCommand();
                    cmd6.CommandType = CommandType.Text;
                    cmd6.CommandText = "update stock set product_in_qty=product_in_qty-" + qty + " where product_name='" +
                                       pname.ToString() + "'";
                    cmd6.ExecuteNonQuery();
                    //to reduce box value in stock
                    qty_Boxses = float.Parse(dr["qty_Boxses"].ToString());
                    pname = dr["product"].ToString();
                    SqlCommand cmd9 = con.CreateCommand();
                    cmd9.CommandType = CommandType.Text;
                    cmd9.CommandText = "update stock set product_in_carton=product_in_carton-" + qty_Boxses +
                                       " where product_name='" + pname.ToString() + "'";
                    cmd9.ExecuteNonQuery();


                }

                //toclear box
                textBox1.Text = "";
                comboBox1.Text = "";
                textBox3.Text = "";
                textBox8.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                label10.Text = "";
                dt.Clear();
                dataGridView1.DataSource = dt;
                MessageBox.Show("Record Inserted Successfully");

                generate_bill gb1 = new generate_bill();
                gb1.get_value(Convert.ToInt32(orderid.ToString()));
                gb1.Show();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
           
        }

        public void fill_shop_name()
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
        //shop
        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            SqlCommand cmd9 = con.CreateCommand();
            cmd9.CommandType = CommandType.Text;
            cmd9.CommandText = "select top 1 * from shop_reg where shop_name='" + comboBox1.Text + "'order by id desc";
            cmd9.ExecuteNonQuery();
            DataTable dt9 = new DataTable();
            SqlDataAdapter da9 = new SqlDataAdapter(cmd9);
            da9.Fill(dt9);
            foreach (DataRow dr9 in dt9.Rows)
            {
                textBox9.Text = dr9["address"].ToString();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox10_Enter_1(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 1 * from purchase_master where product_name='" + textBox3.Text + "'order by id desc";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox10.Text = dr["discount"].ToString();
            }

        }

        private void textBox12_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("Enter Quantity First");

            }
            else
            {

                textBox12.Text = Math.Round((1 / double.Parse(textBox11.Text) * Convert.ToInt32(textBox5.Text)), 1)
                    .ToString();
                //   averagesDoubles = Math.Round((sumInt / ratingListBox.Items.Count), 2);
                //  textBox12.Text = Math.Round(((1/textBox11.Text * textBox5.Text)), 2);
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox13.Text = "P-" + DateTime.Today.ToString("ddMMyyy") + "-" + DateTime.Now.ToString("HHmmss");
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            
        }

       
       
        
        

       

       

    }
}
