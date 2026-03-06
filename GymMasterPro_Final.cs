using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.
FluentDesignSystem;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraCharts;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGauges.Win.
Gauges.Circular;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.LookAndFeel;
using System.Data.SQLite;
using GymOtomasyonu;

namespace GymOtomasyonu
{

    public partial class GymMainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private FluentDesignFormContainer mainContainer;
        private AccordionControl accordionMenu;
        private FluentDesignFormControl mainFormControl;

        public GymMainForm()
        {
            InitializeComponent();
            UserLookAndFeel.Default.
SetSkinStyle("The Bezier", "Macallan");

            ShowDashboard();
        }

        private void InitializeComponent()
        {
            this.mainContainer = new FluentDesignFormContainer();
            this.accordionMenu = new AccordionControl();
            this.mainFormControl = new FluentDesignFormControl();
           
            this.mainContainer.Dock = DockStyle.Fill;
            this.accordionMenu.Dock = DockStyle.Left;
            this.accordionMenu.Width = 260;
            this.accordionMenu.ViewType = AccordionControlViewType.
HamburgerMenu;


            var groupMain = new AccordionControlElement(
ElementStyle.Group) { Text = "ANA PANEL" };

            var itemDash = new AccordionControlElement(
ElementStyle.Item) { Text = "Dashboard (Analiz)" };

            itemDash.Click += (s, e) => ShowDashboard();

            var groupUye = new AccordionControlElement(
ElementStyle.Group) { Text = "ÜYE İŞLEMLERİ" };

            var itemUyeKayit = new AccordionControlElement(
ElementStyle.Item) { Text = "Yeni Üye Kaydı" };

            itemUyeKayit.Click += (s, e) => ShowUyeKayit();
            var itemUyeSorgu = new AccordionControlElement(
ElementStyle.Item) { Text = "TC Sorgu & Antrenör" };

            itemUyeSorgu.Click += (s, e) => ShowUyeSorgu();

            var groupMilli = new AccordionControlElement(
ElementStyle.Group) { Text = "MİLLİ SPORCULAR" };

            var itemMilliGiris = new AccordionControlElement(
ElementStyle.Item) { Text = "Şampiyon Girişi" };

            itemMilliGiris.Click += (s, e) => ShowMilliGiris();

            var groupFinans = new AccordionControlElement(
ElementStyle.Group) { Text = "FİNANSAL" };

            var itemOdeme = new AccordionControlElement(
ElementStyle.Item) { Text = "Ödemeler" };

            itemOdeme.Click += (s, e) => ShowOdemeTablosu();

            groupMain.Elements.Add(
itemDash);
            var itemBmi = new AccordionControlElement(ElementStyle.Item) { Text = "Hızlı BMI Ölçümü" };
itemBmi.Click += (s, e) => { mainContainer.Controls.Clear(); var b = new BmiAnalizModulu(); b.Dock = DockStyle.Fill; mainContainer.Controls.Add(b); };
groupUye.Elements.Add(itemBmi);
            groupUye.Elements.AddRange(new[] { itemUyeKayit, itemUyeSorgu });
            groupMilli.Elements.Add(itemMilliGiris);
            groupFinans.Elements.Add(itemOdeme);
            this.accordionMenu.Elements.AddRange(new[] { groupMain, groupUye, groupMilli, groupFinans });

            var itemTumListe = new AccordionControlElement(ElementStyle.Item) { Text = "Tüm Sporcu Listesi" };
            itemTumListe.Click += (s, e) => ShowTumListe();
            groupUye.Elements.Add(itemTumListe); 

            this.ControlContainer = this.mainContainer;
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.accordionMenu);
            this.Controls.Add(this.mainFormControl);

