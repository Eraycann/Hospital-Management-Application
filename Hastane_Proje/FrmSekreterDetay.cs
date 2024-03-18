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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string TCnumara;
        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {

            LblTC.Text = TCnumara;

            //AD SOYAD
            connect.Open();
            SqlCommand command = new SqlCommand("SELECT SekreterADSOYAD FROM TBL_Sekreter WHERE SekreterTC=@p1",connect );
            command.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader dataReader= command.ExecuteReader();
            while(dataReader.Read())
            {
                LblAdSoyad.Text = dataReader[0].ToString();
            }
            connect .Close();

            //BRANŞ
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT BransAD FROM TBL_Branslar", connect );
            da.Fill(dt);
            dataGridView1.DataSource= dt;

            //DOKTORLAR
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAD + ' ' + DoktorSOYAD) AS 'Doktorlar',DoktorBRANS FROM TBL_Doktorlar", connect );
            da2.Fill(dt2);
            dataGridView2.DataSource= dt2;

            //BRANŞ CMB
            connect.Open();
            SqlCommand command3 = new SqlCommand("SELECT BransAD FROM TBL_Branslar", connect );
            SqlDataReader dr = command3.ExecuteReader();
            while (dr.Read())
            {
                CmbBrans.Items.Add(dr[0]);
            }
            connect .Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command2 = new SqlCommand("INSERT INTO TBL_Randevu (RandevuTARIH,RandevuSAAT,RandevuBRANS,RandevuDOKTOR) VALUES (@p1,@p2,@p3,@p4)", connect );
            command2.Parameters.AddWithValue("@p1",MskTarih.Text);
            command2.Parameters.AddWithValue("@p2", MskSaat.Text);
            command2.Parameters.AddWithValue("@p3", CmbBrans.Text);
            command2.Parameters.AddWithValue("@p4", CmbDoktor.Text);
            command2.ExecuteNonQuery();
            connect .Close();
            MessageBox.Show("Randevu Oluşturuldu");
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            connect.Open();
            CmbDoktor.Items.Clear();

            SqlCommand command4 = new SqlCommand("SELECT DoktorAD,DoktorSOYAD FROM TBL_Doktorlar WHERE DoktorBRANS=@p1",connect );
            command4.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader da3 = command4.ExecuteReader();
            while (da3.Read())
            {
                CmbDoktor.Items.Add(da3[0] + " " + da3[1]);
            }
            connect .Close();
        }

        private void BtnDuyuruOluştur_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command5 = new SqlCommand("INSERT INTO TBL_Duyurular (Duyuru) Values (@p1)", connect );
            command5.Parameters.AddWithValue("@p1", RchDuyuru.Text);
            command5.ExecuteNonQuery();
            connect .Close();
            MessageBox.Show("Duyuru Oluşturuldu.");
        }

        private void BtnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli doktorPaneli = new FrmDoktorPaneli();
            doktorPaneli.Show();

        }

        private void BtnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBrans brans = new FrmBrans();
            brans.Show();
        }

        private void BtnListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListe randevuListe = new FrmRandevuListe();
            randevuListe.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular duyurular = new FrmDuyurular();
            duyurular.Show();
        }
    }
}
