using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    public partial class ClientForm
    {
        // Images
        static Image PokerTableImage = Image.FromFile(String.Format(Directory.GetCurrentDirectory() + "\\..\\..\\Images\\TableT1.png"));
        Bitmap PokerTableBitmap;

        PictureBox PBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Image = PokerTableImage,
            BackColor = Color.FromArgb(100, 88, 44, 55)
    };
    }
}
