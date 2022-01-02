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
    public partial class frmDMHang : Form
    {
        DataTable tblH; //Bảng hàng
        public frmDMHang()
        {
            InitializeComponent();
        }

        private void frmDMHang_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * from LoaiHoa";
            txtMaHang.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            Functions.FillCombo(sql, cboMaLoaiHoa, "maloaihoa", "tenloai");
            cboMaLoaiHoa.SelectedIndex = -1;
            ResetValues();
        }

        private void ResetValues()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cboMaLoaiHoa.Text = "";
            txtSoLuong.Text = "0";
            txtMauSac.Text = "";
            txtDonGiaNhap.Text = "0";
            txtGiamGia.Text = "0";
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtGiamGia.Enabled = true;
            txtAnh.Text = "";
            picAnh.Image = null;
            txtGhichu.Text = "";
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT h.mahoa,h.tenhoa,h.maloaihoa,lh.tenloai,h.mausac,h.dongia,h.giamgia from Hoa h left join LoaiHoa lh on(h.maloaihoa=lh.maloaihoa)";
            tblH = Functions.GetDataToTable(sql);
            dgvHang.DataSource = tblH;
            //dgvHang.Columns[0].HeaderText = "Mã hàng";
            //dgvHang.Columns[1].HeaderText = "Tên hàng";
            //dgvHang.Columns[2].HeaderText = "Chất liệu";
            //dgvHang.Columns[3].HeaderText = "Số lượng";
            //dgvHang.Columns[4].HeaderText = "Đơn giá nhập";
            //dgvHang.Columns[5].HeaderText = "Đơn giá bán";
            //dgvHang.Columns[6].HeaderText = "Ảnh";
            //dgvHang.Columns[7].HeaderText = "Ghi chú";
            //dgvHang.Columns[0].Width = 80;
            //dgvHang.Columns[1].Width = 140;
            //dgvHang.Columns[2].Width = 80;
            //dgvHang.Columns[3].Width = 80;
            //dgvHang.Columns[4].Width = 100;
            //dgvHang.Columns[5].Width = 100;
            //dgvHang.Columns[6].Width = 200;
            //dgvHang.Columns[7].Width = 300;
            dgvHang.AllowUserToAddRows = false;
            dgvHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvHang_Click(object sender, EventArgs e)
        {
            string MaChatLieu;
            string sql;
            //if (btnThem.Enabled == false)
            //{
            //    MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtMaHang.Focus();
            //    return;
            //}
            if (tblH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaHang.Text = dgvHang.CurrentRow.Cells["mahoa"].Value.ToString();
            txtTenHang.Text = dgvHang.CurrentRow.Cells["tenhoa"].Value.ToString();
            MaChatLieu = dgvHang.CurrentRow.Cells["maloaihoa"].Value.ToString();
            sql = "SELECT tenloai FROM LoaiHoa WHERE maloaihoa=N'" + MaChatLieu + "'";
            cboMaLoaiHoa.Text = Functions.GetFieldValues(sql);
            txtMauSac.Text = dgvHang.CurrentRow.Cells["mausac"].Value.ToString();
            //txtSoLuong.Text = dgvHang.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtDonGiaNhap.Text = dgvHang.CurrentRow.Cells["dongia"].Value.ToString();
            txtGiamGia.Text = dgvHang.CurrentRow.Cells["giamgia"].Value.ToString();
            //sql = "SELECT Anh FROM tblHang WHERE MaHang=N'" + txtMaHang.Text + "'";
            //txtAnh.Text = Functions.GetFieldValues(sql);
            //picAnh.Image = Image.FromFile(txtAnh.Text);
            //sql = "SELECT Ghichu FROM tblHang WHERE MaHang = N'" + txtMaHang.Text + "'";
            //txtGhichu.Text = Functions.GetFieldValues(sql);
            btnSua.Enabled = true;
            //btnXoa.Enabled = true;
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
            txtMaHang.Enabled = true;
            txtMaHang.Focus();
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtGiamGia.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if (cboMaLoaiHoa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaLoaiHoa.Focus();
                return;
            }
            //if (txtAnh.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("Bạn phải chọn ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    btnOpen.Focus();
            //    return;
            //}
            sql = "SELECT mahoa FROM Hoa WHERE mahoa=N'" + txtMaHang.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hoa này đã tồn tại, bạn phải chọn mã hoa khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            sql = "INSERT INTO Hoa(mahoa,tenhoa,maloaihoa,mausac,dongia,giamgia) VALUES(N'"
                + txtMaHang.Text.Trim() + 
                "',N'" + txtTenHang.Text.Trim() +
                "',N'" + cboMaLoaiHoa.SelectedValue +
                "',N'" + txtMauSac.Text.Trim() + 
                "'," + txtDonGiaNhap.Text +
                "," + txtGiamGia.Text + 
                //",'" + txtAnh.Text + "',N'" + txtGhichu.Text.Trim() + 
                ")";

            Functions.RunSQL(sql);
            LoadDataGridView();
            //ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaHang.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if (cboMaLoaiHoa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn loại hoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaLoaiHoa.Focus();
                return;
            }
            //if (txtAnh.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("Bạn phải ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtAnh.Focus();
            //    return;
            //}
            sql = "UPDATE Hoa SET tenhoa=N'" + txtTenHang.Text.Trim().ToString() +
                "',maloaihoa=N'" + cboMaLoaiHoa.SelectedValue +
                "',mausac=N'" + txtMauSac.Text +
                "',dongia='" + txtDonGiaNhap.Text + "',giamgia=N'" + txtGiamGia.Text + "' WHERE mahoa=N'" + txtMaHang.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tonkho WHERE mahoa=N'" + txtMaHang.Text + "'";
                Functions.RunSqlDel(sql);
                sql = "DELETE Hoa WHERE mahoa=N'" + txtMaHang.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaHang.Enabled = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHang.Text == "") && (txtTenHang.Text == "") && (cboMaLoaiHoa.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from Hoa WHERE 1=1";
            if (txtMaHang.Text != "")
                sql += " AND mahoa LIKE N'%" + txtMaHang.Text + "%'";
            if (txtTenHang.Text != "")
                sql += " AND tenhoa LIKE N'%" + txtTenHang.Text + "%'";
            if (cboMaLoaiHoa.Text != "")
                sql += " AND maloaihoa LIKE N'%" + cboMaLoaiHoa.SelectedValue + "%'";
            tblH = Functions.GetDataToTable(sql);
            if (tblH.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblH.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvHang.DataSource = tblH;
            ResetValues();
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT mahoa ,tenhoa ,maloaihoa ,mausac ,dongia ,giamgia FROM Hoa";
            tblH = Functions.GetDataToTable(sql);
            dgvHang.DataSource = tblH;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
