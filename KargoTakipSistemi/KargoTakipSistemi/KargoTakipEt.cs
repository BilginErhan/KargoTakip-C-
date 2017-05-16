using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;  //sqlservera bağlanmak için
using System.Data.SqlClient;//gerekli kütüphane tanımlamaları yapılır

namespace KargoTakipSistemi
{
    public partial class KargoTakipEt : Form
    {
        static string conString = "Server=ERHAN; Database =Kargo;Uid=sa;Password=123456";
        SqlConnection veritabani = new SqlConnection(conString);
        SqlCommand komut;   //veri çekme ve gönderme işlemleri için gerekli tanımlamalar yapıldı
        SqlCommand komut1;
        SqlDataAdapter da;
        SqlDataAdapter da1;

        DataTable dt = new DataTable(); //kargo datatable ları
        DataTable dt1 = new DataTable();


        string Takip,sorgu;
        public KargoTakipEt()
        {
            InitializeComponent();
        }
        
        //arama yap denilince bu event çalışır
        private void button1_Click(object sender, EventArgs e)
        {
            veritabani.Close();
            int takipNoDogrumu,takipId=0;
            Takip = TakipNo.Text;//textboxdan alınan veri değişkene atılır
            //ve veritabanı sorgusu yapılır

            veritabani.Open();
            sorgu = "select * from TakipNo where Takip ='" + Takip + "'";

            komut = new SqlCommand(sorgu, veritabani);//sorgu tanımlanır
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                takipNoDogrumu = 1;
                takipId = Int32.Parse(oku["id"].ToString());
            }
            else
            {
                takipNoDogrumu = 0;
                MessageBox.Show("Takip numarasını tekrardan giriniz");
            }
            oku.Close();

            if(takipNoDogrumu==1)
            {
                //inner join ile alıcı ve gönderici tablosu birleştirelerek foreign key ortak olan takip 
                //numarasından veriler çekilir ve texboxa aktarılır
                sorgu = "select * FROM Gonderici INNER JOIN Alici ON [Gonderici].[gTakipId]=[Alici].[aTakipId] and [Gonderici].[gTakipId]='"+ takipId + "'";
                komut = new SqlCommand(sorgu, veritabani);//sorgu tanımlanır
                oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    GondericiAdi.Text = oku["gAdi"].ToString();
                    GondericiSoyadi.Text = oku["gSoyadi"].ToString();
                    GondericiTelNo.Text = oku["gTelNo"].ToString();
                    GondericiMail.Text = oku["gMail"].ToString();

                    AliciAdi.Text = oku["aAdi"].ToString();
                    AliciSoyadi.Text = oku["aSoyadi"].ToString();
                    AliciTelNo.Text = oku["aTelNo"].ToString();
                    AliciMail.Text = oku["aMail"].ToString();
                }
                oku.Close();

                sorgu = "select [GonderiHareketi].[gTarih],[GonderiHareketi].[gIslem],[GonderiHareketi].[gAciklama],[iller].[sehir],[ilceler].[ilce] FROM GonderiHareketi INNER JOIN iller ON [GonderiHareketi].[gilId]=[iller].[id] INNER JOIN ilceler ON [GonderiHareketi].[gilceId]=[ilceler].[id] and [GonderiHareketi].[gTakipId]='" + takipId + "'";

                da = new SqlDataAdapter(sorgu, veritabani);//sorgu tanımlanır
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.Columns[0].HeaderText = "İşlem Tarihi";
                dataGridView1.Columns[1].HeaderText = "Yapılan İşlem";
                dataGridView1.Columns[2].HeaderText = "Açıklama";
                dataGridView1.Columns[3].HeaderText = "İl";
                dataGridView1.Columns[4].HeaderText = "İlçe";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            }
            veritabani.Close();
        }
    }
}
