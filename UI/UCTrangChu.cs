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
        public UCTrangChu()
        {
            InitializeComponent();
        }

        private void UCTrangChu_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, @"Images\TrangChu\bg_TrangChu.jpg"));
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
