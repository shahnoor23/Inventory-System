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
    public partial class generate_order_bill : Form
    {
        int tot = 0;
        string j;
        public void get_value(string i)
        {
            j = i;

        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True");
        public generate_order_bill()
        {
            InitializeComponent();
        }

        private void generate_order_bill_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            DataSet6 ds = new DataSet6();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = j;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds.DataTable1);





            CrystalReport1 myreport = new CrystalReport1();
            myreport.SetDataSource(ds);

            crystalReportViewer1.ReportSource = myreport;


        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
