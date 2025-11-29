using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QL_KTX.Utils
{
    internal class Functions
    {
        public void FillCombox(ComboBox cb, DataTable dt, string display, string value)
        {
            if (dt.Rows.Count == 0) return;
            if (dt.Rows[0][display].ToString() != "")
            {
                DataRow row = dt.NewRow();
                row[display] = "";
                row[value] = "";
                dt.Rows.InsertAt(row, 0);
            }

            cb.DataSource = dt;
            cb.DisplayMember = display;
            cb.ValueMember = value;
        }

        public string SinhMaTuDong(string prefix)
        {
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            return prefix + time;
        }

        public DataTable DocFileExcel(string filePath)
        {
            DataTable dt = new DataTable();
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                workbook = excelApp.Workbooks.Open(filePath);
                worksheet = (Excel.Worksheet)workbook.Sheets[1];
                Excel.Range range = worksheet.UsedRange;

                int rowCount = range.Rows.Count;
                int colCount = range.Columns.Count;

                // Đọc tiêu đề cột (Dòng 1)
                for (int i = 1; i <= colCount; i++)
                {
                    dt.Columns.Add(range.Cells[1, i].Value2.ToString());
                }

                // Đọc dữ liệu (Từ dòng 2 trở đi)
                for (int i = 2; i <= rowCount; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 1; j <= colCount; j++)
                    {
                        dr[j - 1] = range.Cells[i, j].Value2;
                    }
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (workbook != null) workbook.Close(false);
                if (excelApp != null) excelApp.Quit();
                ReleaseObject(worksheet);
                ReleaseObject(workbook);
                ReleaseObject(excelApp);
            }
            return dt;
        }

        // Hàm giải phóng bộ nhớ COM (quan trọng để không bị treo Excel ngầm)
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public void XuatFileExcel(DataGridView dgv, string tieuDe, string tenFileMacDinh)
        {
            try
            {
                // 1. Khởi tạo
                Excel.Application exApp = new Excel.Application();
                Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                // 2. Xử lý Tiêu đề (Header): Gộp ô từ cột 1 đến cột cuối cùng + Căn giữa
                int cotCuoi = dgv.Columns.Count;
                Excel.Range header = (Excel.Range)exSheet.Range[exSheet.Cells[2, 1], exSheet.Cells[2, cotCuoi]];

                // Gộp ô
                header.Merge();
                // Căn giữa
                header.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                header.Value = tieuDe;

                // Định dạng Header
                header.Font.Size = 16;
                header.Font.Name = "Times New Roman";
                header.Font.Color = Color.Red;
                header.Font.Bold = true;

                // 3. Tiêu đề các cột (Dòng 4)
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    exSheet.Cells[4, i + 1] = dgv.Columns[i].HeaderText;

                    Excel.Range colHeader = (Excel.Range)exSheet.Cells[4, i + 1];
                    colHeader.Font.Bold = true;
                    colHeader.Font.Name = "Times New Roman";
                    colHeader.Font.Size = 13;
                    colHeader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }

                // 4. Đổ dữ liệu (Từ dòng 5)
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    // Nếu là dòng new row (dòng trống cuối cùng) thì bỏ qua
                    if (dgv.Rows[i].IsNewRow) continue;

                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        var val = dgv.Rows[i].Cells[j].Value;
                        exSheet.Cells[i + 5, j + 1] = val != null ? val.ToString() : "";
                    }
                }

                // 5. Hoàn thiện
                exSheet.Columns.AutoFit();
                exSheet.Name = tenFileMacDinh;
                exBook.Activate();

                // Lưu file
                SaveFileDialog dlFile = new SaveFileDialog();
                dlFile.Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls";
                dlFile.FileName = tenFileMacDinh;

                if (dlFile.ShowDialog() == DialogResult.OK)
                {
                    exBook.SaveAs(dlFile.FileName.ToString());
                }

                exApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message);
            }
        }
    }
}
