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
    public partial class FrmHastaBilgiDuzenle : Form
    {
        public FrmHastaBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string TCno;

        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        private void FrmHastaBilgiDuzenle_Load(object sender, EventArgs e)
        {
            connect.Open();
            MskTC.Text= TCno;
            SqlCommand command = new SqlCommand("SELECT * FROM TBL_Hastalar WHERE HastaTC@p1",connect );
            command.Parameters.AddWithValue("@p1",MskTC.Text);
            SqlDataReader dr = command.ExecuteReader();
            while(dr.Read())
            {
                //dr 0 çünkü id kolonumuz 0.index
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                mskTelefon.Text = dr[3].ToString();
                TxtSifre.Text = dr[4].ToString();
                CmbCinsiyet.Text = dr[5].ToString();
            }
            connect .Close();
        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command2 = new SqlCommand("UPDATE TBL_Hastalar SET HastaAD=@p1,HastaSOYAD=@p2,HastaTELEFON=@p3,HastaSIFRE=@p4,HastaCINSIYET=@p5 WHERE HastaTC=@p6", connect );
            command2.Parameters.AddWithValue("@p1",TxtAd.Text);
            command2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            command2.Parameters.AddWithValue("@p3", mskTelefon.Text);
            command2.Parameters.AddWithValue("@p4", TxtSifre.Text);
            command2.Parameters.AddWithValue("@p1", CmbCinsiyet.Text);
            command2.Parameters.AddWithValue("@p1", MskTC.Text);
            command2.ExecuteNonQuery();
            connect .Close();
            MessageBox.Show("Bilgileriniz Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
