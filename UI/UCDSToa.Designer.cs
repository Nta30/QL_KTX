namespace QL_KTX.UI
{
    partial class UCDSToa
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSoPhongToiDa = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvDsToa = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSoTang = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvDsPhong = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSoPhongTrong = new System.Windows.Forms.TextBox();
            this.txtSoPhongHienTai = new System.Windows.Forms.TextBox();
            this.txtTenToa = new System.Windows.Forms.TextBox();
            this.txtLeftTenToa = new System.Windows.Forms.TextBox();
            this.txtLeftSoTang = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDsToa)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDsPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(19, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 16);
            this.label13.TabIndex = 11;
            this.label13.Text = "Số Phòng Tối Đa";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1482, 54);
            this.label1.TabIndex = 9;
            this.label1.Text = "Danh Sách Tòa";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel3.Controls.Add(this.btnThoat);
            this.panel3.Controls.Add(this.btnIn);
            this.panel3.Controls.Add(this.btnLamMoi);
            this.panel3.Controls.Add(this.btnLuu);
            this.panel3.Controls.Add(this.btnXoa);
            this.panel3.Controls.Add(this.btnSua);
            this.panel3.Controls.Add(this.btnThem);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.panel3.Location = new System.Drawing.Point(615, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(113, 705);
            this.panel3.TabIndex = 24;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(15, 407);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(88, 36);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(15, 337);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(88, 36);
            this.btnIn.TabIndex = 5;
            this.btnIn.Text = "In";
            this.btnIn.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(15, 274);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(88, 36);
            this.btnLamMoi.TabIndex = 4;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(15, 209);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(88, 36);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(15, 143);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(88, 36);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(15, 81);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(88, 36);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Chỉnh Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(15, 16);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(88, 36);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(19, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 16);
            this.label7.TabIndex = 33;
            this.label7.Text = "Số Phòng Hiện Tại";
            // 
            // txtSoPhongToiDa
            // 
            this.txtSoPhongToiDa.Location = new System.Drawing.Point(22, 88);
            this.txtSoPhongToiDa.Name = "txtSoPhongToiDa";
            this.txtSoPhongToiDa.Size = new System.Drawing.Size(253, 22);
            this.txtSoPhongToiDa.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.txtLeftSoTang);
            this.panel1.Controls.Add(this.txtLeftTenToa);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Controls.Add(this.dgvDsToa);
            this.panel1.Location = new System.Drawing.Point(0, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 705);
            this.panel1.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(12, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Danh Sách Tòa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(101, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Số Tầng";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(478, 115);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(92, 34);
            this.button8.TabIndex = 6;
            this.button8.Text = "Xuất Excel";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(101, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tên Tòa";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTimKiem.Location = new System.Drawing.Point(478, 54);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(92, 36);
            this.btnTimKiem.TabIndex = 5;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // dgvDsToa
            // 
            this.dgvDsToa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDsToa.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDsToa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDsToa.Location = new System.Drawing.Point(15, 197);
            this.dgvDsToa.Name = "dgvDsToa";
            this.dgvDsToa.ReadOnly = true;
            this.dgvDsToa.RowHeadersWidth = 51;
            this.dgvDsToa.RowTemplate.Height = 24;
            this.dgvDsToa.Size = new System.Drawing.Size(686, 497);
            this.dgvDsToa.TabIndex = 0;
            this.dgvDsToa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDsToa_CellClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(340, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Số Tầng";
            // 
            // txtSoTang
            // 
            this.txtSoTang.Location = new System.Drawing.Point(343, 37);
            this.txtSoTang.Name = "txtSoTang";
            this.txtSoTang.Size = new System.Drawing.Size(253, 22);
            this.txtSoTang.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(19, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Tên Tòa";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel2.Controls.Add(this.txtSoPhongHienTai);
            this.panel2.Controls.Add(this.txtSoPhongTrong);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dgvDsPhong);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtSoPhongToiDa);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtSoTang);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtTenToa);
            this.panel2.Location = new System.Drawing.Point(754, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(728, 705);
            this.panel2.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(19, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 16);
            this.label5.TabIndex = 37;
            this.label5.Text = "Danh Sách Phòng";
            // 
            // dgvDsPhong
            // 
            this.dgvDsPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDsPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvDsPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDsPhong.Location = new System.Drawing.Point(22, 197);
            this.dgvDsPhong.Name = "dgvDsPhong";
            this.dgvDsPhong.RowHeadersWidth = 51;
            this.dgvDsPhong.RowTemplate.Height = 24;
            this.dgvDsPhong.Size = new System.Drawing.Size(574, 497);
            this.dgvDsPhong.TabIndex = 36;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(340, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "Số Phòng Trống";
            // 
            // txtSoPhongTrong
            // 
            this.txtSoPhongTrong.Enabled = false;
            this.txtSoPhongTrong.Location = new System.Drawing.Point(343, 143);
            this.txtSoPhongTrong.Name = "txtSoPhongTrong";
            this.txtSoPhongTrong.Size = new System.Drawing.Size(253, 22);
            this.txtSoPhongTrong.TabIndex = 38;
            // 
            // txtSoPhongHienTai
            // 
            this.txtSoPhongHienTai.Enabled = false;
            this.txtSoPhongHienTai.Location = new System.Drawing.Point(22, 143);
            this.txtSoPhongHienTai.Name = "txtSoPhongHienTai";
            this.txtSoPhongHienTai.Size = new System.Drawing.Size(253, 22);
            this.txtSoPhongHienTai.TabIndex = 40;
            // 
            // txtTenToa
            // 
            this.txtTenToa.Location = new System.Drawing.Point(22, 35);
            this.txtTenToa.Name = "txtTenToa";
            this.txtTenToa.Size = new System.Drawing.Size(253, 22);
            this.txtTenToa.TabIndex = 2;
            // 
            // txtLeftTenToa
            // 
            this.txtLeftTenToa.Location = new System.Drawing.Point(104, 59);
            this.txtLeftTenToa.Name = "txtLeftTenToa";
            this.txtLeftTenToa.Size = new System.Drawing.Size(308, 22);
            this.txtLeftTenToa.TabIndex = 11;
            // 
            // txtLeftSoTang
            // 
            this.txtLeftSoTang.Location = new System.Drawing.Point(104, 120);
            this.txtLeftSoTang.Name = "txtLeftSoTang";
            this.txtLeftSoTang.Size = new System.Drawing.Size(308, 22);
            this.txtLeftSoTang.TabIndex = 12;
            // 
            // UCDSToa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "UCDSToa";
            this.Size = new System.Drawing.Size(1482, 780);
            this.Load += new System.EventHandler(this.UCDSToa_Load);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDsToa)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDsPhong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSoPhongToiDa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvDsToa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSoTang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgvDsPhong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSoPhongHienTai;
        private System.Windows.Forms.TextBox txtSoPhongTrong;
        private System.Windows.Forms.TextBox txtLeftSoTang;
        private System.Windows.Forms.TextBox txtLeftTenToa;
        private System.Windows.Forms.TextBox txtTenToa;
    }
}
