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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        public string TCNO;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            connect.Open();
            MskTC.Text= TCNO;
            SqlCommand command = new SqlCommand("SELECT * FROM TBL_Doktorlar WHERE DoktorTC=@p1", connect);
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            SqlDataReader dataReader= command.ExecuteReader();
            while(dataReader.Read() )
            {
                TxtAd.Text = dataReader[1].ToString();
                TxtSoyad.Text = dataReader[2].ToString();
                CmbBrans.Text = dataReader[3].ToString();
                TxtSifre.Text = dataReader[5].ToString();
            }
            connect.Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command2 = new SqlCommand("UPDATE TBL_Doktorlar SET DoktorAD=@p1,DoktorSOYAD=@p2,DoktorBRANS=@p3,DoktorSIFRE=@p4,DoktorTC=@p5", connect );
            command2.Parameters.AddWithValue("@p1",TxtAd.Text);
            command2.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            command2.Parameters.AddWithValue("@p3",CmbBrans.Text);
            command2.Parameters.AddWithValue("@p4",TxtSifre.Text);
            command2.Parameters.AddWithValue("@p5", MskTC.Text);
            command2.ExecuteNonQuery();
            connect .Close();
            MessageBox.Show("Kayıt Güncellendi.");
        }
    }
}
