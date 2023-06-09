using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Görsel_Veritabanı_Yonetimi
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        void MusteriGetir()
        {
            baglanti = new SqlConnection("server=.;Initial Catalog=MusteriSiparis;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("Select *From Musteri", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MusteriGetir();
        }


        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtSehir.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtUlke.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO musteri(Ad,Soyad,Sehir,Ulke,Telefon) VALUES (@Ad,@Soyad,@Sehir,@Ulke,@Telefon)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Ad", txtAd.Text);
            komut.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@Sehir", txtSehir.Text);
            komut.Parameters.AddWithValue("@Ulke", txtUlke.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM musteri where Id=@Id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Id", Convert.ToInt32(txtId.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE musteri SET Ad=@Ad,Soyad=@Soyad,Sehir=@Sehir,Ulke=@Ulke,Telefon=@Telefon WHERE Id=@Id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Id", Convert.ToInt32(txtId.Text));
            komut.Parameters.AddWithValue("@Ad", txtAd.Text );
            komut.Parameters.AddWithValue("@Soyad",txtSoyad.Text );
            komut.Parameters.AddWithValue("@Sehir",txtSehir.Text );
            komut.Parameters.AddWithValue("@Ulke", txtUlke.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }
    }
}
