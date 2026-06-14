using System;
using System.Data;
using System.Windows.Forms;

namespace CSharp_68PM1_NguyenBaBinh_0003268
{
    public partial class UCQLLH : UserControl
    {

        private int currentRow = -1;

        public UCQLLH()
        {
            InitializeComponent();
            dgvLopHoc.AutoGenerateColumns = false;
     

            TaiDuLieu();
        }

  
        private void TaiDuLieu()
        {
            dgvLopHoc.DataSource =
                DatabaseHelper.LayDanhSachLopHoc();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text) ||
        string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            bool result = DatabaseHelper.ThemLopHoc(
                txtMaLop.Text.Trim(),
                txtTenLop.Text.Trim(),
                txtGhiChu.Text.Trim());

            if (result)
            {
                MessageBox.Show(
                    "Thêm lớp học thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                TaiDuLieu(); // load lại DataGridView

                btnLamMoi_Click(null, null);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp cần sửa!");
                return;
            }

            bool result = DatabaseHelper.SuaLopHoc(
                txtMaLop.Text.Trim(),
                txtTenLop.Text.Trim(),
                txtGhiChu.Text.Trim());

            if (result)
            {
                MessageBox.Show("Sửa lớp thành công!");

                TaiDuLieu();

                btnLamMoi_Click(null, null);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa!");
                return;
            }

            DialogResult rs = MessageBox.Show(
                "Xác nhận xóa lớp học?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rs == DialogResult.Yes)
            {
                bool result = DatabaseHelper.XoaLopHoc(
                    txtMaLop.Text.Trim());

                if (result)
                {
                    MessageBox.Show("Xóa lớp thành công!");

                    TaiDuLieu();

                    btnLamMoi_Click(null, null);
                }
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
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp!");
                return;
            }

            Form parentForm = this.FindForm();

            if (parentForm is MainForm main)
            {
                UCQLSinhVien uc =
                    new UCQLSinhVien(txtMaLop.Text);

                uc.Dock = DockStyle.Fill;

                main.pnl_main.Controls.Clear();
                main.pnl_main.Controls.Add(uc);
            }

        }
    }
}