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
    public partial class FrmRandevuListe : Form
    {
        public FrmRandevuListe()
        {
            InitializeComponent();
        }

        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        private void FrmRandevuListe_Load(object sender, EventArgs e)
        {
            DataTable dataTable= new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM TBL_Randevular",connect );
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource= dataTable;
        }

        
    }
}
