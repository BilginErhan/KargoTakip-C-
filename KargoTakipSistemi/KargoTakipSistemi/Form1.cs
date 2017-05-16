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
    public partial class Form1 : Form
    {
        Yonetim yFrom = new Yonetim();
        KargoTakipEt kForm = new KargoTakipEt();

        string vtKullanici, vtSifre;

        //sql server bağlantısı
        static string conString = "Server=ERHAN; Database =Kargo;Uid=sa;Password=123456";
        SqlConnection veritabani = new SqlConnection(conString);
        SqlCommand komut;   //veri çekme ve gönderme işlemleri için gerekli tanımlamalar yapıldı
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        //eğer kullanıcı kargo takip et butonuna basarsa bu fonksiyona girer
        private void button2_Click(object sender, EventArgs e)
        {
            //KargoTakipEt formu açılır
            kForm.ShowDialog();
            this.Close();
        }

        //Giriş Butonuna basılınca
        private void button1_Click(object sender, EventArgs e)
        {
            veritabani.Open();//veritabanı bağlantası kurulur
            string sorgu = "select * from Kullanıcı where KullaniciAdi=@kAdi and Sifre=@kSifre";
            //kullanıcının girdiği texboxdaki yerleri veritanında arama yapar
            komut = new SqlCommand(sorgu, veritabani);//sorgu tanımlanır
            komut.Parameters.AddWithValue("@kAdi",kAdi.Text);//parametreler tanımlandı
            komut.Parameters.AddWithValue("@kSifre", kSifre.Text);
            da = new SqlDataAdapter(komut);//veritanbanı sorgusu çalıştırılır
            da.Fill(dt);//datatable çekilen veri yüklenir
           
            try
            {//eğer bir hata var ise cath e düşer
                vtKullanici = dt.Rows[0]["KullaniciAdi"].ToString(); //data table daki veriler 
                vtSifre = dt.Rows[0]["Sifre"].ToString();    //değişkenlere atılır
            }
            catch
            {

            }
            

            //kullanıcının girdiği veri veritabanındaki ile eşleşirse yönetim paneli açılır
            if (kAdi.Text == vtKullanici && kSifre.Text == vtSifre)
            {
                yFrom.ShowDialog();
                this.Close();
            }
            else
            {//eğer girilen kullanıcı adı veya şifre uyuşmaz ise hata mesajı verdirilir
                MessageBox.Show("Kullanıcı Adı veya Şifre yanlış tekrar deneyiniz");
                kAdi.Text = ""; //textboxlar temizlenir
                kSifre.Text = "";
            }

            //veritabanı bağlantısı kapatılır
            veritabani.Close();
            
            
        }
    }
}
