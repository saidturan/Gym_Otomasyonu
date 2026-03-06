using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GymOtomasyonu
{
    public class BmiAnalizModulu : XtraUserControl
    {
        public BmiAnalizModulu()
        {
            this.Appearance.BackColor = Color.FromArgb(28, 28, 28);
            this.Dock = DockStyle.Fill;

            PanelControl pnl = new PanelControl
            {
                Size = new Size(650, 600),
                Location = new Point(50, 30),
                Appearance = { BackColor = Color.FromArgb(38, 38, 42) },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            };

            LabelControl lblTitle = new LabelControl
            {
                Text = "PROFESYONEL VÜCUT ANALİZ SİSTEMİ",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Gold,
                Location = new Point(30, 25)
            };

            TextEdit txtBoy = new TextEdit { Size = new Size(280, 40), Location = new Point(30, 80), Properties = { NullValuePrompt = "Boy (cm) - Örn: 180" } };
            TextEdit txtKilo = new TextEdit { Size = new Size(280, 40), Location = new Point(30, 130), Properties = { NullValuePrompt = "Kilo (kg) - Örn: 75" } };

            TextEdit txtBel = new TextEdit { Size = new Size(280, 40), Location = new Point(330, 80), Properties = { NullValuePrompt = "Bel Çevresi (cm)" } };
            TextEdit txtKalca = new TextEdit { Size = new Size(280, 40), Location = new Point(330, 130), Properties = { NullValuePrompt = "Kalça Çevresi (cm)" } };
            TextEdit txtBoyun = new TextEdit { Size = new Size(280, 40), Location = new Point(30, 180), Properties = { NullValuePrompt = "Boyun Çevresi (cm)" } };
            ComboBoxEdit cmbAktivite = new ComboBoxEdit { Size = new Size(280, 40), Location = new Point(330, 180) };
            cmbAktivite.Properties.Items.AddRange(new string[] { "Hareketsiz", "Az Hareketli", "Aktif", "Çok Aktif" });
            cmbAktivite.SelectedIndex = 2;

            SimpleButton btnHesapla = new SimpleButton
            {
                Text = "DETAYLI ANALİZİ BAŞLAT",
                Size = new Size(580, 50),
                Location = new Point(30, 240),
                Appearance = { BackColor = Color.DeepSkyBlue, Font = new Font("Segoe UI", 12, FontStyle.Bold) }
            };

            PanelControl pnlSonuc = new PanelControl
            {
                Size = new Size(580, 220),
                Location = new Point(30, 310),
                Visible = false,
                Appearance = { BackColor = Color.FromArgb(45, 45, 48) }
            };
            LabelControl lblSonuc = new LabelControl { AutoSizeMode = LabelAutoSizeMode.Vertical, Size = new Size(540, 180), Location = new Point(20, 20), Font = new Font("Segoe UI", 11) };
            pnlSonuc.Controls.Add(lblSonuc);

            btnHesapla.Click += (s, e) => {
                try
                {
                    double boy = double.Parse(txtBoy.Text) / 100;
                    double kilo = double.Parse(txtKilo.Text);
                    double bmi = kilo / (boy * boy);
                    pnlSonuc.Visible = true;

                   
                    string ekBilgi = $"\n\nAnaliz Notu: Bel ({txtBel.Text}cm) ve Kalça ({txtKalca.Text}cm) ölçümleriniz veri havuzuna kaydedildi.";

                    if (bmi < 23)
                    {
                        lblSonuc.Text = $"BMI: {bmi:F1} - KİLO ALMA PROGRAMI\n\nTavsiye: Hemen günlük kalori saymaya başla. Günlük minimum 3000 kalori sınırı! Kardiyo antrenmanları yasak. Patlayıcı gölge boksu başlanacak 2x5dk ve testosteron artması için alt bacak egzersizleri haftada en az 2'ye çıkacak. Şu hareketlerde PR denemesi yap: Squat, Bench Press ve Deadlift..{ekBilgi}";
                        lblSonuc.ForeColor = Color.LightSkyBlue;
                    }
                    else if (bmi >= 23 && bmi <= 26)
                    {
                        lblSonuc.Text = $"BMI: {bmi:F1} - İDEAL FORM\n\nTavsiye: Kilon Güzel! Haftada 2 gün off-day.Off day'lerden birisi cardio günü olarak ayarlanmalı. Bu süreçte yağ yakarken kas kazanımı en verimli halindedir. Grabbling ve Striking günlerinde tükeniş antrenmanlarına dikkat. Kardiyona ve yağ oranını düşürmeye devam..{ekBilgi}";
                        lblSonuc.ForeColor = Color.LimeGreen;
                    }
                    else
                    {
                        lblSonuc.Text = $"BMI: {bmi:F1} - ZAYIFLAMA PROGRAMI\n\nTavsiye: 8 veya 15 derece eğimli rampada en az 30 dakikadan kardiyolara başla, Heavybag'de 3 set tükeniş striking lower-upper body karışık, Gölge boksu 3x5dk tükeniş, Grabbling günlerinde antrenman sonra ekstra düz (eğimsiz) koşu bandı 30dk..{ekBilgi}";
                        lblSonuc.ForeColor = Color.Tomato;
                    }
                }
                catch { XtraMessageBox.Show("Lütfen Boy ve Kilo alanlarını doldurun!"); }
            };

            pnl.Controls.AddRange(new Control[] { lblTitle, txtBoy, txtKilo, txtBel, txtKalca, txtBoyun, cmbAktivite, btnHesapla, pnlSonuc });
            this.Controls.Add(pnl);
        }
    }
}