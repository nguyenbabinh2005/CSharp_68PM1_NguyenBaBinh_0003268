using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CSharp_68PM1_NguyenBaBinh_0003268
{
    public static class DatabaseHelper
    {
        private static string connectionString =
            ConfigurationManager.ConnectionStrings["qlsvConnection"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Lấy toàn bộ danh sách sinh viên từ DB (JOIN với tbl_lophoc để lấy tên lớp)
        /// </summary>
        public static DataTable GetAllSinhVien()
        {
            DataTable dt = new DataTable();
            string sql = @"
                SELECT sv.id, sv.hoten, sv.gioitinh, sv.ngaysinh, sv.malop
                FROM tbl_sinhviens sv
                ORDER BY sv.id";

            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu sinh viên:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Lấy danh sách lớp học để đổ vào ComboBox
        /// </summary>
        public static DataTable GetAllLopHoc()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT malop, tenlop FROM tbl_lophoc ORDER BY malop";
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách lớp:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Thêm sinh viên mới vào DB
        /// </summary>
        public static bool ThemSinhVien(int id, string hoTen, string gioiTinh,
            DateTime ngaySinh, string maLop)
        {
            string sql = @"
                INSERT INTO tbl_sinhviens (id, hoten, gioitinh, ngaysinh, malop)
                VALUES (@id, @hoten, @gioitinh, @ngaysinh, @malop)";
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@hoten", hoTen);
                    cmd.Parameters.AddWithValue("@gioitinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@ngaysinh", ngaySinh.Date);
                    cmd.Parameters.AddWithValue("@malop", maLop);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm sinh viên:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra ID sinh viên đã tồn tại chưa
        /// </summary>
        public static bool KiemTraIdTonTai(int id)
        {
            string sql = "SELECT COUNT(*) FROM tbl_sinhviens WHERE id = @id";
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool SuaSinhVien(int id, string hoTen, string gioiTinh,
    DateTime ngaySinh, string maLop)
        {
            string sql = @"UPDATE tbl_sinhviens
                   SET hoten=@hoten, gioitinh=@gioitinh, ngaysinh=@ngaysinh, malop=@malop
                   WHERE id=@id";
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@hoten", hoTen);
                    cmd.Parameters.AddWithValue("@gioitinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@ngaysinh", ngaySinh.Date);
                    cmd.Parameters.AddWithValue("@malop", maLop);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa sinh viên:\n" + ex.Message);
                return false;
            }
        }
        public static bool XoaSinhVien(int id)
        {
            string sql = "DELETE FROM tbl_sinhviens WHERE id=@id";
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa sinh viên:\n" + ex.Message);
                return false;
            }
        }
        public static DataTable TimKiemSinhVien(
    string keyword,
    int page,
    int pageSize)
        {
            string sql = @"
        SELECT id,
               hoten,
               gioitinh,
               ngaysinh,
               malop
        FROM tbl_sinhviens
        WHERE CAST(id AS VARCHAR(20)) LIKE @kw
           OR hoten LIKE @kw
           OR malop LIKE @kw
        ORDER BY id
        OFFSET @offset ROWS
        FETCH NEXT @pageSize ROWS ONLY";

            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@kw",
                    "%" + keyword + "%");

                cmd.Parameters.AddWithValue("@offset",
                    (page - 1) * pageSize);

                cmd.Parameters.AddWithValue("@pageSize",
                    pageSize);

                using (SqlDataAdapter da =
                       new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

    }
}
