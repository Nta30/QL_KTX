using QL_KTX.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KTX.UI
{
    public partial class UCTrangChu : UserControl
    {
        DashboardDAL dashboardDAL = new DashboardDAL();
        public UCTrangChu()
        {
            InitializeComponent();
        }

        private void UCTrangChu_Load(object sender, EventArgs e)
        {
            //this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\TrangChu\bg_TrangChu.jpg"));
            //this.BackgroundImageLayout = ImageLayout.Stretch;
            LoadThongKeTongQuat();
            LoadChartDoanhThu();
            LoadChartTyLePhong();
            LoadChartTopDien();
        }

        private void LoadThongKeTongQuat()
        {
            DataTable dt = dashboardDAL.LayThongKeTongQuat();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtTongSinhVien.Text = row["TongSinhVien"].ToString();
                txtPhongTrong.Text = row["SoPhongTrong"].ToString();
                txtDoanhThu.Text = string.Format("{0:N0} VNĐ", row["DoanhThuDienNuoc"]);
                txtViPham.Text = row["SoViPhamThang"].ToString();
                txtThang.Text = DateTime.Now.Month.ToString();
                txtNam.Text = DateTime.Now.Year.ToString();
            }
        }

        private void LoadChartDoanhThu()
        {
            chartDoanhThu.Series["DoanhThu"].Points.Clear();
            DataTable dt = dashboardDAL.LayDoanhThuTheoNam(DateTime.Now.Year);

            foreach (DataRow row in dt.Rows)
            {
                string thang = "Tháng " + row["Thang"].ToString();
                double tien = Convert.ToDouble(row["TongTien"]);
                chartDoanhThu.Series["DoanhThu"].Points.AddXY(thang, tien);
            }
        }

        private void LoadChartTyLePhong()
        {
            chartTyLePhong.Series["TyLe"].Points.Clear();
            DataTable dt = dashboardDAL.LayTyLeLapDayPhong();

            foreach (DataRow row in dt.Rows)
            {
                string trangThai = row["TrangThai"].ToString();
                int soLuong = Convert.ToInt32(row["SoLuong"]);
                chartTyLePhong.Series["TyLe"].Points.AddXY(trangThai, soLuong);
            }
        }

        private void LoadChartTopDien()
        {
            chartTopDien.Series["SoDien"].Points.Clear();
            DataTable dt = dashboardDAL.TopPhongTieuThuDien(DateTime.Now.Month - 1, DateTime.Now.Year);

            foreach (DataRow row in dt.Rows)
            {
                string tenPhong = row["TenToa"].ToString() + "-" + row["TenPhong"].ToString();
                double soDien = Convert.ToDouble(row["SoDien"]);

                chartTopDien.Series["SoDien"].Points.AddXY(tenPhong, soDien);
            }
        }
    }
}
