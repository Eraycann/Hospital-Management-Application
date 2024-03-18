using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Proje
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;

        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            //AD-SOYAD ÇEKME
            connect.Open();
            SqlCommand command = new SqlCommand("SELECT HastaAD, HastaSOYAD FROM TBL_Hastalar WHERE HastaTC=@p1", connect );
            command.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader dr = command.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1]; //0 ve 1 olmasının sebebi isim soyisim alması
            }
            connect .Close();

            //RANDEVU GEÇMİŞİ
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_Randevu WHERE HastaTC=" + tc,connect );
            //dataadapter parametre almadığı için bu şekilde yaptık
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //BRANŞ ÇEKME
            connect.Open();
            SqlCommand command2 = new SqlCommand("SELECT bransAD FROM TBL_Branslar", connect );
            SqlDataReader dr2 = command2.ExecuteReader();
            while(dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            connect.Open();
            CmbDoktor.Items.Clear();
            SqlCommand command3= new SqlCommand("SELECT DoktorAD,DoktorSOYAD FROM TBL_Doktorlar WHERE DoktorBRANS=@p1",connect );
            command3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = command3.ExecuteReader();
            while(dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            connect .Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt2= new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_Randevu WHERE RandevuBRANS='" + CmbBrans.Text + "'" + " and RandevuDoktor = '" + CmbDoktor.Text + "' and RandevuDurum=0", connect );
            da.Fill(dt2);
            dataGridView2.DataSource= dt2;
        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBilgiDuzenle bilgiDuzenle = new FrmHastaBilgiDuzenle();
            bilgiDuzenle.TCno = LblTC.Text;
            bilgiDuzenle.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            TxtID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command4 = new SqlCommand("UPDATE TBL_Randevular SET RandevuDURUM=1,HastaTC=@p1,HastaSİKAYET=@p2 WHERE RandevuID=@p3", connect );
            command4.Parameters.AddWithValue("@p1",LblTC.Text);
            command4.Parameters.AddWithValue("@p2", RchSikayet.Text);
            command4.Parameters.AddWithValue("@p3", TxtID.Text);
            connect .Close();
            MessageBox.Show("Randevu Alındı","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
