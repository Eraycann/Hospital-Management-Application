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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        public string TC;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            connect.Open();
            LblTC.Text = TC;

            //Doktor Ad Soyad
            SqlCommand command = new SqlCommand("SELECT DoktorAD,DoktorSOYAD FROM TBL_Doktorlar WHERE DoktorTC=@p1",connect );
            command.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                LblAdSoyad.Text = dataReader[0] + " " + dataReader[1];
            }
            connect .Close();

            //Randevular
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_Randevu WHERE RandevuDOKTOR='" + LblAdSoyad.Text + "'", connect );
            da.Fill(dt);
            dataGridView1.DataSource= dt;
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle bilgiDuzenle = new FrmDoktorBilgiDuzenle();
            bilgiDuzenle.TCNO = LblTC.Text;
            bilgiDuzenle.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular duyurular = new FrmDuyurular();
            duyurular.Show();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            RchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