            this.FluentDesignFormControl = this.mainFormControl;
            this.Size = new Size(1250, 850);
            this.Text = "GYM MASTER PRO | PREMIUM";
        }

        private void ShowDashboard() { mainContainer.Controls.Clear()
; var d = new PremiumDashboard(); d.Dock = DockStyle.Fill; mainContainer.Controls.Add(d); }
        private void ShowMilliGiris() { mainContainer.Controls.Clear(); var m = new MilliSporcuModulu(); m.Dock = DockStyle.Fill; mainContainer.Controls.Add(m); }
        private void ShowOdemeTablosu() { mainContainer.Controls.Clear(); var o = new FinalModules(); o.ShowPayments(); o.Dock = DockStyle.Fill; mainContainer.Controls.Add(o); }
        private void ShowUyeSorgu() { mainContainer.Controls.Clear(); var u = new UyeSorguModulu(); u.Dock = DockStyle.Fill; mainContainer.Controls.Add(u); }
        private void ShowTumListe() { mainContainer.Controls.Clear(); var t = new TumSporcularModulu(); t.Dock = DockStyle.Fill; mainContainer.Controls.Add(t); }
        private void ShowUyeKayit() { mainContainer.Controls.Clear(); var k = new UyeKayitModulu(); k.Dock = DockStyle.Fill; mainContainer.Controls.Add(k); }
    }

   
    

    public class PremiumDashboard : XtraUserControl
    {
        public PremiumDashboard()
        {
            this.Appearance.BackColor = Color.FromArgb(28, 28, 28);
            TableLayoutPanel layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Padding = new Padding(10) };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

            int toplamUye = 0, milliSporcu = 0;
            decimal toplamKazanc = 0;

            using (var conn = SqlManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Uyeler", conn)) toplamUye = Convert.ToInt32(cmd.
ExecuteScalar());

                using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Uyeler WHERE IsMilli = 1", conn)) milliSporcu = Convert.ToInt32(cmd.
ExecuteScalar());

                using (var cmd = new SQLiteCommand("SELECT SUM(Tutar) FROM Odemeler", conn)) {
                    var res = cmd.ExecuteScalar();
                    toplamKazanc = res != DBNull.Value ? Convert.ToDecimal(res) : 0;
                }
            }

            PanelControl p1 = CreateCard("TOPLAM GELİR ANALİZİ", Color.Cyan);
            GaugeControl gc = new GaugeControl { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            CircularGauge gauge = gc.AddCircularGauge();
            gauge.AddDefaultElements();
            gauge.Scales[0].MaxValue = 50000;
            gauge.Scales[0].Value = (float)toplamKazanc;
            p1.Controls.Add(gc);

            PanelControl p2 = CreateCard("ÜYE KADROSU DAĞILIMI", Color.Orange);
            ChartControl chart = new ChartControl { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            Series s = new Series("Üye Tipi", ViewType.Pie);
            s.Points.Add(new SeriesPoint("Standart Üye", toplamUye - milliSporcu));
            s.Points.Add(new SeriesPoint("Milli Sporcu", milliSporcu));
            chart.Series.Add(s);
            chart.Legend.Visibility = DefaultBoolean.True;
            p2.Controls.Add(chart);

            layout.Controls.Add(p1, 0, 0);
            layout.Controls.Add(p2, 1, 0);
            this.Controls.Add(layout);
        }

        private PanelControl CreateCard(string title, Color accent)
        {
            PanelControl p = new PanelControl { Margin = new Padding(10), Dock = DockStyle.Fill };
            p.Appearance.BackColor = Color.FromArgb(38, 38, 42);
            p.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            LabelControl lbl = new LabelControl { Text = title, ForeColor = accent, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(15, 10) };
            p.Controls.Add(lbl);
            return p;
        }
    }


    public class UyeKayitModulu : XtraUserControl
    {
        public UyeKayitModulu()
        {
            this.Appearance.BackColor = Color.FromArgb(28, 28, 28);
            PanelControl pnl = new PanelControl { Size = new Size(500, 600), Location = new Point(50, 50), Appearance = { BackColor = Color.FromArgb(38, 38, 42) }, BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder };
           
            LabelControl lbl = new LabelControl { Text = "YENİ ÜYE KAYDI", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.Gold, Location = new Point(30, 30) };
            TextEdit tAd = CreateInput("Ad Soyad", 100, pnl);
            TextEdit tTC = CreateInput("TC No", 160, pnl);
            TextEdit tBoy = CreateInput("Boy (cm)", 220, pnl);
            TextEdit tKilo = CreateInput("Kilo (kg)", 280, pnl);
            TextEdit tYas = CreateInput("Yaş", 340, pnl);
            CheckEdit chk = new CheckEdit { Text = "Milli Sporcu mu?", Location = new Point(30, 400), Properties = { Appearance = { ForeColor = Color.White } } };
           
            SimpleButton btn = new SimpleButton { Text = "ÜYEYİ SİSTEME KAYDET", Size = new Size(440, 50), Location = new Point(30, 460), Appearance = { BackColor = Color.LimeGreen, Font = new Font("Segoe UI", 12, FontStyle.Bold) } };
            btn.Click += (s, e) => {
                try {
                    using (var conn = SqlManager.GetConnection()) {
                        conn.Open();
                        string sql = "INSERT INTO Uyeler (TC, AdSoyad, Boy, Kilo, Yas, IsMilli) VALUES (@tc, @ad, @boy, @kilo, @yas, @m)";
                        using (var cmd = new SQLiteCommand(sql, conn)) {
                            cmd.Parameters.AddWithValue("@tc", tTC.Text);
                            cmd.Parameters.AddWithValue("@ad", tAd.Text);
                            cmd.Parameters.AddWithValue("@boy", tBoy.Text);
                            cmd.Parameters.AddWithValue("@kilo", tKilo.Text);
                            cmd.Parameters.AddWithValue("@yas", tYas.Text);
                            cmd.Parameters.AddWithValue("@m", chk.Checked ? 1 : 0);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    XtraMessageBox.Show("Üye başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tAd.Text = tTC.Text = tBoy.Text = tKilo.Text = tYas.Text = "";
                } catch (Exception ex) { XtraMessageBox.Show("Hata: " + ex.Message); }
            };
            pnl.Controls.AddRange(new Control[] { lbl, tAd, tTC, tBoy, tKilo, tYas, chk, btn });
            this.Controls.Add(pnl);
        }
        private TextEdit CreateInput(string p, int y, Control parent) {
            TextEdit t = new TextEdit { Size = new Size(440, 40), Location = new Point(30, y), Font = new Font("Segoe UI", 12), Properties = { NullValuePrompt = p } };
            return t;
        }
    }
    public class MilliSporcuModulu : XtraUserControl
    {
        public MilliSporcuModulu()
        {
            this.Appearance.BackColor = Color.FromArgb(28, 28, 28);
            LabelControl lbl = new LabelControl { Text = "MİLLİ SPORCU GİRİŞİ", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.Gold, Location = new Point(50, 50) };
            TextEdit txt = new TextEdit { Size = new Size(350, 45), Location = new Point(50, 120), Font = new Font("Segoe UI", 14), Properties = { NullValuePrompt = "Lisans No (SAID2025)" } };
            SimpleButton btn = new SimpleButton { Text = "ŞAMPİYON GİRİŞİ", Location = new Point(420, 120), Size = new Size(180, 45), Appearance = { BackColor = Color.Gold, ForeColor = Color.Black, Font = new Font("Segoe UI", 10, FontStyle.Bold) } };
           
            btn.Click += (s, e) => {
                if (txt.Text.Trim().ToUpper() == "0008563557") {
                    int yas = 17;
                    string turnuva = yas < 18 ? "17.03.2026 Türkiye Milli Takım Seçmeleri - Mevlana Kültür ve Spor Merkezi" : "12.06.2026 Trabzon Alkayış Fight Night";
                    XtraMessageBox.Show($"HOŞ GELDİN ŞAMPİYON!\n\nGELECEK TURNUVA:\n{turnuva}", "Milli Takım Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else { XtraMessageBox.Show("Geçersiz Lisans!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            };
            this.Controls.AddRange(new Control[] { lbl, txt, btn });
        }
    }

    public class UyeSorguModulu : XtraUserControl
    {
        public UyeSorguModulu()
        {
            this.Appearance.BackColor = Color.FromArgb(28, 28, 28);
            TextEdit txt = new TextEdit { Size = new Size(400, 40), Location = new Point(50, 50), Properties = { NullValuePrompt = "TC Giriniz..." } };
            SimpleButton btn = new SimpleButton { Text = "SORGULA", Location = new Point(470, 50), Size = new Size(120, 40) };
            PanelControl pnl = new PanelControl { Location = new Point(50, 120), Size = new Size(700, 450), Visible = false, Appearance = { BackColor = Color.FromArgb(35, 35, 38) }, BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder };
           
            btn.Click += (s, e) => {
                pnl.Controls.Clear();
                pnl.Visible = true;
                double bmi = 26.5; 
                Color accent = bmi > 25 ? Color.Tomato : Color.LimeGreen;
                string baslik = bmi > 25 ? "YAĞ YAKIMI PROGRAMI" : "HACİM KAZANMA PROGRAMI";
                string hareketler = bmi > 25 ? "1. Patlayıcı Bench (4x15)\n2. Flying Knee (4x60sn)\n3. Lower Body Striking (4x60sn)\n4. Upper Body Striking (4x60sn)" : "1. Squat (4x10)\n2. Bench Press (4x8)\n3. Deadlift (3x8)\n4. Barbell Curl (3x12)";
                string talimat = bmi > 25 ? "KESİN TALİMAT: Antrenman sonuna 20 dk Kardiyo ekle!" : "KESİN TALİMAT: Antrenman sonu bireysel en az 20 dk. torba çalışması.";

                LabelControl lblB = new LabelControl { Text = baslik, Font = new Font("Segoe UI Black", 18), ForeColor = accent, Location = new Point(20, 20) };
                LabelControl lblH = new LabelControl { Text = hareketler, Font = new Font("Segoe UI", 12), ForeColor = Color.White, Location = new Point(20, 70), AutoSizeMode = LabelAutoSizeMode.Vertical, Size = new Size(500, 150) };
               
                PanelControl pNot = new PanelControl { Size = new Size(600, 70), Location = new Point(20, 250), Appearance = { BackColor = Color.Maroon } };
                LabelControl lblN = new LabelControl { Text = talimat, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.Gold, Location = new Point(15, 25) };
                pNot.Controls.Add(lblN);
               
                pnl.Controls.AddRange(new Control[] { lblB, lblH, pNot });
            };
            this.Controls.AddRange(new Control[] { txt, btn, pnl });
        }
    }

    public class FinalModules : XtraUserControl
    {
       public void ShowPayments()
        {
            this.Controls.Clear();
            GridControl grid = new GridControl { Dock = DockStyle.Fill };
            GridView view = new GridView(grid);
            grid.MainView = view;

            view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            view.OptionsBehavior.EditingMode = GridEditingMode.Inplace;
            view.OptionsView.ShowGroupPanel = false;

          
            Action VerileriYukle = () => {
                using (var conn = SqlManager.GetConnection())
                {
                    conn.Open();
                    SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM Odemeler", conn);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);
                    grid.DataSource = dt;
                }
            };
            VerileriYukle();

           
            view.CellValueChanged += (s, e) => {
                view.PostEditor();
                view.UpdateCurrentRow();
            };


            view.RowUpdated += (s, e) => {
                var row = view.GetFocusedDataRow();
                if (row == null) return;

                using (var conn = SqlManager.GetConnection())
                {
                    conn.Open();
                    string sql = (row["ID"] == DBNull.Value)
                        ? "INSERT INTO Odemeler (UyeTC, Tutar, Durum) VALUES (@tc, @tutar, @durum)"
                        : "UPDATE Odemeler SET UyeTC=@tc, Tutar=@tutar, Durum=@durum WHERE ID=@id";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tc", row["UyeTC"]?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@tutar", row["Tutar"] != DBNull.Value ? row["Tutar"] : 0);
                        cmd.Parameters.AddWithValue("@durum", row["Durum"]?.ToString() ?? "");
                        if (row["ID"] != DBNull.Value) cmd.Parameters.AddWithValue("@id", row["ID"]);

                        cmd.ExecuteNonQuery();
                    }
                }
               
                this.BeginInvoke(new MethodInvoker(() => VerileriYukle()));
            };

            SimpleButton btnPdf = new SimpleButton { Text = "PDF RAPOR AL", Dock = DockStyle.Bottom, Height = 40, Appearance = { BackColor = Color.DarkRed, Font = new Font("Segoe UI", 10, FontStyle.Bold) } };
            btnPdf.Click += (s, e) => RaporSihirbazi.TabloyuPdfYap(grid, "OdemeRaporu");

            this.Controls.Add(grid);
            this.Controls.Add(btnPdf);
        }

    }
}

    public class TumSporcularModulu : XtraUserControl
    {
        public TumSporcularModulu()
        {
            this.Appearance.BackColor = Color.FromArgb(28, 28, 28);
            this.Dock = DockStyle.Fill;

            GridControl grid = new GridControl { Dock = DockStyle.Fill };
            GridView view = new GridView(grid);
            grid.MainView = view;
            view.OptionsView.ShowAutoFilterRow = true;
            view.OptionsView.ShowGroupPanel = false;

            using (var conn = SqlManager.GetConnection())
            {
                conn.Open();
                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT TC, AdSoyad, Boy, Kilo, Yas, CASE WHEN IsMilli=1 THEN 'Milli' ELSE 'Standart' END as Durum FROM Uyeler", conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
            }

        
            SimpleButton btnPdf = new SimpleButton { Text = "TÜM LİSTEYİ PDF YAP", Dock = DockStyle.Bottom, Height = 40, Appearance = { BackColor = Color.DarkSlateBlue, Font = new Font("Segoe UI", 10, FontStyle.Bold) } };
            btnPdf.Click += (s, e) => RaporSihirbazi.TabloyuPdfYap(grid, "SporcuListesi");

            this.Controls.Add(grid);
            this.Controls.Add(btnPdf);
        }
    }