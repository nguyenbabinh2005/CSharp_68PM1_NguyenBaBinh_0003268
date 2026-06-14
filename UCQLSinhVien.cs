using System;
using System.Data;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace CSharp_68PM1_NguyenBaBinh_0003268
{
    public partial class UCQLSinhVien : UserControl
    {
        private int currentPage = 1;
        private int pageSize = 10;
        private string maLop;
        public UCQLSinhVien()
        {
            InitializeComponent();

            dgvSinhVien.AutoGenerateColumns = false;

            LoadDanhSachLop();

            LoadDuLieuTuDB(); // hiện tất cả sinh viên
        }

        public UCQLSinhVien(string maLop)
        {
            InitializeComponent();
            this.maLop = maLop;

            dgvSinhVien.AutoGenerateColumns = false;

            LoadDanhSachLop();

            LoadDuLieuTuDB();
        }
        private void LoadDanhSachLop()
        {
            try
            {
                DataTable dtLop = DatabaseHelper.GetAllLopHoc();
                cboLop.Items.Clear();
                foreach (DataRow row in dtLop.Rows)
                {
                    cboLop.Items.Add(row["malop"].ToString());
                }
                if (cboLop.Items.Count > 0)
                    cboLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load lớp học:\n" + ex.Message);
            }
        }

        /// <summary>
        /// Load toàn bộ sinh viên từ SQL Server vào DataTable local rồi hiển thị
        /// </summary>
        private void LoadDuLieuTuDB()
        {

            if (string.IsNullOrEmpty(maLop))
            {
                dgvSinhVien.DataSource =
                    DatabaseHelper.GetAllSinhVien();
            }
            else
            {
                dgvSinhVien.DataSource =
                    DatabaseHelper.LaySinhVienTheoLop(maLop);
            }

            lblTrang.Text =
                $"Trang 1/1 | {dgvSinhVien.Rows.Count} bản ghi";
        }

  

        private void btnSua_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtMaSV.Text);

            bool ok = DatabaseHelper.SuaSinhVien(
                id,
                txtHoTen.Text,
                cboGioiTinh.Text,
                dtpNgaySinh.Value,
                cboLop.Text);

            if (ok)
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadDuLieuTuDB();
                btnLamMoi_Click(null, null);
            }
        }

    

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            cboGioiTinh.SelectedIndex = -1;
            cboLop.SelectedIndex = -1;
        }

      

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (txtMaSV.Text.Trim() == "" || txtHoTen.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtMaSV.Text.Trim(), out int maSV))
            {
                MessageBox.Show("Mã SV phải là số nguyên!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DatabaseHelper.KiemTraIdTonTai(maSV))
            {
                MessageBox.Show($"Mã SV '{maSV}' đã tồn tại trong CSDL!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLop = cboLop.Text;
            bool success = DatabaseHelper.ThemSinhVien(
                maSV,
                txtHoTen.Text.Trim(),
                cboGioiTinh.Text,
                dtpNgaySinh.Value,
                maLop
            );

            if (success)
            {
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDuLieuTuDB();     
                btnLamMoi_Click(null, null);
            }

        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

       
            DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

            txtMaSV.Text = row.Cells[0].Value?.ToString();
            txtHoTen.Text = row.Cells[1].Value?.ToString();
            cboGioiTinh.Text = row.Cells[2].Value?.ToString();
            cboLop.Text = row.Cells[4].Value?.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "")
                return;

            int id = int.Parse(txtMaSV.Text);

        
            if (MessageBox.Show("Xác nhận xóa?", "Xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ok = DatabaseHelper.XoaSinhVien(id);
                if (ok)
                {
                    LoadDuLieuTuDB();
                    btnLamMoi_Click(null, null);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            currentPage = 1;

            DataTable dt;

            if (!string.IsNullOrEmpty(maLop))
            {
                dt = DatabaseHelper.TimKiemSinhVienTheoLop(
                    maLop,
                    keyword,
                    currentPage,
                    pageSize);
            }
            else
            {
                dt = DatabaseHelper.TimKiemSinhVien(
                    keyword,
                    currentPage,
                    pageSize);
            }

            dgvSinhVien.DataSource = dt;

            lblTrang.Text =
                $"Trang {currentPage} | {dt.Rows.Count} bản ghi";
        }
    }
}
