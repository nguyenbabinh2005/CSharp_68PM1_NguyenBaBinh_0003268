using System;
using System.Data;
using System.Windows.Forms;

namespace CSharp_68PM1_NguyenBaBinh_0003268
{
    public partial class UCQLLH : UserControl
    {
        private DataTable dt = new DataTable();
        private int currentRow = -1;

        public UCQLLH()
        {
            InitializeComponent();
            dgvLopHoc.AutoGenerateColumns = false;
            dt.Columns.Add("MaID");
            dt.Columns.Add("MaLop");
            dt.Columns.Add("TenLop");
            dt.Columns.Add("GhiChu");
            HienThiDuLieu();
        }

        private void HienThiDuLieu()
        {
            dgvLopHoc.DataSource = dt;
            lblTrang.Text = $"Trang 1/1  |  {dt.Rows.Count} bản ghi";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaLop.Text == "" || txtTenLop.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dt.Rows.Add((dt.Rows.Count + 1).ToString(), txtMaLop.Text, txtTenLop.Text, txtGhiChu.Text);
            HienThiDuLieu();
            btnLamMoi_Click(null, null);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (currentRow < 0) return;
            dt.Rows[currentRow]["MaLop"] = txtMaLop.Text;
            dt.Rows[currentRow]["TenLop"] = txtTenLop.Text;
            dt.Rows[currentRow]["GhiChu"] = txtGhiChu.Text;
            HienThiDuLieu();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentRow < 0) return;
            if (MessageBox.Show("Xác nhận xóa?", "Xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dt.Rows[currentRow].Delete();
                HienThiDuLieu();
                btnLamMoi_Click(null, null);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaID.Text = "";
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtGhiChu.Text = "";
            currentRow = -1;
            txtMaLop.ReadOnly = false;
            txtMaLop.BackColor = System.Drawing.Color.White;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            DataTable dtFilter = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                if (row["MaID"].ToString().ToLower().Contains(keyword) ||
                    row["MaLop"].ToString().ToLower().Contains(keyword) ||
                    row["TenLop"].ToString().ToLower().Contains(keyword))
                {
                    dtFilter.ImportRow(row);
                }
            }
            dgvLopHoc.DataSource = dtFilter;
            lblTrang.Text = $"Trang 1/1  |  {dtFilter.Rows.Count} bản ghi";
        }

        private void dgvLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            currentRow = e.RowIndex;
            DataGridViewRow row = dgvLopHoc.Rows[e.RowIndex];
            txtMaID.Text = row.Cells[0].Value?.ToString() ?? "";
            txtMaLop.Text = row.Cells[1].Value?.ToString() ?? "";
            txtTenLop.Text = row.Cells[2].Value?.ToString() ?? "";
            txtGhiChu.Text = row.Cells[3].Value?.ToString() ?? "";
            txtMaLop.ReadOnly = true;
            txtMaLop.BackColor = System.Drawing.Color.LightGray;
        }

        private void btnXemDSSV_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();

            if (parentForm is MainForm main)
            {
                UCQLSinhVien uc = new UCQLSinhVien();
                uc.Dock = DockStyle.Fill;

                main.Controls["pnl_main"].Controls.Clear();
                main.Controls["pnl_main"].Controls.Add(uc);
            }
        }

        
    }
}