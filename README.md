# Veritabanı Yönetim Uygulaması (Windows Forms)

Veritabanı yönetimi ile ilgilenen kullanıcılar için tasarlanmış bir yazılımdır. Bu yazılım, kullanıcılara veritabanı yönetim işlemlerini daha kolay, hızlı ve verimli bir şekilde gerçekleştirmelerini sağlar.

Program, kullanıcıların kendilerine ait veritabanlarına kayıt ekleme, silme, güncelleme işlemlerini yapmalarına olanak tanır.

Bu program, kullanıcı dostu bir arayüze sahiptir ve kolay kullanımı sayesinde kullanıcıların zamanını ve emeğini tasarruf etmelerine yardımcı olur.

Bu yazılım, işletmeler, öğrenciler, araştırmacılar, geliştiriciler ve diğer veritabanı yönetimi ile ilgilenen herkes için faydalı olacaktır. Ayrıca veritabanı yönetimi işlemlerini daha kolay ve verimli bir şekilde gerçekleştirebilirsiniz.

# Proje Yapım Aşaması
Kodlarımıza geçmeden önce;
Sql server bağlantısı için;

```bash
using System.Data.SqlClient;
```
eklememiz gerektiğini hatırlatalım.

Öncelikle SqlConnection, SqlDataAdapter ve SqlCommand nesnelerimizi tekrar tekrar tanımlamamak için Public olarak tanımlıyoruz.
![](https://i.hizliresim.com/b8frrxw.png)

Verilerimizi çekme işlemini de bir kaç yerde kullanacağımız için MusteriGetir isimli bir fonksiyon tanımlıyoruz.

```bash
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
```
Oluşturmuş olduğumuz bu metodu Form ilk açıldığında çalıştırmak için Form_Load olayına ekliyoruz. Bu sayede Form açılır açılmaz Verilerimizin DataGridView‘ de görüntülenmesi sağlanacaktır.

```bash
          private void Form1_Load(object sender, EventArgs e)
        {
            MusteriGetir();
        }
```
## Form Tasarım ve Düzenleme
Kullanacağınız veritabanının içinde bulunan bilgilerden hangilerinin görünmesini ve düzenlenmesini istiyorsanız onları kullanabilirsiniz.

![](https://i.hizliresim.com/57mf0jm.png)

Benim kullanacağım bilgiler bunlar. Ayrıca DataGridView 'de görevler kısmından eklemeyi,düzenlemeyi,silmeyi etkinleştir kısmındaki tikleri kaldırmalıyız. Çünkü tablo üzerinde herhangi bir değişiklik yapmıyacağız. (Buton özellikleri dışında)

Şimdi DataGridView 'deki bilgilerin ilgili alanlara gelmesi için her bir TextBox 'a (Name) atamalarını yapıyoruz.
![](https://i.hizliresim.com/ow2335l.png)

Örneğin ben Müsteri Id kısmındaki TextBox 'ın Name 'ini txtId şeklinde yaptım. Butonlar dahil olmak üzere hepsini değiştiriyoruz.

DataGridView 'de bir satırı seçtiğimizde satırın tamamını işaretlemesi içinde özelliklerden " _SelectionMode_ "kısmını FullRowSelect olarak değiştiriyoruz.

İlgili alanlara bilgilerin gelmesi için CellEnter olayını yapmamız lazım. Öncelikle DataGridView özelliklerinden Events kısmından CellEnter 'e çift tıklamamız gerekli.
![](https://i.hizliresim.com/bwdxqat.png)

Oluşan **dataGridView1_CellEnter** fonksiyonunu aşağıdaki gibi düzenliyoruz. Burada yaptığımız işlem DataGridView kontrolündeki seçilen satırın belirli hücrelerinin değerlerini TextBox kontrolüne atayarak, kullanıcıya bu verileri düzenleme veya görüntüleme imkanı sağlıyor.

![](https://i.hizliresim.com/b4t8lr0.png)

![veriler](https://github.com/mertbilger/Windows-Forms-ile-Veritabani-Yonetim-Arayuzu/assets/93099933/60750318-9ec5-4faa-b0b1-91a265310c01)


## Buton İşlemleri
### Ekleme İşlemi
Öncelikle ekle butonumuza çift tıklayarak fonksiyonu oluşturuyoruz.
Aşağıdaki kod kullanıcının girdiği müşteri bilgilerini alarak, bu bilgileri bir SQL INSERT sorgusuyla veritabanına ekler. Ardından veritabanındaki müşteri verilerini güncellemek için **MusteriGetir** fonksiyonunu çağırır.
```bash
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
```

![ekle](https://github.com/mertbilger/Windows-Forms-ile-Veritabani-Yonetim-Arayuzu/assets/93099933/4085f657-49c8-4e01-b869-3f72a2860e52)


### Silme İşlemi

Sil butonuna çift tıklayarak fonksiyonumuzu oluşturuyoruz. Burada "Id" değerini kullanarak veritabanından bir müşteri kaydını siliyoruz. İlgili "Id" değeri, "txtId" TextBox kontrolünden alınıyor. Daha sonra, müşteri verilerini güncellemek için **MusteriGetir** fonksiyonunu çağrıyoruz.
```
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
```
- **komut.Parameters.AddWithValue("@Ad", txtAd.Text);:** SqlCommand nesnesine, "@Ad" parametresiyle "txtAd" TextBox kontrolündeki metni eklemek için AddWithValue metodu kullanılıyor. Diğer TextBox kontrollerindeki metinler de benzer şekilde ilgili parametrelerle eşleştiriliyor.

![sil](https://github.com/mertbilger/Windows-Forms-ile-Veritabani-Yonetim-Arayuzu/assets/93099933/198a4e44-e3db-4c1a-a321-2077eecdb1cb)


### Güncelleme İşlemi
Güncelle butonuna çift tıklayarak fonksiyonumuzu oluşturuyoruz. Burada kullanıcının girdiği müşteri bilgilerini alarak, belirli bir "Id" değerine sahip müşteri kaydını güncellemek için bir SQL UPDATE sorgusu kullanıyoruz. Daha sonra veritabanındaki müşteri verilerini güncellemek için **MusteriGetir** fonksiyonunu çağrıyoruz.

- **string sorgu = "UPDATE musteri SET Ad=@Ad,Soyad=@Soyad,Sehir=@Sehir,Ulke=@Ulke,Telefon=@Telefon WHERE Id=@Id";:** SQL sorgusunu tanımlayan bir dize oluşturuluyor. Bu sorgu, "musteri" adlı tablodaki bir müşteri kaydının "Id" sütununda belirtilen değeri kullanarak "Ad", "Soyad", "Sehir", "Ulke" ve "Telefon" sütunlarını güncellemek için kullanılıyor. Güncellenecek kaydın "Id" değeri, daha sonra parametre aracılığıyla sağlanacak.
- **komut = new SqlCommand(sorgu, baglanti);:** SqlCommand sınıfından yeni bir nesne oluşturuluyor. Oluşturulan SqlCommand nesnesi, sorguyu ve bağlantıyı temsil ediyor.
- **komut.Parameters.AddWithValue("@Id", Convert.ToInt32(txtId.Text));:** SqlCommand nesnesine, "@Id" parametresiyle "txtId" TextBox kontrolündeki metni, Integer türüne dönüştürerek eklemek için AddWithValue metodu kullanılıyor. Bu, güncellenecek müşterinin "Id" değerini temsil ediyor.
- **komut.Parameters.AddWithValue("@Ad", txtAd.Text );:** SqlCommand nesnesine, "@Ad" parametresiyle "txtAd" TextBox kontrolündeki metni eklemek için AddWithValue metodu kullanılıyor. Diğer TextBox kontrollerindeki metinler de benzer şekilde ilgili parametrelerle eşleştiriliyor.
- **baglanti.Open();:** Veritabanı bağlantısı açılıyor.
- **komut.ExecuteNonQuery();:** SqlCommand nesnesi kullanılarak SQL sorgusu veritabanında çalıştırılıyor. UPDATE sorgusu, belirtilen "Id" değerine sahip müşteri kaydının "Ad", "Soyad", "Sehir", "Ulke" ve "Telefon" sütunlarını günceller.
- **baglanti.Close();:** Veritabanı bağlantısı kapatılıyor.
- **MusteriGetir();:**
```
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
````

![güncelle](https://github.com/mertbilger/Windows-Forms-ile-Veritabani-Yonetim-Arayuzu/assets/93099933/aca272b3-c83e-4a09-902c-8b78313db09d)

