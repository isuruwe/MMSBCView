using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMSViewTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public string sqlQuery;
        public string UserValue = "";
        private void LoadPersonal2(string id)
        {
            DateTime batchdate = new DateTime();
            DataSet odsLoadLoanAppliedPersonal = new DataSet();
            odsLoadLoanAppliedPersonal.Clear();
            dataGridView1.Rows.Clear();
            DataSet odsvoltxndata = new DataSet();


            String strDBCon = "Data Source = 135.22.210.105; Initial Catalog = MMS; User ID = mmsuser; Password = password;";
            oSqlConnection = new SqlConnection(strDBCon);
            oSqlCommand = new SqlCommand();
            sqlQuery = "SELECT distinct f.Relationship,a.TestSID,e.CategoryName,c.serviceNo,c.Surname,c.Initials,CAST(a.[RequestedTime] as DATE) FROM [MMS].[dbo].[Lab_Report] as a left join [MMS].[dbo].[Patient_Detail] as b on a.PDID=b.PDID inner join [MMS].[dbo].[Patient] as  c  on c.PID=b.PID inner join  [MMS].[dbo].[Lab_SubCategory] as d on d.[LabTestID]=a.[LabTestID] inner join [MMS].[dbo].[Lab_MainCategory] as e on e.CategoryID=d.CategoryID inner join [MMS].[dbo].[RelationshipType] as f on c.RelationshipType=f.RTypeID  where   a.TestSID='" + id + "' ";

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;

            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlConnection.Open();
            oSqlDataAdapter.Fill(odsLoadLoanAppliedPersonal);
            oSqlConnection.Close();
            if (odsLoadLoanAppliedPersonal.Tables[0].Rows.Count > 0)
            {
                for (int count = 0; count < odsLoadLoanAppliedPersonal.Tables[0].Rows.Count; count++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[count].Cells["ServiceNo"].Value = odsLoadLoanAppliedPersonal.Tables[0].Rows[count]["ServiceNo"].ToString();
                    dataGridView1.Rows[count].Cells["Initials"].Value = odsLoadLoanAppliedPersonal.Tables[0].Rows[count]["Initials"].ToString();
                    dataGridView1.Rows[count].Cells["Surname"].Value = odsLoadLoanAppliedPersonal.Tables[0].Rows[count]["Surname"].ToString();
                    dataGridView1.Rows[count].Cells["Test"].Value = odsLoadLoanAppliedPersonal.Tables[0].Rows[count]["CategoryName"].ToString();
                    dataGridView1.Rows[count].Cells["pdid"].Value = odsLoadLoanAppliedPersonal.Tables[0].Rows[count]["TestSID"].ToString();
                    dataGridView1.Rows[count].Cells["ptype"].Value = odsLoadLoanAppliedPersonal.Tables[0].Rows[count]["Relationship"].ToString();
                    dataGridView1.Rows[count].Cells["rqdate"].Value = odsLoadLoanAppliedPersonal.Tables[0].Rows[count]["Column1"];
                }
            }
            textBox1.Text = "";

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadPersonal2(this.textBox1.Text);
                this.ActiveControl = textBox1;
            }
        }
    }
}
