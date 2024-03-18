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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM TBL_Sekreter WHERE SekreterTC=@p1 AND SekreterSIFRE=@p2",connect );
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            command.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = command.ExecuteReader();
            if(dr.Read()) 
            {
                FrmSekreterDetay sekreterDetay = new FrmSekreterDetay();
                sekreterDetay.TCnumara = MskTC.Text;
                sekreterDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC & Şifre");
            }
            connect .Close();
        }
    }
}
