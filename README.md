# 🏋️‍♂️ Champion's Arena - Gym Management System

**Champion's Arena**, spor salonlarının (Gym) ve spor kulüplerinin tüm idari, operasyonel ve analitik süreçlerini dijitalleştirmek amacıyla geliştirilmiş, **Nesne Yönelimli Programlama (OOP)** mimarisine dayanan modern bir masaüstü (Desktop) otomasyon yazılımıdır.

Bu proje, standart Windows Form bileşenlerinin görsel kısıtlamalarını aşmak için **DevExpress Fluent Design System** kullanılarak tasarlanmış olup, "Karanlık Mod (Dark Theme)" desteğiyle birinci sınıf bir kullanıcı deneyimi (UX) sunar.

---

## ✨ Öne Çıkan Özellikler

Sistem 3 ana modül ve 1 raporlama motorundan oluşmaktadır:

### 1. 🥇 Milli Sporcu Yönetim Modülü
* Profesyonel ve lisanslı sporcuların kayıt altına alındığı modüldür.
* **Gelişmiş Veri Listeleme:** DevExpress `GridControl` kullanılarak anlık arama (Find Panel), sütun bazlı filtreleme ve gruplama (Örn: Branşa göre gruplama) yapılabilir.
* **OOP Entegrasyonu:** Tüm veriler `MilliSporcu` sınıfından türetilen nesneler olarak `BindingList<T>` koleksiyonlarında yönetilir.

### 2. 👥 Standart Üye ve Paket Yönetimi
* Salon üyelerinin kayıt, takip ve finansal paket işlemlerini kapsar.
* **Otomatik Tarih Algoritması:** Kullanıcı "3 Aylık" veya "6 Aylık" paket seçtiğinde, sistem arka planda `AddMonths()` metodunu çalıştırarak üyelik bitiş tarihini otomatik ve hatasız hesaplar. İnsan kaynaklı hesaplama hatalarını sıfıra indirir.

### 3. 🧬 Profesyonel Vücut Analiz İstasyonu (BMI)
* Üyelerin sağlık durumlarını analiz eden Karar Destek Sistemidir (Decision Support System).
* **Akıllı Analiz:** Girilen Boy ve Kilo verileri arka planda Vücut Kitle İndeksi formülünden geçer. Çıkan sonuca göre bir Karar Ağacı (Decision Tree) çalışır ve ekranda "ZAYIF", "FİT", "KİLOLU" gibi sonuçlar renk kodlamalarıyla (Yeşil/Kırmızı) gösterilir.

### 4. 📄 Dinamik Raporlama ve Çıktı Alma
* Sistemdeki `GridControl` üzerinde listelenen tüm veriler, tek tıklamayla **PDF, Excel (XLSX) veya HTML** formatında dışa aktarılabilir (`ExportToPdf`).

---

## 🛠️ Kullanılan Teknolojiler ve Mimari

* **Programlama Dili:** C# 10.0
* **Framework:** Microsoft .NET Framework 4.8
* **Kullanıcı Arayüzü (UI):** DevExpress v24.1 (Fluent Design System, Accordion Control, Skin Controller - The Bezier / Macallan Dark)
* **Veri Yönetimi:** In-Memory Object-Oriented Database (`BindingList<T>`)
* **Geliştirme Ortamı:** Visual Studio 2022 Enterprise (macOS üzerinde UTM Sanal Makine / Windows 11 ARM emülasyonu ile geliştirilmiştir.)

---

## 🚀 Kurulum ve Çalıştırma

Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyin:

### Ön Koşullar
* Bilgisayarınızda **Visual Studio 2019 veya 2022** yüklü olmalıdır.
* Projenin arayüzünün derlenebilmesi için sisteminizde **DevExpress WinForms kütüphanelerinin (v24.1 veya uyumlu bir sürümü)** kurulu ve lisanslanmış (veya deneme sürümü) olması gerekmektedir.

### Adımlar
1. Projeyi bilgisayarınıza klonlayın:
   ```bash
   git clone [https://github.com/KULLANICI_ADIN/champions-arena.git](https://github.com/KULLANICI_ADIN/champions-arena.git)
