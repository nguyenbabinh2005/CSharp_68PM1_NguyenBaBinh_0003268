using System;
using System.Windows.Forms;

namespace CSharp_68PM1_NguyenBaBinh_0003268
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadUCQLSV();
        }

        private void LoadUCQLSV()
        {
            pnl_main.Controls.Clear();

            UCQLSinhVien uc = new UCQLSinhVien();
            uc.Dock = DockStyle.Fill;

            pnl_main.Controls.Add(uc);
        }

      

       

        private void quảnLýLớpHọcToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            pnl_main.Controls.Clear();

            UCQLLH uc = new UCQLLH();
            uc.Dock = DockStyle.Fill;

            pnl_main.Controls.Add(uc);
        }

        private void quảnLýSinhViênToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                pnl_main.Controls.Clear();

                UCQLSinhVien uc = new UCQLSinhVien();
                uc.Dock = DockStyle.Fill;

                pnl_main.Controls.Add(uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}