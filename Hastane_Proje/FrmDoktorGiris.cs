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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=qwerty\SQLEXPRESS;Initial Catalog=HastahaneDB;Integrated Security=True");

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM TBL_Doktorlar WHERE DoktorTC=@p1 and DoktorSIFRE=@p2", connect);
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            command.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = command.ExecuteReader();
            if(dr.Read())
            {
                FrmDoktorDetay doktorDetay = new FrmDoktorDetay();
                doktorDetay.TC = MskTC.Text;
                doktorDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı veya şifre");
            }
            connect.Close();
        }
    }
}
