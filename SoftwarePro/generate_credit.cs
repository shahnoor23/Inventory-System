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
    public partial class generate_credit : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\projects\SoftwarePro\SoftwarePro\inventory.mdf;Integrated Security=True");
        string j;
        public void get_value(string i)
        {
            j = i;

        }
        
        public generate_credit()
        {
            InitializeComponent();
        }

        private void generate_credit_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();
            DataSet7 ds = new DataSet7();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = j;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds.DataTable1);





            CrystalReport7 myreport = new CrystalReport7();
            myreport.SetDataSource(ds);

            crystalReportViewer1.ReportSource = myreport;


        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
