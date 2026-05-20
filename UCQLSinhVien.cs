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

            HienThiDuLieu();
        }

        private void HienThiDuLieu()
        {
            dgvSinhVien.DataSource = dt;
            lblTrang.Text = $"Trang 1/1 | {dt.Rows.Count} bản ghi";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "" || txtHoTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            dt.Rows.Add(
                txtMaSV.Text,
                txtHoTen.Text,
                cboGioiTinh.Text,
                dtpNgaySinh.Value.ToString("dd/MM/yyyy"),
                cboLop.Text
            );

            HienThiDuLieu();
            btnLamMoi_Click(null, null);
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
    }
}