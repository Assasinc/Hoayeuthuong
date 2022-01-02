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
    public partial class frmKho : Form
    {
        DataTable tblKho; //Bảng khách hàng
        public frmKho()
        {
            InitializeComponent();
        }

        private void frmKho_Load(object sender, EventArgs e)
        {
            txtMaCuaHang.Enabled = false;
            cboHoa.Enabled = false;
            btnThem.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            Functions.FillCombo("SELECT mahoa FROM Hoa", cboHoa, "mahoa", "tenhoa");
            cboHoa.SelectedIndex = -1;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT tk.idcuahang,tk.mahoa,h.tenhoa,tk.soluongtonkho from tonkho tk left join hoa h on(tk.mahoa = h.mahoa)";
            tblKho = Functions.GetDataToTable(sql); //Lấy dữ liệu từ bảng
            dgvKho.DataSource = tblKho; //Hiển thị vào dataGridView
            //dgvKho.Columns[0].HeaderText = "Mã khách";
            //dgvKho.Columns[1].HeaderText = "Tên khách";
            //dgvKho.Columns[2].HeaderText = "Địa chỉ";
            //dgvKho.Columns[0].Width = 100;
            //dgvKho.Columns[1].Width = 150;
            //dgvKho.Columns[2].Width = 150;
            dgvKho.AllowUserToAddRows = false;
            dgvKho.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvKho_Click(object sender, EventArgs e)
        {
            //if (btnThem.Enabled == false)
            //{
            //    MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtMaKhach1.Focus();
            //    return;
            //}
            if (tblKho.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaCuaHang.Text = dgvKho.CurrentRow.Cells["idcuahang"].Value.ToString();
            cboHoa.SelectedIndex = cboHoa.FindStringExact(dgvKho.CurrentRow.Cells["mahoa"].Value.ToString());
            txtSoLuongTon.Text = dgvKho.CurrentRow.Cells["soluongtonkho"].Value.ToString();
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
            txtMaCuaHang.Enabled = true;
            cboHoa.Enabled = true;
            txtMaCuaHang.Focus();
        }
        private void ResetValues()
        {
            txtMaCuaHang.Text = "";
            cboHoa.SelectedIndex = -1;
            txtSoLuongTon.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaCuaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã cửa hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCuaHang.Focus();
                return;
            }
            if (txtSoLuongTon.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng tồn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongTon.Focus();
                return;
            }
            //sql = "SELECT idnhanvien FROM NhanVien WHERE idnhanvien=N'" + txtMaNhanVien.Text.Trim() + "'";
            //if (Functions.CheckKey(sql))
            //{
            //    MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtMaNhanVien.Focus();
            //    txtMaNhanVien.Text = "";
            //    return;
            //}
            sql = "INSERT INTO tonkho(idcuahang,mahoa,soluongtonkho) VALUES " +
                "(N'" + txtMaCuaHang.Text.Trim()
                + "',N'" + cboHoa.SelectedValue
                + "','" + txtSoLuongTon.Text.Trim()
                + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaCuaHang.Enabled = false;
            cboHoa.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKho.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaCuaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (txtTenNhanVien.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtTenNhanVien.Focus();
            //    return;
            //}
            //if (txtDiaChi.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtDiaChi.Focus();
            //    return;
            //}
            sql = "UPDATE tonkho SET soluongtonkho=N'" + txtSoLuongTon.Text.Trim().ToString() +
                    "' WHERE idcuahang=N'" + txtMaCuaHang.Text + "' and mahoa='" + cboHoa.SelectedValue + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKho.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaCuaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tonkho WHERE idcuahang=N'" + txtMaCuaHang.Text + 
                    "' and mahoa='" + cboHoa.SelectedValue + "'";
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
            txtMaCuaHang.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
