using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_68PM1_NguyenBaBinh_0003268
{
    public partial class FormQLSV : Form
    {
        private DataTable dt = new DataTable();
        private int currentRow = -1;
        public FormQLSV()
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
            lblTrang.Text = $"Trang 1/1  |  {dt.Rows.Count} bản ghi";
        }

        private void cboGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void FormQLSV_Load(object sender, EventArgs e)
        {

        }

        private void btnDauTrang_Click(object sender, EventArgs e)
        {

        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {

        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "" || txtHoTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dt.Rows.Add(txtMaSV.Text, txtHoTen.Text,
                cboGioiTinh.Text, dtpNgaySinh.Value.ToString("dd/MM/yyyy"),
                cboLop.Text.Split('–')[0].Trim());
            HienThiDuLieu();
            btnLamMoi_Click(null, null);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (currentRow < 0) return;
            dt.Rows[currentRow]["HoTen"] = txtHoTen.Text;
            dt.Rows[currentRow]["GioiTinh"] = cboGioiTinh.Text;
            dt.Rows[currentRow]["NgaySinh"] = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
            dt.Rows[currentRow]["Lop"] = cboLop.Text.Split('–')[0].Trim();
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
            txtMaSV.Clear();
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = 0;
            cboLop.SelectedIndex = 0;
            currentRow = -1;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            DataTable dtFilter = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                if (row["HoTen"].ToString().ToLower() == keyword ||
                    row["MaSV"].ToString().ToLower() == keyword ||
                    row["Lop"].ToString().ToLower() == keyword)
                {
                    dtFilter.ImportRow(row);
                }
            }

            dgvSinhVien.DataSource = dtFilter;
            lblTrang.Text = $"Trang 1/1  |  {dtFilter.Rows.Count} bản ghi";
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            currentRow = e.RowIndex;
            DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

            txtMaSV.Text = row.Cells[0].Value?.ToString() ?? "";
            txtHoTen.Text = row.Cells[1].Value?.ToString() ?? "";
            cboGioiTinh.Text = row.Cells[2].Value?.ToString() ?? "";

  
            string ngaySinh = row.Cells[3].Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(ngaySinh))
            {
                if (DateTime.TryParseExact(ngaySinh, "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime dt))
                {
                    dtpNgaySinh.Value = dt;
                }
            }

            cboLop.Text = row.Cells[4].Value?.ToString() ?? "";
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
