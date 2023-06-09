# Veritabanı Yönetim Uygulaması (Windows Forms)

Veritabanı yönetimi ile ilgilenen kullanıcılar için tasarlanmış bir yazılımdır. Bu yazılım, kullanıcılara veritabanı yönetim işlemlerini daha kolay, hızlı ve verimli bir şekilde gerçekleştirmelerini sağlar.

Program, kullanıcıların kendilerine ait veritabanlarına kayıt ekleme, silme, güncelleme işlemlerini yapmalarına olanak tanır.

Bu program, kullanıcı dostu bir arayüze sahiptir ve kolay kullanımı sayesinde kullanıcıların zamanını ve emeğini tasarruf etmelerine yardımcı olur.

Bu yazılım, işletmeler, öğrenciler, araştırmacılar, geliştiriciler ve diğer veritabanı yönetimi ile ilgilenen herkes için faydalı olacaktır. Ayrıca veritabanı yönetimi işlemlerini daha kolay ve verimli bir şekilde gerçekleştirebilirsiniz.

# Proje Yapım Aşaması

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
