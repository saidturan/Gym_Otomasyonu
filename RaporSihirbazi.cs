using System;
using System.Windows.Forms;
using DevExpress.XtraGrid;

namespace GymOtomasyonu
{
    public static class RaporSihirbazi
    {
        public static void TabloyuPdfYap(GridControl grid, string dosyaAdi)
        {
            try
            {
                string yol = Application.StartupPath + "\\" + dosyaAdi + ".pdf";
                grid.ExportToPdf(yol);
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(yol) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rapor oluşturulurken hata: " + ex.Message);
            }
        }
    }
}
