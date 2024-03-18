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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        private void FrmBrans_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_Branslar",connect );
            da.Fill(dt);
            dataGridView1.DataSource= dt;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command= new SqlCommand("INSERT INTO TBL_Branslar (BransAD) VALUES (@p1)",connect );
            command.Parameters.AddWithValue("@p1", TxtBrans.Text); 
            command.ExecuteNonQuery();
            connect .Close();
            MessageBox.Show("Branş Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command2 = new SqlCommand("DELETE FROM TBL_Branslar WHERE BransID=@p1", connect );
            command2.Parameters.AddWithValue("@p1",TxtID.Text); 
            command2.ExecuteNonQuery();
            connect .Close();
            MessageBox.Show("Branş Silindi.");
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command3 = new SqlCommand("UPDATE TBL_Branslar SET BransAD=@p1 WHERE BransID=@p2",connect );
            command3.Parameters.AddWithValue("@p1",TxtBrans.Text);
            command3.Parameters.AddWithValue("@p2", TxtID.Text);
            command3.ExecuteNonQuery();
            connect .Close();
            MessageBox.Show("Branş Güncellendi.");
        }
    }
}
