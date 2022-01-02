
namespace QuanLyBanHang
{
    partial class frmTimHoa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvTimHoa = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenHoa = new System.Windows.Forms.TextBox();
            this.txtLoaiHoa = new System.Windows.Forms.TextBox();
            this.txtMauSac = new System.Windows.Forms.TextBox();
            this.btnTimHoa = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimHoa)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTimHoa);
            this.panel1.Controls.Add(this.txtMauSac);
            this.panel1.Controls.Add(this.txtLoaiHoa);
            this.panel1.Controls.Add(this.txtTenHoa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 160);
            this.panel1.TabIndex = 0;
            // 
            // dgvTimHoa
            // 
            this.dgvTimHoa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimHoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTimHoa.Location = new System.Drawing.Point(0, 160);
            this.dgvTimHoa.Name = "dgvTimHoa";
            this.dgvTimHoa.RowHeadersWidth = 51;
            this.dgvTimHoa.RowTemplate.Height = 24;
            this.dgvTimHoa.Size = new System.Drawing.Size(800, 290);
            this.dgvTimHoa.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên hoa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Loại hoa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Màu sắc";
            // 
            // txtTenHoa
            // 
            this.txtTenHoa.Location = new System.Drawing.Point(105, 29);
            this.txtTenHoa.Name = "txtTenHoa";
            this.txtTenHoa.Size = new System.Drawing.Size(231, 22);
            this.txtTenHoa.TabIndex = 4;
            // 
            // txtLoaiHoa
            // 
            this.txtLoaiHoa.Location = new System.Drawing.Point(105, 68);
            this.txtLoaiHoa.Name = "txtLoaiHoa";
            this.txtLoaiHoa.Size = new System.Drawing.Size(231, 22);
            this.txtLoaiHoa.TabIndex = 5;
            // 
            // txtMauSac
            // 
            this.txtMauSac.Location = new System.Drawing.Point(105, 110);
            this.txtMauSac.Name = "txtMauSac";
            this.txtMauSac.Size = new System.Drawing.Size(231, 22);
            this.txtMauSac.TabIndex = 6;
            // 
            // btnTimHoa
            // 
            this.btnTimHoa.Location = new System.Drawing.Point(482, 60);
            this.btnTimHoa.Name = "btnTimHoa";
            this.btnTimHoa.Size = new System.Drawing.Size(81, 41);
            this.btnTimHoa.TabIndex = 7;
            this.btnTimHoa.Text = "Tìm kiếm";
            this.btnTimHoa.UseVisualStyleBackColor = true;
            this.btnTimHoa.Click += new System.EventHandler(this.btnTimHoa_Click);
            // 
            // frmTimHoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvTimHoa);
            this.Controls.Add(this.panel1);
            this.Name = "frmTimHoa";
            this.Text = "Tìm hoa";
            this.Load += new System.EventHandler(this.frmTimHoa_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimHoa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvTimHoa;
        private System.Windows.Forms.Button btnTimHoa;
        private System.Windows.Forms.TextBox txtMauSac;
        private System.Windows.Forms.TextBox txtLoaiHoa;
        private System.Windows.Forms.TextBox txtTenHoa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}