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
    public partial class frmDMNhanvien : Form
    {
        DataTable tblNV; //Lưu dữ liệu bảng nhân viên
        public frmDMNhanvien()
        {
            InitializeComponent();
        }

        private void frmDMNhanvien_Load(object sender, EventArgs e)
        {
            txtMaNhanVien.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            Functions.FillCombo("SELECT idcuahang FROM cuahang", cboMaCuaHang, "idcuahang", "MaCuaHang");
            cboMaCuaHang.SelectedIndex = -1;
            LoadDataGridView();
        }

        public void LoadDataGridView()
        {
            string sql;
            sql = "SELECT idnhanvien,account,idcuahang,hoten,diachi,sdt,email,luong,sodonhang,doanhso FROM NhanVien";
            tblNV = Functions.GetDataToTable(sql); //lấy dữ liệu
            dgvNhanVien.DataSource = tblNV;
            //dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            //dgvNhanVien.Columns[1].HeaderText = "Tên tài khoản";
            //dgvNhanVien.Columns[2].HeaderText = "Mã cửa hàng";
            //dgvNhanVien.Columns[3].HeaderText = "Họ tên";
            //dgvNhanVien.Columns[4].HeaderText = "Địa chỉ";
            //dgvNhanVien.Columns[5].HeaderText = "Số điện thoại";
            //dgvNhanVien.Columns[6].HeaderText = "Email";
            //dgvNhanVien.Columns[7].HeaderText = "Lương";
            //dgvNhanVien.Columns[8].HeaderText = "Doanh số";
            //dgvNhanVien.Columns[0].Width = 100;
            //dgvNhanVien.Columns[1].Width = 150;
            //dgvNhanVien.Columns[2].Width = 100;
            //dgvNhanVien.Columns[3].Width = 150;
            //dgvNhanVien.Columns[4].Width = 100;
            //dgvNhanVien.Columns[5].Width = 100;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvNhanVien_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanVien.Focus();
                return;
            }
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNhanVien.Text = dgvNhanVien.CurrentRow.Cells["idnhanvien"].Value.ToString();
            txtTenNhanVien.Text = dgvNhanVien.CurrentRow.Cells["hoten"].Value.ToString();
            //if (dgvNhanVien.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam") chkGioiTinh.Checked = true;
            //else chkGioiTinh.Checked = false;
            txtAccount.Text = dgvNhanVien.CurrentRow.Cells["account"].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells["diachi"].Value.ToString();
            txtDienThoai.Text = dgvNhanVien.CurrentRow.Cells["sdt"].Value.ToString();
            cboMaCuaHang.SelectedIndex = cboMaCuaHang.FindStringExact(dgvNhanVien.CurrentRow.Cells["idcuahang"].Value.ToString());
            txtEmail.Text = dgvNhanVien.CurrentRow.Cells["email"].Value.ToString();
            txtLuong.Text = dgvNhanVien.CurrentRow.Cells["luong"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaNhanVien.Enabled = true;
            txtMaNhanVien.Focus();
        }
        private void ResetValues()
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            txtAccount.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            cboMaCuaHang.SelectedIndex = -1;
            txtEmail.Text = "";
            txtLuong.Text = "";
            //mskNgaySinh.Text = "";
            //mtbDienThoai.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }
            if (txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            //if (mtbDienThoai.Text == "(   )     -")
            //{
            //    MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    mtbDienThoai.Focus();
            //    return;
            //}
            //if (mskNgaySinh.Text == "  /  /")
            //{
            //    MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    mskNgaySinh.Focus();
            //    return;
            //}
            //if (!Functions.IsDate(mskNgaySinh.Text))
            //{
            //    MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            // mskNgaySinh.Text = "";
            //    mskNgaySinh.Focus();
            //    return;
            //}
            //if (chkGioiTinh.Checked == true)
            //    gt = "Nam";
            //else
            //    gt = "Nữ";
            sql = "SELECT idnhanvien FROM NhanVien WHERE idnhanvien=N'" + txtMaNhanVien.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                txtMaNhanVien.Text = "";
                return;
            }
            sql = "INSERT INTO Acc(account,pw) values " +
                "('" + txtAccount.Text.Trim() + "','123456')";
            Functions.RunSQL(sql);
            sql = "INSERT INTO NhanVien(idnhanvien,account,idcuahang,hoten,diachi,sdt,email,luong,sodonhang,doanhso) VALUES " +
                "(N'" + txtMaNhanVien.Text.Trim()
                + "',N'" + txtAccount.Text.Trim()
                + "','" + cboMaCuaHang.SelectedValue
                + "',N'" + txtTenNhanVien.Text.Trim() 
                + "',N'" + txtDiaChi.Text.Trim() 
                + "','" + txtDienThoai.Text
                + "',N'" + txtEmail.Text.Trim()
                + "',N'" + txtLuong.Text.Trim()
                + "', 0, 0"
                + ")";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaNhanVien.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            //if (mtbDienThoai.Text == "(   )     -")
            //{
            //    MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    mtbDienThoai.Focus();
            //    return;
            //}
            //if (mskNgaySinh.Text == "  /  /")
            //{
            //    MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    mskNgaySinh.Focus();
            //    return;
            //}
            //if (!Functions.IsDate(mskNgaySinh.Text))
            //{
            //    MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    mskNgaySinh.Text = "";
            //    mskNgaySinh.Focus();
            //    return;
            //}
            //if (chkGioiTinh.Checked == true)
            //    gt = "Nam";
            //else
            //    gt = "Nữ";
            sql = "UPDATE NhanVien SET hoten=N'" + txtTenNhanVien.Text.Trim().ToString() +
                    "',diachi=N'" + txtDiaChi.Text.Trim().ToString() +
                    "',sdt='" + txtDienThoai.Text.ToString() +
                    "',email=N'" + txtEmail.Text.Trim().ToString() +
                    "',luong=N'" + txtLuong.Text.Trim().ToString() +

                    /*gt +*/
                    //"',NgaySinh='" + Functions.ConvertDateTime(mskNgaySinh.Text) +
                    "' WHERE idnhanvien=N'" + txtMaNhanVien.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE lichsuluong WHERE idnhanvien=N'" + txtMaNhanVien.Text + "'";
                Functions.RunSqlDel(sql);
                sql = "DELETE NhanVien WHERE idnhanvien=N'" + txtMaNhanVien.Text + "'";
                Functions.RunSqlDel(sql);
                sql = "DELETE Acc WHERE account=N'" + txtAccount.Text + "'";
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
            txtMaNhanVien.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
