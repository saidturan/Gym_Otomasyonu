using System;
using System.Windows.Forms;

namespace GymOtomasyonu
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            this.Load += (s, e) => {
                SqlManager.VeritabaniniHazirla();
                this.Hide();
                GymMainForm anaForm = new GymMainForm();
                anaForm.ShowDialog();
                this.Close();
            };
        }
    }
}