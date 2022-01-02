using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyBanHang
{
    public partial class frmDangNhap : Form
    {
        public static string UserName = "";
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void cboHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (cboHienMatKhau.Checked)
            {
                txtMatKhau.PasswordChar = (char)0;
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            SqlConnection Conn = new SqlConnection();
            try
            {
                Conn.ConnectionString = "Server=.;Database=QuanLyBanHang;Integrated Security=True";
                Conn.Open();                  //Mở kết nối
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "KiemTraDangNhap";
                cmd.Parameters.AddWithValue("@UserName", txtTaiKhoan.Text);
                cmd.Parameters.AddWithValue("@Password", txtMatKhau.Text);
                cmd.Connection = Conn;
                UserName = txtTaiKhoan.Text;
                object kq = cmd.ExecuteScalar();
                int code = Convert.ToInt32(kq);
                if (code == 0)
                {
                    MessageBox.Show("Tài khoản không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatKhau.Text = "";
                    txtTaiKhoan.Text = "";
                    txtTaiKhoan.Focus();
                }
                else if (code == 1)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    frmMain frmMain = new frmMain(); //Khởi tạo đối tượng
                    frmMain.ShowDialog(); //Hiển thị
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatKhau.Text = "";
                    txtTaiKhoan.Text = "";
                    txtTaiKhoan.Focus();
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes) Application.Exit();
        }
    }
}
