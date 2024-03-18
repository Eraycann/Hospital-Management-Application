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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            connect.Open();
            //DOKTORLARI ÇEKME
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * FROM TBL_Doktorlar", connect);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //BRANŞ CMB
            SqlCommand command2 = new SqlCommand("SELECT BransAD FROM TBL_Branslar", connect);
            SqlDataReader dr = command2.ExecuteReader();
            while (dr.Read())
            {
                CmbBrans.Items.Add(dr[0]);
            }
            connect .Close();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("INSERT INTO TBL_Doktorlar (DoktorAD,DoktorSOYAD,DoktorBRANS,DoktorTC,DoktorSIFRE) VALUES (@p1,@p2,@p3,@p4,@p5)",connect );
            command.Parameters.AddWithValue("@p1", TxtAd.Text);
            command.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            command.Parameters.AddWithValue("@p3", CmbBrans.Text);
            command.Parameters.AddWithValue("@p4", MskTC.Text);
            command.Parameters.AddWithValue("@p5", TxtSifre.Text);
            command.ExecuteNonQuery();
            connect .Close();
            MessageBox.Show("Doktor Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command3 = new SqlCommand("DELETE FROM TBL_Doktorlar WHERE DoktorTC=@p1", connect );
            command3.Parameters.AddWithValue("@p1", MskTC.Text);
            command3.ExecuteNonQuery();
            connect .Close();
            MessageBox.Show("Kayıt Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command4 = new SqlCommand("UPDATE TBL_Doktorlar SET DoktorAD=@p1,DoktorSOYAD=@p2,DoktorBRANS=@p3,DoktorTC=@p4,DoktorSIFRE=@p5",connect );
            command4.Parameters.AddWithValue("@p1", TxtAd.Text);
            command4.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            command4.Parameters.AddWithValue("@p3", CmbBrans.Text);
            command4.Parameters.AddWithValue("@p4", MskTC.Text);
            command4.Parameters.AddWithValue("@p5", TxtSifre.Text);
            command4.ExecuteNonQuery();
            connect .Close();
            MessageBox.Show("Doktor Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
