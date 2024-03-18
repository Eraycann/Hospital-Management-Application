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
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        private void BtnKayıtYap_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("INSERT INTO TBL_Hastalar (HastaAD, HastaSOYAD, HastaTC,HastaTELEFON, HastaSIFRE, HastaCINSIYET) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)",connect );

            command.Parameters.AddWithValue("@p1", TxtAd.Text);
            command.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            command.Parameters.AddWithValue("@p3", MskTC.Text);
            command.Parameters.AddWithValue("@p4", MskTelefon.Text);
            command.Parameters.AddWithValue("@p5", TxtSifre.Text);
            command.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);
            command.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Kaydınız Gerçekleşmiştir. Şifreniz : " +TxtSifre.Text, "Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
