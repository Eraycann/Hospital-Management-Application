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

namespace Hastane_Proje
{
    public partial class FrmDuyurular : Form
    {
        public FrmDuyurular()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        private void FrmDuyurular_Load(object sender, EventArgs e)
        {
            DataTable dataTable= new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM TBL_Duyurular",connect );
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource= dataTable;
        }
    }
}
