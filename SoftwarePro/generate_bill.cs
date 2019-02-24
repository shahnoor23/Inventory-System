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
    public partial class generate_bill : Form
    {
         int tot = 0;
         int j;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True");
        public generate_bill()
        {
            InitializeComponent();
        }

        public void get_value(int i)
        {
            j = i;

        }

        private void generate_bill_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            con.Open();
            DataSet2 ds = new DataSet2();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from order_user where id="+j+"";
            cmd.ExecuteNonQuery();
            DataTable dt=new DataTable();
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            da.Fill(ds.DataTable2);


            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from order_item where  order_id=" + j + "";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(ds.DataTable1);
            da2.Fill(dt2);

            


          


            tot = 0;
            foreach (DataRow dr2 in dt2.Rows)
            {
                tot = tot + Convert.ToInt32(dr2["total"].ToString());
            }
            CrystalReport2 myreport = new CrystalReport2();
            myreport.SetDataSource(ds);
            myreport.SetParameterValue("total",tot.ToString());
            crystalReportViewer1.ReportSource = myreport;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
