using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class frmTimHoa : Form
    {
        public frmTimHoa()
        {
            InitializeComponent();
        }

        private void frmTimHoa_Load(object sender, EventArgs e)
        {
            ResetValues();
            dgvTimHoa.DataSource = null;
        }

        private void btnTimHoa_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtTenHoa.Text == "") && (txtLoaiHoa.Text == "") && (txtMauSac.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtTenHoa.Focus();
        }
    }
}
