using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using QuanLyBanHang.Class;

namespace QuanLyBanHang
{
    public partial class frmDMKhachHang : Form
    {
        DataTable tblKH; //Bảng khách hàng
        public frmDMKhachHang()
        {
            InitializeComponent();
        }

        private void frmDMKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKhach1.Enabled = false;
            //btnThem.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * from KhachHang";
            tblKH = Functions.GetDataToTable(sql); //Lấy dữ liệu từ bảng
            dgvKhachHang.DataSource = tblKH; //Hiển thị vào dataGridView
            //dgvKhachHang.Columns[0].HeaderText = "Mã khách";
            //dgvKhachHang.Columns[1].HeaderText = "Tên khách";
            //dgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
            //dgvKhachHang.Columns[3].HeaderText = "Điện thoại";
            //dgvKhachHang.Columns[0].Width = 100;
            //dgvKhachHang.Columns[1].Width = 150;
            //dgvKhachHang.Columns[2].Width = 150;
            //dgvKhachHang.Columns[3].Width = 150;
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvKhachHang_Click(object sender, EventArgs e)
        {
            //if (btnThem.Enabled == false)
            //{
            //    MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtMaKhach1.Focus();
            //    return;
            //}
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaKhach1.Text = dgvKhachHang.CurrentRow.Cells["idkhachhang"].Value.ToString();
            txtHoTen.Text = dgvKhachHang.CurrentRow.Cells["hoten"].Value.ToString();
            txtAccount.Text = dgvKhachHang.CurrentRow.Cells["account"].Value.ToString();
            txtDiaChi1.Text = dgvKhachHang.CurrentRow.Cells["diachi"].Value.ToString();
            txtDienThoai.Text = dgvKhachHang.CurrentRow.Cells["sdt"].Value.ToString();
            txtEmail.Text = dgvKhachHang.CurrentRow.Cells["email"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaKhach1.Enabled = true;
            txtMaKhach1.Focus();
        }
        private void ResetValues()
        {
            txtMaKhach1.Text = "";
            txtHoTen.Text = "";
            txtAccount.Text = "";
            txtDiaChi1.Text = "";
            txtDienThoai.Text = "";
            txtEmail.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaKhach1.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhach1.Focus();
                return;
            }
            if (txtHoTen.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
                return;
            }
            if (txtDiaChi1.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi1.Focus();
                return;
            }
            if (txtDienThoai.Text == "(  )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            //Kiểm tra đã tồn tại mã khách chưa
            sql = "SELECT idkhachhang FROM KhachHang WHERE idkhachhang=N'" + txtMaKhach1.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhach1.Focus();
                return;
            }
            //Chèn thêm
            sql = "INSERT INTO KhachHang VALUES (N'" + txtMaKhach1.Text.Trim() +
                "',N'" + txtAccount.Text.Trim() +
                "',N'" + txtHoTen.Text.Trim() + 
                "',N'" + txtDiaChi1.Text.Trim() +
                "',N'" + txtDienThoai.Text.Trim() +
                "','" + txtEmail.Text + 
                "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();

            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaKhach1.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhach1.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtHoTen.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
                return;
            }
            if (txtDiaChi1.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi1.Focus();
                return;
            }
            if (txtDienThoai.Text == "(  )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            sql = "UPDATE KhachHang SET hoten=N'" + txtHoTen.Text.Trim().ToString() + "',diachi=N'" +
                txtDiaChi1.Text.Trim().ToString() + "',sdt='" + txtDienThoai.Text.ToString() +
                "',email='" + txtEmail.Text.ToString() +
                "' WHERE idkhachhang=N'" + txtMaKhach1.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhach1.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE KhachHang WHERE idkhachhang=N'" + txtMaKhach1.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaKhach1.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
