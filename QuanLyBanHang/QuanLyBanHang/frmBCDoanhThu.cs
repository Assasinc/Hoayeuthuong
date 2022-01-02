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
using Microsoft.Reporting.WinForms;

namespace QuanLyBanHang
{
    public partial class frmBCDoanhThu : Form
    {
        public frmBCDoanhThu()
        {
            InitializeComponent();
        }

        private void frmBCDoanhThu_Load(object sender, EventArgs e)
        {

            this.rpvBaoCao.RefreshReport();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            //Khai báo câu lệnh SQL
            if(dtpTuNgay.Value > dtpDenNgay.Value)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String sql = "Select * from DonHang Where ngaygiao >='" + dtpTuNgay.Value.ToString() + "' and ngaygiao < '" + dtpDenNgay.Value.ToString() + "'";
            SqlConnection con = new SqlConnection();
            //Truyền vào chuỗi kết nối tới cơ sở dữ liệu
            //Gọi Application.StartupPath để lấy đường dẫn tới thư mục chứa file chạy chương trình 
            con.ConnectionString = "Server =.; Database = QuanLyBanHang; Integrated Security = True";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            //Khai báo chế độ xử lý báo cáo, trong trường hợp này lấy báo cáo ở local
            rpvBaoCao.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            //Đường dẫn báo cáo
            rpvBaoCao.LocalReport.ReportPath = "BCDoanhThu.rdlc";
            //Nếu có dữ liệu
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Tạo nguồn dữ liệu cho báo cáo
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "tblBCDoanhThu";
                rds.Value = ds.Tables[0];
                //Xóa dữ liệu của báo cáo cũ trong trường hợp người dùng thực hiện câu truy vấn khác
                rpvBaoCao.LocalReport.DataSources.Clear();
                //Add dữ liệu vào báo cáo
                rpvBaoCao.LocalReport.DataSources.Add(rds);
                //Refresh lại báo cáo
                rpvBaoCao.RefreshReport();
            }
        }    
    }
}
