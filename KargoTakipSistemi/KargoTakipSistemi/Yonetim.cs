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
    public partial class Yonetim : Form
    {
        static string conString = "Server=ERHAN; Database =Kargo;Uid=sa;Password=123456";
        SqlConnection veritabani = new SqlConnection(conString);
        SqlCommand komut;   //veri çekme ve gönderme işlemleri için gerekli tanımlamalar yapıldı
        SqlCommand komut1;
        SqlDataAdapter da;
        SqlDataAdapter da1;
        SqlDataAdapter da2;
        SqlDataAdapter da3;
        SqlDataAdapter da4;

        DataTable dt = new DataTable(); //kargo datatable ları
        DataTable dt1 = new DataTable();

        DataTable dt2 = new DataTable();//gönderici datatable ları
        DataTable dt3 = new DataTable();

        DataTable dt4 = new DataTable();//kargo aktarım/teslim datatable ları
        DataTable dt5 = new DataTable();


        //değişken tanımlamaları yapılır
        string gondAdi, gondSoyadi, gondTc, gondTel, gondMail, gondAdres, gondIl, gondIlce;
        string alAdi, alSoyadi, alTelno, alMail, alAdres;
        string kargoKilo, kargoEn, kargoBoy,kargoYuk, kargoIl, kargoIlce, kargoMah;

        private void aktarTakip_TextChanged(object sender, EventArgs e)
        {
            
        }

        bool hizliKargo, yavasKargo;
        string takipNo;
        string sorgu1, sorgu;



        //Kargo nun gönderileceği il seçiminden sonra veritabanından o ile ait ilçeler comboboxlara atanır
        private void kil_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt1.Clear();
            string ilId = kil.SelectedValue.ToString();
            string sorgu1 = "select * from ilceler where sehir=" + ilId + "";
            //kullanıcının girdiği texboxdaki yerleri veritanında arama yapar
            komut1 = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            da1 = new SqlDataAdapter(komut1);//veritanbanı sorgusu çalıştırılır
            da1.Fill(dt1);//datatable çekilen veri yüklenir

            kilce.DataSource = dt1;
            kilce.ValueMember = "id";
            kilce.DisplayMember = "ilce";

            veritabani.Close();
        }


        //Göderici il seçiminden sonra o ile ait ilçeler comboxa atanır
        private void gil_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt3.Clear();
            string ilId1 = gil.SelectedValue.ToString();
            string sorgu1 = "select * from ilceler where sehir=" + ilId1 + "";
            //kullanıcının girdiği texboxdaki yerleri veritanında arama yapar
            komut1 = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            da3 = new SqlDataAdapter(komut1);//veritanbanı sorgusu çalıştırılır
            da3.Fill(dt3);//datatable çekilen veri yüklenir

            gilce.DataSource = dt3;
            gilce.ValueMember = "id";
            gilce.DisplayMember = "ilce";

            veritabani.Close();
        }

        
        //boş event silince hata veriyor
        private void gilce_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        //aktarım veya teslim edilecek il seçiminden sonra o ile ait ilçeler comboxa atanır
        private void aktaril_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt5.Clear();
            veritabani.Open();
            string ilIdsi = aktaril.SelectedValue.ToString();
            string sorgu1 = "select * from ilceler where sehir=" + ilIdsi + "";
            //kullanıcının girdiği texboxdaki yerleri veritanında arama yapar
            komut1 = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            da4 = new SqlDataAdapter(komut1);//veritanbanı sorgusu çalıştırılır
            da4.Fill(dt5);//datatable çekilen veri yüklenir

            aktarilce.DataSource = dt5;
            aktarilce.ValueMember = "id";
            aktarilce.DisplayMember = "ilce";

            veritabani.Close();
        }


        //boş event silince hata veriyor
        private void aktarilce_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public Yonetim()
        {
            InitializeComponent();
            kTakipNo.Hide();
            kVarisSure.Hide();
            kUcret.Hide();

            //kargo il ilçe ayarlamaları
            kil.SelectedIndexChanged -= kil_SelectedIndexChanged;
            veritabani.Open();
            sorgu = "select * from iller";
            //kullanıcının girdiği texboxdaki yerleri veritanında arama yapar
            komut = new SqlCommand(sorgu, veritabani);//sorgu tanımlanır
            da = new SqlDataAdapter(komut);//veritanbanı sorgusu çalıştırılır
            da.Fill(dt);//datatable çekilen veri yüklenir

            kil.DataSource = dt;
            kil.ValueMember = "id";
            kil.DisplayMember = "sehir";

            
            kil.SelectedIndexChanged += kil_SelectedIndexChanged;

            gil.SelectedIndexChanged -= gil_SelectedIndexChanged;
            
            dt1.Clear();


            //gönderici il ilçe ayarlamaları
            sorgu1 = "select * from iller";
            //kullanıcının girdiği texboxdaki yerleri veritanında arama yapar
            komut = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            da2 = new SqlDataAdapter(komut);//veritanbanı sorgusu çalıştırılır
            da2.Fill(dt2);//datatable çekilen veri yüklenir

            gil.DataSource = dt2;
            gil.ValueMember = "id";
            gil.DisplayMember = "sehir";

            
            gil.SelectedIndexChanged += gil_SelectedIndexChanged;


            aktaril.SelectedIndexChanged -= aktaril_SelectedIndexChanged;
            //gönderici il ilçe ayarlamaları
            sorgu1 = "select * from iller";
            //kullanıcının girdiği texboxdaki yerleri veritanında arama yapar
            komut = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            da4 = new SqlDataAdapter(komut);//veritanbanı sorgusu çalıştırılır
            da4.Fill(dt4);//datatable çekilen veri yüklenir

            aktaril.DataSource = dt4;
            aktaril.ValueMember = "id";
            aktaril.DisplayMember = "sehir";

            veritabani.Close();
            aktaril.SelectedIndexChanged += aktaril_SelectedIndexChanged;


            label27.Hide();
            label28.Hide();
            label29.Hide();
            label32.Hide();

        }

        //Kargoyu teslim al butonuna basılırsa
        private void button1_Click(object sender, EventArgs e)
        {
            int tno;//kargo numarası seçici
            string gonderiTipi;
            //gönderici bilgieri texboxtan alınıp değişkenlere atılır
            gondAdi = gAdi.Text;
            gondSoyadi = gSoyadi.Text;
            gondTc = gTc.Text;
            gondTel = gTel.Text;
            gondMail = gMail.Text;
            gondIl = gil.SelectedValue.ToString();
            gondIlce = gilce.SelectedValue.ToString();
            gondAdres = gAdres.Text;

            //Alıcı bilgileri texboxdan alınıp değişkenlere atılır
            alAdi = aAdi.Text;
            alSoyadi = aSoyadi.Text;
            alTelno = aTel.Text;
            alMail = aMail.Text;

            //kargo bilgileri texboxlardan alınıp değişkenlere atılır
            kargoKilo = kKilo.Text;
            kargoEn = kEn.Text;
            kargoBoy = kBoy.Text;
            kargoYuk = kYuk.Text;
            kargoIl = kil.SelectedValue.ToString();
            kargoIlce = kilce.SelectedValue.ToString();
            kargoMah = kadres.Text;

            //kargo gönderme tipi bool tipinde alınır
            hizliKargo = hKargo.Checked;
            yavasKargo = nKargo.Checked;

            Random rnd = new Random();
            int kargoSure;
            double desi;
            //kargo Ucreti Hesaplama
            //hızlı kargo desi*4
            desi = (Int32.Parse(kargoBoy) * Int32.Parse(kargoEn) * Int32.Parse(kargoYuk)) / 3000;
            if (hizliKargo)
            {
                kargoSure = rnd.Next(1, 3);
                if(desi<Int32.Parse(kargoKilo))
                {
                    desi = Int32.Parse(kargoKilo);
                }
                else
                {

                }
                desi *= 4;
            }//normal kargo desi*2
            else
            {
                kargoSure = rnd.Next(3,6);
                if (desi < Int32.Parse(kargoKilo))
                {
                    desi = Int32.Parse(kargoKilo);
                }
                else
                {

                }
                desi *= 2;
            }

            double kargoFiyat=0;
            //kargo fiyat
            if (desi == 1)
                kargoFiyat = 3.99;
            else if (desi > 1 && desi <= 5)
                kargoFiyat = 4.50;
            else if (desi > 5 && desi <= 10)
                kargoFiyat = 4.99;
            else if (desi > 10 && desi <= 15)
                kargoFiyat = 6.50;
            else if (desi > 15 && desi <= 20)
                kargoFiyat = 8.50;
            else if (desi > 20 && desi <= 25)
                kargoFiyat = 10.90;
            else if (desi > 25 && desi <= 30)
                kargoFiyat = 11.90;
            else if (desi > 30 )
                kargoFiyat = desi*0.40;

            string odeme;
            //alıcı veya gönderici odemeli
            if(gOder.Checked)
            {
                odeme = "Gönderici Öder";
            }
            else
            {
                odeme = "Alıcı Öder";
            }

            kUcret.Text = kargoFiyat.ToString() + "TL";
            kUcret.Show();
            kVarisSure.Text = kargoSure.ToString()+" Gün";
            kVarisSure.Show();



            //takip numarası KP + (Gonderen İl kodu)+(Gonderl İlçe Kodu)+(Alıcı İl kodu)+(Alıcı ilçe Kodu)+(hızlı veya yavaş)+(en sonki kargo idsinin 1 fazlası)
            //kargo numaralarında çakışma olmaması için en sondaki id değeri eklendi
            takipNo ="KP"+ gondIl.ToString() + gondIlce.ToString() + kargoIl.ToString() + kargoIlce.ToString();
            if (hizliKargo)
            {//radio button dan seçili değere göre takipnoya string eklemesi yapılır
                takipNo += "F".ToString();
                gonderiTipi = "F";
            }
            else
            {
                takipNo += "S".ToString();
                gonderiTipi = "S";
            }

            veritabani.Open();//veritabanı bağlantısı açılır
            
            

            sorgu1 = "select TOP 1 id from TakipNo order by id desc";//son takip numarasının id si çekilir
            //kargo takip numaraları çakışmaması için numaranın sonuna id yazdırılır
            komut1 = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            SqlDataReader oku = komut1.ExecuteReader();
            if (oku.Read())
            {
                tno = Int32.Parse(oku["id"].ToString());
                tno++;
                takipNo += tno.ToString();
            }
            else
            {
                takipNo += "1";
                tno = 1;
            }
            oku.Close();

            //takip no tabloya eklendi
            sorgu = "insert into TakipNo Values('" + takipNo.ToString() + "')";
            komut = new SqlCommand(sorgu, veritabani);
            komut.ExecuteNonQuery();

            sorgu1 = "select TOP 1 id from TakipNo order by id desc";//son takip numarasının id si çekilir
            //kargo takip numaraları çakışmaması için numaranın sonuna id yazdırılır
            komut1 = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            oku = komut1.ExecuteReader();
            if (oku.Read())
            {
                tno = Int32.Parse(oku["id"].ToString());
            }
            oku.Close();
            kTakipNo.Text = takipNo.ToString();//kargo takip numarası ekranda gösterilir.
            kTakipNo.Show();


            //gonderici tablosuna bilgiler eklendi
            sorgu1 = "insert into Gonderici Values('"+gondAdi+"','"+gondSoyadi+"','"+gondTc+"','"+gondTel+"','"+gondMail+"','"+Int32.Parse(gondIl)+"','"+Int32.Parse(gondIlce)+"','"+gondAdres+"','"+tno+"')";
            komut1 = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            komut1.ExecuteNonQuery();


            //alıcı tablosuna bilgiler eklendi
            sorgu = "insert into Alici Values('" + alAdi + "','" + alSoyadi + "','" + alTelno + "','" + alMail + "','"+tno+"')";
            komut = new SqlCommand(sorgu, veritabani);
            komut.ExecuteNonQuery();

            //gonderiHareketi tablosına bilgiler eklendi
            sorgu = "insert into GonderiHareketi Values('"+DateTime.Now.ToString()+"','Kargo Teslim Alindi','Kargo Kabul edildi','" + gondIl + "','" + gondIlce + "','" + tno+ "')";
            komut = new SqlCommand(sorgu, veritabani);
            komut.ExecuteNonQuery();


            //son eklenen alıcı ve göndrici id yi çekiyoruz
            int alici=0, gonderici=0;
            sorgu1 = "select TOP 1 id from Alici order by id desc";
            komut1 = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            oku = komut1.ExecuteReader();
            if (oku.Read())
                alici = Int32.Parse(oku["id"].ToString());
            oku.Close();

            sorgu1 = "select TOP 1 id from Gonderici order by id desc";
            komut1 = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            oku = komut1.ExecuteReader();
            if (oku.Read())
                gonderici = Int32.Parse(oku["id"].ToString());
            oku.Close();

            //Kargo Bilgisi tablosuna bilgiler eklendi
            sorgu = "insert into KargoBilgisi Values('"+kargoKilo+"','"+kargoEn+"','"+kargoBoy+"','"+kargoYuk+"','"+ Int32.Parse(kargoIl)+ "','" + Int32.Parse(kargoIlce) + "','"+kargoMah+"','"+gonderiTipi+"','"+alici+"','"+gonderici+"','"+tno+"','"+ kargoFiyat.ToString()+ "','"+odeme+"','"+kargoSure+"')";
            komut = new SqlCommand(sorgu, veritabani);
            komut.ExecuteNonQuery();
            veritabani.Close();

            MessageBox.Show("Başarılı. Takip Numarası :"+takipNo.ToString()+" Kargo Ücreti :"+ kUcret .ToString()+ " Tahmini Varış Süresi :"+kVarisSure);
        }


        //Aktarma/Teslim et butonuna basılınca bu event çalışır
        private void aktarmaBtn_Click(object sender, EventArgs e)
        {
            string aktarmaTakipNo, aktarmaIslem, aktarmaAciklama;
            int aktarmaIl, aktarmaIlce;

            aktarmaTakipNo = aktarTakip.Text;
            aktarmaIslem = aktarİslem.Text;
            aktarmaAciklama = aktarAciklama.Text;
            aktarmaIl = Int32.Parse(aktaril.SelectedValue.ToString());
            aktarmaIlce = Int32.Parse(aktarilce.SelectedValue.ToString());

            int takipNoVarmi = 0,takipNo=0;
            veritabani.Open();

            sorgu1 = "select * from TakipNo where Takip='"+aktarmaTakipNo+"'";//son takip numarasının id si çekilir
            //kargo takip numaraları çakışmaması için numaranın sonuna id yazdırılır
            komut1 = new SqlCommand(sorgu1, veritabani);//sorgu tanımlanır
            SqlDataReader oku = komut1.ExecuteReader();
            if (oku.Read())
            {
                takipNoVarmi = 1;
                takipNo = Int32.Parse(oku["id"].ToString());

            }
            else
            {
                takipNoVarmi = 0;
                MessageBox.Show("TakipNo ile Eşleşme Olmadı Tekrar Deneyin");
            }
            oku.Close();

            sorgu = "select * from KargoBilgisi INNER JOIN iller ON [iller].[id]=[KargoBilgisi].[kilId] INNER JOIN ilceler ON [ilceler].[id]=[KargoBilgisi].[kilceId] and  [KargoBilgisi].[kTakipId] = '" + takipNo + "'";
            komut = new SqlCommand(sorgu, veritabani);//sorgu tanımlanır
            oku = komut.ExecuteReader();

            if (oku.Read())
            {
                label27.Text = oku["sehir"].ToString();
                label28.Text = oku["ilce"].ToString();
                label29.Text = oku["kAdres"].ToString();
                label32.Text = oku["kOdemeTipi"].ToString();

                label27.Show();
                label28.Show(); 
                label29.Show();
                label32.Show();
            }
            oku.Close();

            if (takipNoVarmi==1)
            {   
                sorgu = "insert into GonderiHareketi Values('" + DateTime.Now.ToString() + "','" + aktarmaIslem + "','" + aktarmaAciklama + "','" + aktarmaIl + "','" + aktarmaIlce + "','" + takipNo + "')";
                komut = new SqlCommand(sorgu, veritabani);
                komut.ExecuteNonQuery();
            }

            veritabani.Close();

            MessageBox.Show("Aktarma/Teslim İşlemi Yapıldı");
        }

    }
}
