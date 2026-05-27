using System;
using System.Data;
using System.Windows.Forms;

namespace CSharp_68PM1_NguyenBaBinh_0003268
{
    public partial class UCQLSinhVien : UserControl
    {
        private DataTable dt = new DataTable();
        private int currentRow = -1;

        public UCQLSinhVien()
        {
            InitializeComponent();

            dgvSinhVien.AutoGenerateColumns = false;

            dt.Columns.Add("MaSV");
            dt.Columns.Add("HoTen");
            dt.Columns.Add("GioiTinh");
            dt.Columns.Add("NgaySinh");
            dt.Columns.Add("Lop");
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
            dt.Clear();
            DataTable dtDB = DatabaseHelper.GetAllSinhVien();
            foreach (DataRow row in dtDB.Rows)
            {
                dt.Rows.Add(
                    row["id"].ToString(),
                    row["hoten"].ToString(),
                    row["gioitinh"].ToString(),
                    Convert.ToDateTime(row["ngaysinh"]).ToString("dd/MM/yyyy"),
                    row["malop"].ToString()
                );
            }
            HienThiDuLieu();
        }

        private void HienThiDuLieu()
        {
            dgvSinhVien.DataSource = dt;
            lblTrang.Text = $"Trang 1/1 | {dt.Rows.Count} bản ghi";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (currentRow < 0) return;

            dt.Rows[currentRow]["HoTen"] = txtHoTen.Text;
            dt.Rows[currentRow]["GioiTinh"] = cboGioiTinh.Text;
            dt.Rows[currentRow]["NgaySinh"] = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
            dt.Rows[currentRow]["Lop"] = cboLop.Text;

            HienThiDuLieu();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentRow < 0) return;

            dt.Rows[currentRow].Delete();
            HienThiDuLieu();
            btnLamMoi_Click(null, null);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            currentRow = -1;
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            currentRow = e.RowIndex;
            DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

            txtMaSV.Text = row.Cells[0].Value?.ToString();
            txtHoTen.Text = row.Cells[1].Value?.ToString();
            cboGioiTinh.Text = row.Cells[2].Value?.ToString();
            cboLop.Text = row.Cells[4].Value?.ToString();
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
    }
}
