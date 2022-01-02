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
using QuanLyBanHang.Class;

namespace QuanLyBanHang
{
    public partial class frmTimHDBan : Form
    {
        DataTable tblHDB; //Hoá đơn bán
        public frmTimHDBan()
        {
            InitializeComponent();
        }

        private void frmTimHDBan_Load(object sender, EventArgs e)
        {
            ResetValues();
            dgvTKHoaDon.DataSource = null;
            Functions.FillCombo("SELECT idnhanvien,hoten FROM NhanVien", cboNhanVien, "idnhanvien", "hoten");
            cboNhanVien.SelectedIndex = -1;
            Functions.FillCombo("SELECT idkhachhang,hoten FROM KhachHang", cboKhachHang, "idkhachhang", "hoten");
            cboKhachHang.SelectedIndex = -1;
        }
        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaHDBan.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHDBan.Text == "") && (txtThang.Text == "") && (txtNam.Text == "") &&
               (cboNhanVien.SelectedIndex == -1) && (cboKhachHang.SelectedIndex == -1) &&
               (txtTongTien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM DonHang WHERE 1=1";
            if (txtMaHDBan.Text != "")
                sql = sql + " AND madh Like N'%" + txtMaHDBan.Text + "%'";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH(ngaygiao) =" + txtThang.Text;
            if (txtNam.Text != "")
                sql = sql + " AND YEAR(ngaygiao) =" + txtNam.Text;
            if (cboNhanVien.SelectedIndex > -1)
                sql = sql + " AND idnhanvien='" + cboNhanVien.SelectedValue + "'";
            if (cboKhachHang.SelectedIndex > -1)
                sql = sql + " AND idkhachhang='" + cboKhachHang.SelectedValue + "'";
            if (txtTongTien.Text != "")
                sql = sql + " AND tongtien <=" + txtTongTien.Text;
            tblHDB = Functions.GetDataToTable(sql);
            if (tblHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tblHDB.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvTKHoaDon.DataSource = tblHDB;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            dgvTKHoaDon.Columns[0].HeaderText = "Mã Đơn hàng";
            dgvTKHoaDon.Columns[1].HeaderText = "Mã khách hàng";
            dgvTKHoaDon.Columns[2].HeaderText = "Mã nhân viên";
            dgvTKHoaDon.Columns[3].HeaderText = "Ngày giao";
            dgvTKHoaDon.Columns[4].HeaderText = "Tổng tiền";
            dgvTKHoaDon.Columns[5].HeaderText = "Tình trạng";
            dgvTKHoaDon.Columns[6].HeaderText = "Địa chỉ giao";
            dgvTKHoaDon.Columns[0].Width = 70;
            dgvTKHoaDon.Columns[1].Width = 80;
            dgvTKHoaDon.Columns[2].Width = 80;
            dgvTKHoaDon.Columns[3].Width = 80;
            dgvTKHoaDon.Columns[4].Width = 80;
            dgvTKHoaDon.Columns[5].Width = 80;
            dgvTKHoaDon.Columns[6].Width = 150;
            dgvTKHoaDon.AllowUserToAddRows = false;
            dgvTKHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dgvTKHoaDon.DataSource = null;
        }

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void dgvTKHoaDon_DoubleClick(object sender, EventArgs e)
        {
            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mahd = dgvTKHoaDon.CurrentRow.Cells["madh"].Value.ToString();
                frmHoaDonBan frm = new frmHoaDonBan();
                //frm.txtMaHDBan.Text = mahd;
                frm.txtMaHDBan.Text = mahd;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
